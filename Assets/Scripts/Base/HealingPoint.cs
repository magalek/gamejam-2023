using System;
using Interfaces;
using UnityEngine;

public class HealingPoint : InteractableMonoBehaviourBase, IActivable
{
    [SerializeField] private Root attachPoint;

    [SerializeField] private float healingPower;
    [SerializeField] private float tickCooldown;
    
    public bool Activated { get; private set;  }

    private float t;
    
    public override InteractionResult Interact()
    {
        if (EquipmentController.Instance.Has<RootItem>())
        {
            RootsController.Instance.AttachRoot(attachPoint, this);
            Activated = true;
            EquipmentController.Instance.PutDownItem();
        }
        else
        {
            return InteractionResult.Default;
        }

        base.Interact();

        return new InteractionResult(false);
    }

    private void Update()
    {
        if (!Activated) return;
        
        t += Time.deltaTime;

        if (t > tickCooldown)
        {
            BaseStructure.Instance.Heal(healingPower);
            t = 0;
        }
    }
    
    public void ChangeActivationState(bool state)
    {
        Activated = state;
        t = 0;
    }
}