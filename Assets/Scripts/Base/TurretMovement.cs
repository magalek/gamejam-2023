using UnityEngine;
using UnityEngine.Serialization;

public class TurretMovement : MonoBehaviour
{
    [FormerlySerializedAs("TurretForwardDirection")] [FormerlySerializedAs("TurretForward")] [FormerlySerializedAs("Direction")] public Vector3 TurretDirection;
    
    private Turret turret;

    private CameraController cameraController;
    private PlayerController playerController;

    private void Start()
    {
        cameraController = CameraController.Instance;
        playerController = PlayerController.Instance;
        turret = GetComponent<Turret>();
    }

    private void Update()
    {
        CalculateDirection();
        if (turret.IsUsed) Rotate();
    }

    private void CalculateDirection()
    {
        var mousePositionWorldSpace = cameraController.cameraRef.ScreenToWorldPoint(Input.mousePosition);
        TurretDirection = mousePositionWorldSpace - playerController.transform.position;
        TurretDirection.z = 0;
        TurretDirection.Normalize();
    }

    private void Rotate()
    {
        var playerPosition = playerController.transform.position;
        Debug.DrawLine(playerPosition, playerPosition + TurretDirection, Color.red);

        transform.rotation = Quaternion.FromToRotation(Vector3.up, TurretDirection);
    }
}