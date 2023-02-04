
using System;
using UnityEngine;

public enum Origin
{
    Player,
    Enemy
}

public interface ISpawner
{
    Vector2 Position { get; }
}

public interface IMovement
{
    void Initialize(Vector2 _destination, ISpawner _spawner);
    void Move();
}

public interface IHittable
{
    float Health { get; }
    void TryHit(float damage, Origin origin);
    void Kill();
}

public class Enemy : MonoBehaviour, IHittable
{
    public float Health { get; set; }

    public EnemyAsset EnemyAsset;
    
    private IMovement movement;

    public static Enemy Spawn(ISpawner spawner, EnemyAsset enemyAsset)
    {
        Enemy enemy = Instantiate(enemyAsset.prefab, spawner.Position, Quaternion.identity);
        enemy.movement.Initialize(Vector3.zero, spawner);
        enemy.Health = enemyAsset.health;
        enemy.EnemyAsset = enemyAsset;
        return enemy;
    }

    private void Awake()
    {
        movement = GetComponent<IMovement>();
    }

    private void Update()
    {
        movement.Move();
    }


    public void TryHit(float damage, Origin origin)
    {
        Debug.Log($"Try hit {gameObject.name}");
        if (origin == Origin.Enemy) return;

        Health -= damage;
        if (Health <= 0) Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}