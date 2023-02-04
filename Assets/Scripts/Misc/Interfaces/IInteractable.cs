namespace Interfaces
{
    public interface IInteractable
    {
        InteractionResult Interact();
    }

    public struct InteractionResult
    {
        public static InteractionResult Default => new(false);
        
        public bool LocksMovement;

        public InteractionResult(bool locksMovement)
        {
            LocksMovement = locksMovement;
        }
    }
}