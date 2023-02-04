using Interfaces;
using UnityEngine;

public class HarvestingPoint : InteractableMonoBehaviourBase
{
    [SerializeField] private Item harvestedItem;

    public override InteractionResult Interact()
    {
        base.Interact();

        EquipmentController.Instance.PickUpItem(harvestedItem);

        return new InteractionResult(false);
    }
}