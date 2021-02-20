using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class EndGoal : MonoBehaviour
{
    [SerializeField] GameObject game_manager;
    FollowManager follow_manager;
    LevelManager level_manager;
    [SerializeField] Transform end_position;
    [SerializeField] float offset = 3f;

    void Start()
    {
        follow_manager = game_manager.GetComponent<FollowManager>();
        level_manager = game_manager.GetComponent<LevelManager>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DeliverNPC();
            level_manager.UpdateScore(follow_manager.NPC.Count - 1);
        }
    }

    void DeliverNPC()
    {
        for(int i = 1; i < follow_manager.NPC.Count; i++) //começa no 1 porque 0 é o player
        {
            Vector3 difference = new Vector3 (Random.Range(-offset, offset), 0f, Random.Range(-offset, offset));

            NavMeshAgent agent = follow_manager.NPC[i].GetComponent<NavMeshAgent>();
            NPC npc = follow_manager.NPC[i].GetComponent<NPC>();
            DanceRandom danceRandom = follow_manager.NPC[i].GetComponent<DanceRandom>();
            npc.can_follow = false;
            npc.segue = false;
            npc.entregue = true;
            npc.CancelAnimation();
            danceRandom.Randomizer();

            print(agent.name + " está indo para " + end_position.position);
            agent.SetDestination(end_position.position + difference);
        }
    }
}
