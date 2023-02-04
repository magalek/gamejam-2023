using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event Action<Vector2> Moved;
    
    [SerializeField, Range(1, 100)] private float speed;

    private InteractionController interactionController;

    private bool lockedMovement;
    
    private void Start()
    {
        interactionController = InteractionController.Instance;
        interactionController.Interacted += OnInteracted;
    }

    private void OnInteracted(InteractionResult result)
    {
        lockedMovement = result.LocksMovement;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (lockedMovement) return;
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += (Vector3)(movement * (speed * Time.deltaTime));
        if (movement.magnitude > 0) Moved?.Invoke(movement);
    }
}
