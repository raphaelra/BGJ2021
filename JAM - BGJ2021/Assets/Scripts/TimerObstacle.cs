using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerObstacle : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float timer = 1f;
    float current_time;

    void Start()
    {
        gameObject.tag = "Wall";
    }

    void Update()
    {
        if(current_time <= 0)
        {
            animator.Play("Attack"); //ex.: tampa do bueiro levantada
            current_time = timer;
        }
        else
        {
            animator.Play("Rest"); //ex.: tampa do bueiro abaixada
            current_time -= Time.deltaTime;
        }
    }

}
