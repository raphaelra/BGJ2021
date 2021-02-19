using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    FollowManager follow_manager;

    [SerializeField] string next_level;
    public int total_of_npcs;
    int current_npcs;
    [SerializeField] EndGoal end;
    Transform player;

    void Start()
    {
        follow_manager = GameObject.FindWithTag("Follow Manager").GetComponent<FollowManager>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(current_npcs >= total_of_npcs)
        {
            NextLevel(next_level);
        }
    }

    public void UpdateScore(int i)
    {
        current_npcs += i;
        follow_manager.ResetList();
    }

    void NextLevel(string s)
    {

    }
}
