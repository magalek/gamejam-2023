using Interfaces;
using UnityEngine;

public class HarvestingPoint : InteractableMonoBehaviourBase
{
    [SerializeField] private Item harvestedItem;
    [SerializeField] private AudioSource pickupSoundEffect;

    public override InteractionResult Interact()
    {
        base.Interact();
        pickupSoundEffect.Play();
        EquipmentController.Instance.PickUpItem(harvestedItem);

        return new InteractionResult(false);
       
    }
}