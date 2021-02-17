using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public List<Transform> NPC = new List<Transform>();

    void Start()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        NPC.Add(player);
    }

    public void UpdateList(Transform obj)
    {
        NPC.Add(obj);
    }
    
}