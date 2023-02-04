using System;
using Interfaces;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public event Action<InteractionResult> Interacted;
    
    [SerializeField] private float interactionRange;
    
    public static InteractionController Instance => instance;

    private static InteractionController instance;
    
    private Collider2D[] collidersBuffer = new Collider2D[10];

    private IInteractable currentInteractable;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }
    }
    
    private void Update()
    {
        FindInteractable();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            var result = currentInteractable.Interact();
            Interacted?.Invoke(result);
        }
    }

    private void FindInteractable()
    {
        var collidersAmount = Physics2D.OverlapCircleNonAlloc(transform.position, interactionRange, collidersBuffer);

        for (int i = 0; i < collidersAmount; i++)
        {
            var currentCollider = collidersBuffer[i];
            if (currentCollider.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
                return;
            }
        }

        currentInteractable = null;
    }
}
