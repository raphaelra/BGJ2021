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
    [HideInInspector] public bool can_follow = false; 
    int index;
    bool added = false;
    GameObject game_manager;

    private GameObject player;
    public GameObject Seta;
    private Animator npcAnim;
    private float distance;
    public bool segue = false;
    public bool entregue = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        game_manager = GameObject.Find("GameManager");
        follow_manager = game_manager.GetComponent<FollowManager>();
        player = GameObject.Find("PlayerPrefab");
        npcAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Debug.Log(index);
        distance = Vector3.Distance(follow_manager.NPC[index].position, this.transform.position);
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
            segue = true;
            Seta.active = false;

            if(added == false)
            {
                follow_manager.UpdateList(transform);
                added = true;                
            }
            
        }

        if(segue)
		{
			npcAnim.SetBool("wave", false);
			npcAnim.SetBool("run", true);
			if (distance <= 10f)
			{
				npcAnim.SetBool("run", false);
			} else {
				npcAnim.SetBool("run", true);
			}
    	} 
        
        if (!entregue && !segue) {
			if (distance >= 20f){
				npcAnim.SetBool("wave", true);
			} else {
				npcAnim.SetBool("wave", false);
			}
		}

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, view_range);
    }

    public void CancelAnimation ()
    {
        npcAnim.SetBool("wave", false);
        npcAnim.SetBool("idle", true);
        npcAnim.SetBool("run", false);
    }
}