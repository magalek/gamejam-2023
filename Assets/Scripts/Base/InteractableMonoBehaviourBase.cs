using System;
using Interfaces;
using UnityEngine;

public abstract class InteractableMonoBehaviourBase : MonoBehaviour, IInteractable
{
    public event Action<bool> InteractionStateChanged;
    
    public bool IsUsed;
    
    public virtual void Interact()
    {
        IsUsed = !IsUsed;
        InteractionStateChanged?.Invoke(IsUsed);
    }
}