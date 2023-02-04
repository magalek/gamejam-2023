using System;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private IProjectileShooter shooter;
    private ProjectileItem item;

    private Vector2 direction;

    private bool isStarted;

    private float traveledDistance;

    public static Projectile Spawn(IProjectileShooter shooter, ProjectileItem item)
    {
        Projectile projectile = Instantiate(item.prefab, shooter.ShotPoint, quaternion.identity);
        projectile.Shoot(shooter, item);
        return projectile;
    }
    
    private void Update()
    {
        Move();
        if (traveledDistance > 100) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable hittable))
        {
            hittable.TryHit(item.damage, shooter.ShotOrigin);
        }
    }

    private void Move()
    {
        if (!isStarted) return;
        var movement = (Vector3)direction * (item.speed * Time.deltaTime);
        transform.position += movement;
        traveledDistance += movement.magnitude;
    }

    private void Shoot(IProjectileShooter _shooter, ProjectileItem _item)
    {
        shooter = _shooter;
        direction = shooter.ShotDirection;
        item = _item;
        isStarted = true;
    }
}