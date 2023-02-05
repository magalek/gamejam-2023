using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rendererRef;

    [SerializeField] private AudioClip audioClip;
    
    public event Action<Vector2> Moved;
    public event Action StartedMoving;
    public event Action StoppedMoving; 
    
    [SerializeField, Range(1, 100)] private float speed;

    public bool IsMoving { get; private set; }
    
    private InteractionController interactionController;

    private bool lockedMovement;

    [SerializeField] private AudioSource chodzenieSoundEffect;

    private Coroutine soundCoroutine;

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

        if (Input.GetMouseButtonDown(0)) Swing();
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
            if (!IsMoving)
            {
                StartedMoving?.Invoke();
                chodzenieSoundEffect.Play();
            }
            IsMoving = true;
            Moved?.Invoke(movement);
        }
        else
        {
            if (IsMoving)
            {
                StoppedMoving?.Invoke();
                chodzenieSoundEffect.Stop();
            }
            IsMoving = false;
        }
    }

    private void Swing()
    {
        if (!EquipmentController.Instance.Has<SwordItem>()) return;

        Collider2D[] collidersBuffer = new Collider2D[20];
        
        var sword = EquipmentController.Instance.CurrentItem as SwordItem;
        
        var amount = Physics2D.OverlapCircleNonAlloc(transform.position, 5, collidersBuffer);

        for (int i = 0; i < amount; i++)
        {
            var currentCollider = collidersBuffer[i];
            if (currentCollider.TryGetComponent(out IHittable hittable))
            {
                hittable.TryHit(sword.damage, Origin.Player, AttackType.PlayerMelee, out _);
                
            }
        }
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
        
        EquipmentController.Instance.PutDownItem();
    }
}
