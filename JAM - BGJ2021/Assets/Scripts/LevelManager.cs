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
        follow_manager = GetComponent<FollowManager>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(current_npcs >= total_of_npcs)
        {
            EndLevelDisplay();
            NextLevel(next_level);
        }
    }

    public void UpdateScore(int i)
    {
        current_npcs += i;
        follow_manager.ResetList();
    }

    public void EndLevelDisplay()
    {
        //animar a UI com um botãoq que triggar que NextLevel()
    }

    void NextLevel(string s)
    {
        SceneManager.LoadScene(next_level);
    }
}
