using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    public float Health { get; set; }

    private EnemyAttack attack;
    
    public EnemyAsset EnemyAsset;
    
    private IMovement movement;

    public static Enemy Spawn(ISpawner spawner, EnemyAsset enemyAsset)
    {
        Enemy enemy = Instantiate(enemyAsset.prefab, spawner.Position, Quaternion.identity);
        enemy.movement.Initialize(Vector3.zero);
        enemy.Health = enemyAsset.health;
        enemy.EnemyAsset = enemyAsset;
        return enemy;
    }

    private void Awake()
    {
        movement = GetComponent<IMovement>();
        attack = GetComponent<EnemyAttack>();
        movement.EndedRoute += attack.Attack;
    }

    private void Update()
    {
        movement.Move();
    }

    public void TryHit(float damage, Origin origin, out bool hit)
    {
        hit = false;
        if (origin == Origin.Enemy) return;

        hit = true;
        Health -= damage;
        if (Health <= 0) Kill();
    }

    public void Kill()
    {
        movement.EndedRoute -= attack.Attack;
        Destroy(gameObject);
    }
}