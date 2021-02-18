using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    [SerializeField] Vector3 step;
    [SerializeField] Transform destination;
    [SerializeField] float move_rate = 1f;
    Vector3 start_position;
    
    void Start()
    {
        start_position = transform.position;
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(move_rate);

        transform.position += step;

        if(Vector3.Distance(transform.position, destination.position) < 0.5f)
        {
            print("chegou no destino");
            transform.position = start_position;
        }

        StartCoroutine("Move");
    }
}
