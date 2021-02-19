using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent agent2;
    public GameObject player;
    public GameObject bola;
	public Animator anim;

    bool segue = false;

    void Update()
    {
    	if(Input.GetKeyDown(KeyCode.F)){
    		if(segue == false){
    			segue = true;
    		}else {
    			segue = false;
    		}
    	}
    	if(segue == true){
        	agent.SetDestination(player.transform.position);
			anim.SetBool("idle", false);
            //agent2.SetDestination(bola.transform.position);
    	}
    }
}
