using System;
using Interfaces;
using UnityEngine;

public abstract class InteractableMonoBehaviourBase : MonoBehaviour, IInteractable
{
    public event Action<bool> InteractionStateChanged;
    
    public bool IsUsed;
    
    public virtual InteractionResult Interact()
    {
        IsUsed = !IsUsed;
        InteractionStateChanged?.Invoke(IsUsed);
        return InteractionResult.Default;
    }
}