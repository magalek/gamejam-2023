using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Turret : InteractableMonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (isUsed) CameraController.Instance.ZoomIn();
        else CameraController.Instance.ZoomOut();


        base.Interact();
    }
}

public abstract class InteractableMonoBehaviour : MonoBehaviour, IInteractable
{
    protected bool isUsed;
    
    public virtual void Interact()
    {
        isUsed = !isUsed;
    }
}
