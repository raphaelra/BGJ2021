﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Distancia do player para entrar em modo de ataque
    public float viewRange;
    //Distancia para sair do modo de ataque
    public float disengageDistance;
    //Velocidade de patrulhamento
    public float wanderSpeed;
    //Velocidade de perseguição
    public float followSpeed;
    //Estados que definem comportamentos da AI
    public enum stateMachine { isWaiting, isReadyToWander, isMoving, isAttacking }
    [HideInInspector]
    public stateMachine myState;
   

    //Contador de tempo
    private float timeToWait;
    private float timer;
    //Posição do destino de uma patrulha
    private Vector3 navMeshPosition;
    private NavMeshAgent navMeshAgent;
    private NavMeshHit navHit;
    [HideInInspector]
    public EnemyFollowPath pathManager;

    private Animator anim;

    void Start()
    {
        pathManager = GetComponent<EnemyFollowPath>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //Define estado inicial para patrulhar
        myState = stateMachine.isReadyToWander;
        //Define velocidade inicial de patrulhamento
        navMeshAgent.speed = wanderSpeed;
        //Velocidade angular de rotação
        navMeshAgent.angularSpeed = 320;
    }

    void Update()
    {
        //Se estiver preparado para pratulhar, patrulha
        if (myState == stateMachine.isReadyToWander)
        {
            wander();
            
        }
        //Se estiver patrulhando checa se ja chegou ao seu destino
        else if (myState == stateMachine.isMoving)
        {
            checkIfReachedDestination();
            anim.SetBool("idle", false);
            
        }
        //Se estiver esperando entre uma patrulha e outra, calcula o tempo que tem que esperar
        else if (myState == stateMachine.isWaiting)
        {
            waitForTime();
            anim.SetBool("idle", true);
            
        }
        //Se estiver em modo de ataque, executa comportamento de ataque
        else if (myState == stateMachine.isAttacking)
        {
            followAndAttack();
            
        }
    }
    //Persegue e ataca o player se estiver a uma distancia minima, muda comportamento caso esteja muito longe do player
    private void followAndAttack()
    {
        myState = stateMachine.isReadyToWander;
        navMeshAgent.speed = wanderSpeed;
        
    }

    //Calcula quanto tempo se deve esperar e muda de estado para voltar patrulhar
    private void waitForTime()
    {
        //Conta o tempo predefinido
        timer += Time.deltaTime;
        if (timer >= timeToWait)
        {
            //Reseta o contador e muda de estado
            timer = 0f;
            myState = stateMachine.isReadyToWander;
        }
    }

    //Checa se já chegou ao destino
    private void checkIfReachedDestination()
    {
        //Pega o index atual
        int index = pathManager.getPreviousIndex();
        //Pega distancia do destino
        float destinationDistance = Vector3.Distance(transform.position, pathManager.pathPoints[index].destinationPos);
        //Checa se chegou minimamente perto do destino
        if (destinationDistance <= 1f)
        {
            //Caso deva esperar, pega o tempo que se deve esperar antes de iniciar nova patrulha
            if (pathManager.pathPoints[index].shouldWait)
            {
                timeToWait = pathManager.pathPoints[index].waitTime;
                myState = stateMachine.isWaiting;
            }
            //Caso contrário inicia direto uma nova patrulha
            else
            {
                myState = stateMachine.isReadyToWander;
            }
        }
    }

    //Inicia patrulha definindo um destino randomico e válido no NavMesh
    private void wander()
    {
        //Se posição aleatória em volta da posição atual for válida
        if (RandomWanderTarget(transform.position, out navMeshPosition))
        {
            //Marca posição aleatória como destino e muda de estado
            navMeshAgent.SetDestination(navMeshPosition);
            myState = stateMachine.isMoving;
        }
    }

    //Recebe posição atual (centro) e retorna posição aleatória em torno dessa posição
    private bool RandomWanderTarget(Vector3 center, out Vector3 result)
    {
        Vector3 nextPosition = pathManager.getNextPos(center);

        //Se posição do destino for válida no NavMesh
        if (pathManager.pathPoints.Count > 0 && NavMesh.SamplePosition(nextPosition, out navHit, 1.0f, NavMesh.AllAreas) && pathManager.path != null)
        {
            //Retorna true e a posição no NavMesh
            result = navHit.position;
            return true;
        }
        else
        {
            //Caso contrário não é uma posição válida, retorna posição atual e false
            result = center;
            return false;
        }
    }
}
