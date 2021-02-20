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
	public float distance;

    void Update()
    {
		distance = Vector3.Distance(player.transform.position,agent.transform.position);

    	if(Input.GetKeyDown(KeyCode.F)){
    		if(segue == false){
    			segue = true;
    		}else {
    			segue = false;
    		}
    	}
    	if(segue == true){
			anim.SetBool("wave", false);
        	agent.SetDestination(player.transform.position);
			anim.SetBool("run", true);
            //agent2.SetDestination(bola.transform.position);
			if (distance <= 10f)
			{
				anim.SetBool("run", false);
			}else {
				anim.SetBool("run", true);
			}
    	}else 
		{
			if (distance >= 20f){
				anim.SetBool("wave", true);
			}else 
			{
				anim.SetBool("wave", false);
			}
		}
    }
}
