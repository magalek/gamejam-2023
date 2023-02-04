using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Create Enemy", fileName = "Enemy", order = 0)]
public class EnemyAsset : ScriptableObject
{
    public Sprite sprite;
    public Enemy prefab;
    public float health;
    public float movementSpeed;
}