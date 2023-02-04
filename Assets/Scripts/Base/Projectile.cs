using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 direction;

    private float traveledDistance;

    private void Update()
    {
        var movement = (Vector3)direction * (speed * Time.deltaTime);
        transform.position += movement;
        traveledDistance += movement.magnitude;
        if (traveledDistance > 100) Destroy(gameObject);
    }

    public void Shoot(Vector2 _direction)
    {
        direction = _direction;
    }
}