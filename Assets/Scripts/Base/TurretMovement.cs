using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    private Turret turret;

    private CameraController cameraController;
    private PlayerController playerController;
    
    private void Awake()
    {
        cameraController = CameraController.Instance;
        turret = GetComponent<Turret>();
    }

    private void Update()
    {
        if (turret.IsUsed) Rotate();
    }

    private void Rotate()
    {
        var mousePositionWorldSpace = cameraController.cameraRef.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePositionWorldSpace - playerController.transform.position;
        direction.Normalize();
        
        //transform.Rotate();
    }
}