using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    [SerializeField] List<Transform> NPCs = new List<Transform>();

    List<NavMeshAgent> agents = new List<NavMeshAgent>();

    void Start()
    {

    }

    void Update()
    {
        FollowNext();
    }

    void FollowNext()
    {
        for (int i = 1; i < NPCs.Count; i++)
        {
            agents[i] = NPCs[i].GetComponent<NavMeshAgent>();
            agents[i].SetDestination(NPCs[i - 1].transform.position);
        }
    }

    void AddNPC(GameObject obj)
    {
        Transform new_npc = (Instantiate(obj, NPCs[NPCs.Count - 1].position, NPCs[NPCs.Count - 1].rotation) as GameObject).transform;

        new_npc.transform.SetParent(transform);

        NPCs.Add(new_npc);        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            GameObject obj = other.gameObject;
            AddNPC(obj);
            //Destroy(other.gameObject);
        }
    }    
}
