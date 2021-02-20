using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceRandom : MonoBehaviour
{
    
    private Animator anim;
    public bool isStaticDancingNPC = false;
    private int danceIndex = 0;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        if (isStaticDancingNPC)
        {
            Randomizer();
        }
    }

    public void Randomizer()
    {
        danceIndex = Random.Range(1,4);
        anim.SetTrigger("dance"+danceIndex);
    }


}
