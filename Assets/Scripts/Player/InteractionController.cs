using System;
using Interfaces;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    
    private Collider2D[] collidersBuffer = new Collider2D[10];

    private IInteractable currentInteractable;
    
    private void Update()
    {
        FindInteractable();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
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
