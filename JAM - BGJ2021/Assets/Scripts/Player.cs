using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    enum Direction {UP, DOWN, LEFT, RIGHT}
    Direction direction;
    [SerializeField] float move_rate = 1f;
    float current_time;
    Rigidbody rb;
    Vector3 next_position;
    Vector3 current_position;
    [SerializeField] float step = 0.5f;
    NavMeshAgent agent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        current_time = move_rate;
    }

    void Update()
    {
        CheckDirection();

        if(current_time <= 0)
        {
            Move();
            current_time = move_rate;
        }
        else
        {
            current_time -= Time.deltaTime;
        }

        print(current_time);
    }

    void Move()
    {
        current_position = transform.position;
        next_position = current_position + (DefineDirection() * step);

        agent.SetDestination(next_position);
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

    void CheckDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal > 0)
        {
            direction = Direction.RIGHT;
        }

        if(horizontal < 0)
        {
            direction = Direction.LEFT;
        }

        if(vertical > 0)
        {
            direction = Direction.UP;
        }

        if(vertical < 0)
        {
            direction = Direction.DOWN;
        }
    }
}
