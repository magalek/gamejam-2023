using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private float speed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position += (Vector3)(movement * (speed * Time.deltaTime));
    }
}
