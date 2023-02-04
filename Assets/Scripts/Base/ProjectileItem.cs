using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Items/Create Projectile", fileName = "Item", order = 0)]
public class ProjectileItem : Item
{
    public Projectile prefab;
    public int shootsAmount;
    public float speed;
    public float cooldown;
    public float damage;
}