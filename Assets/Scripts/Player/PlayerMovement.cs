using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rendererRef;
    
    public event Action<Vector2> Moved;
    public event Action StartedMoving;
    public event Action StoppedMoving;
    
    [SerializeField, Range(1, 100)] private float speed;

    public bool IsMoving { get; private set; }
    
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
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement.Normalize();
        transform.position += (Vector3)(movement * (speed * Time.deltaTime));
        
        if (movement.magnitude > 0)
        {
            rendererRef.flipX = movement.x > 0;
            if (!IsMoving) StartedMoving?.Invoke();
            IsMoving = true;
            Moved?.Invoke(movement);
        }
        else
        {
            if (IsMoving) StoppedMoving?.Invoke();
            IsMoving = false;
        }
    }
}
