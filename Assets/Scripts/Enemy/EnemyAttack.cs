using System;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public abstract void Attack();
}