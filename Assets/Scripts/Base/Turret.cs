using Interfaces;
using UnityEngine;

public interface IActivable
{
    bool Activated { get; }

    void ChangeActivationState(bool state);
}

public class Turret : InteractableMonoBehaviourBase, IActivable
{
    [SerializeField] private Root attachPoint;
    
    public bool Activated { get; private set; }
    
    private TurretMovement movement;
    private TurretShooting shooting;
    
    private LineRenderer lineRenderer;

    private void Awake()
    {
        movement = GetComponent<TurretMovement>();
        shooting = GetComponent<TurretShooting>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        DrawAimLine();
    }

    public void ChangeActivationState(bool state)
    {
        Activated = state;
    }
    
    public override InteractionResult Interact()
    {
        if (!IsUsed)
        {
            if (!Activated)
            {
                if (EquipmentController.Instance.Has<RootItem>())
                {
                    RootsController.Instance.AttachRoot(attachPoint, this);
                    Activated = true;
                    EquipmentController.Instance.PutDownItem();
                }
                return InteractionResult.Default;
            }

            if (!EquipmentController.Instance.Has<ProjectileItem>())
            {
                return InteractionResult.Default;
            }
        }

        base.Interact();
        if (!IsUsed) CameraController.Instance.ZoomIn();
        else
        {
            CameraController.Instance.ZoomOut();
        }

        lineRenderer.enabled = IsUsed;
        return new InteractionResult(IsUsed);
    }

    private void DrawAimLine()
    {
        if (!IsUsed) return;
        var position = transform.position;
        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position + (movement.TurretDirection * shooting.Range));
    }
}