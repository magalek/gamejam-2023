using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float damage;
    
    public abstract void Attack();
}