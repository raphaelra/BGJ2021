using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform end_position;
    [SerializeField] float step = 0.5f;
    bool can_move = false;

    void Update()
    {
        if(can_move)
        {
            target.transform.position = Vector3.MoveTowards(target.position, end_position.position, step);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            can_move = true;
        }
    }
}
