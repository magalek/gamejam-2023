using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Turret : InteractableMonoBehaviourBase
{
    private TurretMovement movement;
    
    private LineRenderer lineRenderer;

    private void Awake()
    {
        movement = GetComponent<TurretMovement>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DrawAimLine();
    }
    
    public override InteractionResult Interact()
    {
        base.Interact();
        if (!IsUsed) CameraController.Instance.ZoomIn();
        else CameraController.Instance.ZoomOut();

        lineRenderer.enabled = IsUsed;
        return new InteractionResult(IsUsed);
    }

    private void DrawAimLine()
    {
        if (!IsUsed) return;
        var position = transform.position;
        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position + (movement.Direction * 30));
    }
}