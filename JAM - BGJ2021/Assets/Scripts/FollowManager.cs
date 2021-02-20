using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public List<Transform> NPC = new List<Transform>();
    Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        NPC.Add(player);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            print(NPC.Count);
        }
    }

    public void UpdateList(Transform obj)
    {
        NPC.Add(obj);
    }

    public void ResetList()
    {
        NPC.RemoveRange(1, NPC.Count - 1);
    }
    
}