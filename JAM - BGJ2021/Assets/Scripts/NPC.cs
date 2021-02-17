using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    NavMeshAgent agent;
    FollowManager follow_manager;
    [SerializeField] float view_range;
    [SerializeField] LayerMask player_layer;
    bool player_in_range;
    bool can_follow = false; 
    [SerializeField] int index;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        follow_manager = GameObject.FindWithTag("Follow Manager").GetComponent<FollowManager>();
    }

    void Update()
    {
        player_in_range = Physics.CheckSphere(transform.position, view_range, player_layer);

        if(player_in_range)
        {
            print("detectou player");
            can_follow = true;
            index = follow_manager.NPC.Count - 1;
            view_range = 0f;
        }

        if(can_follow)
        {            
            agent.SetDestination(follow_manager.NPC[index].transform.position);

            if(follow_manager.NPC.Contains(transform) == false)
            {
                follow_manager.UpdateList(transform);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, view_range);
    }
}