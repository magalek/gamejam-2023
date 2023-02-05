using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMeleeAttack : EnemyAttack
{
    [SerializeField] protected float damage;
    [SerializeField] private AnimationCurve attackCurve;
    [SerializeField] private float attackTime;
    [FormerlySerializedAs("CooldownTime")] [SerializeField] private float cooldownTime;

    private Collider2D[] collidersBuffer = new Collider2D[10];
    
    public override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(cooldownTime);
            StartCoroutine(AnimationCoroutine());
            var amount = Physics2D.OverlapCircleNonAlloc(transform.position, 5, collidersBuffer);

            for (int i = 0; i < amount; i++)
            {
                var currentCollider = collidersBuffer[i];
                if (currentCollider.TryGetComponent(out IHittable hittable))
                {
                    hittable.TryHit(damage, Origin.Enemy, out _);
                }
            }
        }
    }

    private IEnumerator AnimationCoroutine()
    {
        float t = 0;
        Vector3 initialPosition = transform.position;
        while (t < attackTime)
        {
            t += Time.deltaTime;
            var normalizedT = t / attackTime;
            transform.position = initialPosition + (transform.up * attackCurve.Evaluate(normalizedT));
            yield return null;
        }
    }
}