using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretMovement : MonoBehaviour
{
    public Vector3 Direction;
    
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
        Direction = mousePositionWorldSpace - playerController.transform.position;
        Direction.z = 0;
        Direction.Normalize();
    }

    private void Rotate()
    {
        var playerPosition = playerController.transform.position;
        Debug.DrawLine(playerPosition, playerPosition + Direction, Color.red);

        transform.rotation = Quaternion.FromToRotation(Vector3.up, Direction);
    }
}