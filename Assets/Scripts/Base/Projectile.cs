using System;
using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private IProjectileShooter shooter;

    private float damage;
    private float speed;
    
    private Vector2 direction;
    private float range;

    private bool isStarted;

    private float traveledDistance;

    public static Projectile Spawn(IProjectileShooter shooter, ProjectileItem item, float range)
    {
        Projectile projectile = Instantiate(item.prefab, shooter.ShotPoint, quaternion.identity);
        projectile.Shoot(shooter, item, range);
        return projectile;
    }
    
    public static Projectile Spawn(IProjectileShooter shooter, Projectile prefab, float damage, float speed, float range)
    {
        Projectile projectile = Instantiate(prefab, shooter.ShotPoint, quaternion.identity);
        projectile.Shoot(shooter, damage, speed, range);
        return projectile;
    }
    
    private void Update()
    {
        Move();
        if (traveledDistance >= range) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable hittable))
        {
            hittable.TryHit(damage, shooter.ShotOrigin, out bool hit);
            if (hit) Destroy(gameObject);
        }
    }

    private void Move()
    {
        if (!isStarted) return;
        var movement = (Vector3)direction * (speed * Time.deltaTime);
        transform.position += movement;
        traveledDistance += movement.magnitude;
    }

    private void Shoot(IProjectileShooter _shooter, ProjectileItem _item, float _range) => Shoot(_shooter, _item.damage, _item.speed, _range);

    private void Shoot(IProjectileShooter _shooter, float _damage, float _speed, float _range)
    {
        shooter = _shooter;
        direction = shooter.ShotDirection;
        damage = _damage;
        speed = _speed; 
        range = _range;
        isStarted = true;
    }
}