using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float cooldown;
    
    private TurretMovement movement;
    private Turret turret;

    private float elapsedTime;
    
    private void Awake()
    {
        movement = GetComponent<TurretMovement>();
        turret = GetComponent<Turret>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0) && turret.IsUsed && elapsedTime > cooldown)
        {
            Shoot();
        }

        elapsedTime += Time.deltaTime;
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Shoot(movement.TurretDirection);
        elapsedTime = 0;
    }
}