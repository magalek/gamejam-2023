using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int IsMovingHash = Animator.StringToHash("isMoving");

    private PlayerMovement movement;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<PlayerMovement>();
        movement.StartedMoving += OnStartedMoving;
        movement.StoppedMoving += OnStoppedMoving;
    }

    private void OnStartedMoving() => animator.SetBool(IsMovingHash, true);

    private void OnStoppedMoving() => animator.SetBool(IsMovingHash, false);
}