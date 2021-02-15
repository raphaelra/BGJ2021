using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] List<Transform> BodyParts = new List<Transform>();

    [SerializeField] private float min_distance = 1f;
    [SerializeField] private int inicial_size;

    [SerializeField] private float speed = 1;
    [SerializeField] private float rotation_speed = 50;

    [SerializeField] GameObject body_prefab;

    private float distance;
    private Transform current_body_part;
    private Transform previous_body_part;

    void Start()
    {
        for(int i = 0; i < inicial_size - 1; i++)
        {
            AddBodyPart();
        }
    }

    void Update()
    {
        Move();

        if(Input.GetKey(KeyCode.Q))
        {
            AddBodyPart();
        }
    }

    void Move()
    {
        BodyParts[0].Translate(BodyParts[0].forward * speed * Time.smoothDeltaTime, Space.World);

        float move_direction = Input.GetAxis("Horizontal");

        if(move_direction != 0)
        {
            BodyParts[0].Rotate(Vector3.up * rotation_speed * move_direction * Time.deltaTime);
            for(int i = 1; i < BodyParts.Count; i++)
            {
                current_body_part = BodyParts[i];
                previous_body_part = BodyParts[i - 1];

                distance = Vector3.Distance(previous_body_part.position, current_body_part.position);

                Vector3 new_position = previous_body_part.position;

                new_position.y = BodyParts[0].position.y;

                float T = Time.deltaTime * distance / min_distance * speed;

                if(T > 0.5f)
                {
                    T = 0.5f;
                    current_body_part.position = Vector3.Slerp(current_body_part.position, new_position, T);
                    current_body_part.rotation = Quaternion.Slerp(current_body_part.rotation, previous_body_part.rotation, T);
                }
            }
        }        
    }

    void AddBodyPart()
    {
        Transform new_part = (Instantiate(body_prefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;

        new_part.SetParent(transform);

        BodyParts.Add(new_part);
    }
}
