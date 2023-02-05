using System.Collections;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttack, IProjectileShooter
{
    [SerializeField] private float castTime;
    [SerializeField] private ProjectileItem projectileItem;
    [SerializeField] private AudioSource atakSoundEffect;
    [SerializeField] private AudioSource strzalSoundEffect;

    public Vector2 ShotPoint => transform.position;
    public Vector2 ShotDirection { get; private set; }
    public Origin ShotOrigin => Origin.Enemy;
    
    public float NormalizedCurrentCastTime { get; private set; }

    private Vector3 targetPosition;
    
    private void Awake()
    {
        targetPosition = BaseStructure.Instance.transform.position;
    }

    public override void Attack()
    {
        StartCoroutine(ShootingCoroutine());
   
    }

    private IEnumerator ShootingCoroutine()
    {
        ShotDirection = targetPosition - transform.position;
        while (gameObject.activeSelf)
        {
            atakSoundEffect.Play();
            yield return StartCoroutine(CastCoroutine());
            Shoot();
            yield return new WaitForSeconds(projectileItem.cooldown);
        }
    }

    private IEnumerator CastCoroutine()
    {
        float t = 0;
        while (t < castTime)
        {
            t += Time.deltaTime;
            NormalizedCurrentCastTime = t / castTime;
            yield return null;
        }
    }

    private void Shoot()
    {
        Projectile.Spawn(this, projectileItem, short.MaxValue);
        strzalSoundEffect.Play();
    }
}