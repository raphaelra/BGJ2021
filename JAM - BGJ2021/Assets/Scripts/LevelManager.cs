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

    public GameObject fadeT;

    private TMPro.TextMeshProUGUI NpcText;

    void Start()
    {
        follow_manager = GetComponent<FollowManager>();
        player = GameObject.FindWithTag("Player").transform;
        NpcText  = GameObject.Find("BonecoText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        NpcText.text = current_npcs + " / " + total_of_npcs;
        if(current_npcs >= total_of_npcs)
        {
            EndLevelDisplay();

            Animator anim = player.GetComponentInChildren<Animator>();
            CharacterController controller = player.GetComponent<CharacterController>();
            controller.enabled = false;
            
            GameObject playerModel = GameObject.Find("radio");
            playerModel.transform.rotation = Quaternion.Euler(0f, 180f, 0f);   

            anim.SetBool("death", false);
		    anim.SetBool("walk", false);
		    anim.SetBool("land", false);
            anim.SetTrigger("dance");

            NextLevel();
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

    public void NextLevel()
    {
        StartCoroutine("ChangeScene");
    }

    public void NextLevelButton()
    {
        StartCoroutine("ChangeSceneButton");
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(4);
        fadeT.active = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(next_level);
    }

    public IEnumerator ChangeSceneButton()
    {
        fadeT.active = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(next_level);
    }
}
