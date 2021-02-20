using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    GameObject game_manager;
    FollowManager follow_manager;
    NavMeshAgent agent;
    [SerializeField] float view_range;
    [SerializeField] LayerMask player_layer;
    [SerializeField] Animator animator;
    Transform player;
    bool player_in_range;
    bool can_follow = false;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        game_manager = GameObject.Find("GameManager");
        follow_manager = game_manager.GetComponent<FollowManager>();
        player = GameObject.Find("PlayerPrefab").transform;
        gameObject.tag = "Wall";
    }

    
    void Update()
    {
        player_in_range = Physics.CheckSphere(transform.position, view_range, player_layer);

        if(player_in_range)
        {
            can_follow = true;
            view_range = 0f;
        }

        if(can_follow)
        {
            agent.SetDestination(player.position);
        }
    }
}
