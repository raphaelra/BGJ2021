using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolMovement : MonoBehaviour
{
    [SerializeField] float step = 1f;
    [SerializeField] Transform destination;
    [SerializeField] float move_rate = 1f;
    float current_time;
    Vector3 start_position;
    Vector3 next_position;
    Vector3 current_position;

    enum Direction {UP, DOWN, LEFT, RIGHT}
    [SerializeField] Direction direction;
    NavMeshAgent agent;
    
    void Start()
    {
        start_position = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(current_time <= 0)
        {
            Move();
            current_time = move_rate;
        }
        else
        {
            current_time -= Time.deltaTime;
        }
    }

    void Move()
    {
        current_position = transform.position;
        next_position = current_position + (DefineDirection() * step);

        agent.SetDestination(next_position);

        if(Vector3.Distance(transform.position, destination.position) < 0.5f)
        {
            transform.position = start_position;
        }
    }

    Vector3 DefineDirection()
    {
        switch(direction)
        {
            case(Direction.UP):
            return new Vector3(0, 0, 1);

            case(Direction.DOWN):
            return new Vector3(0, 0, -1);

            case(Direction.LEFT):
            return new Vector3(-1, 0, 0);

            case(Direction.RIGHT):
            return new Vector3(1, 0, 0);
        }
        return Vector3.zero;
    }
}
