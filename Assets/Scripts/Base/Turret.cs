using System;
using System.Collections;
using System.Collections.Generic;

public class Turret : InteractableMonoBehaviourBase
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (IsUsed) CameraController.Instance.ZoomIn();
        else CameraController.Instance.ZoomOut();
        
        base.Interact();
    }
}