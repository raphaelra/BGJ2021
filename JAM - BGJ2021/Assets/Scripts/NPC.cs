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
    public bool can_follow = false; 
    [SerializeField] int index;
    bool added = false;
    GameObject game_manager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        game_manager = GameObject.Find("GameManager");
        follow_manager = game_manager.GetComponent<FollowManager>();
    }

    void Update()
    {
        player_in_range = Physics.CheckSphere(transform.position, view_range, player_layer);

        if(player_in_range)
        {
            can_follow = true;

            index = follow_manager.NPC.Count - 1;

            view_range = 0f;
        }

        if(can_follow)
        {            
            agent.SetDestination(follow_manager.NPC[index].transform.position);

            if(added == false)
            {
                follow_manager.UpdateList(transform);
                added = true;                
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, view_range);
    }
}