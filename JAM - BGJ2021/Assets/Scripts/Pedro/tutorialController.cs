using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialController : MonoBehaviour
{
    
    private BasicCharacter bc;
    private Animator anim;

    void Start()
    {
        bc  = GameObject.Find("PlayerPrefab").GetComponent<BasicCharacter>();
        anim = GetComponent<Animator>();
        bc.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            anim.SetTrigger("close");
            bc.enabled = true;
        }
    }

    
    public void close()
    {
        this.gameObject.active = false;
    }
}
