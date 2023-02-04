namespace Interfaces
{
    public interface IInteractable
    {
        InteractionResult Interact();
    }

    public struct InteractionResult
    {
        public bool LocksMovement;

        public InteractionResult(bool locksMovement)
        {
            LocksMovement = locksMovement;
        }
    }
}