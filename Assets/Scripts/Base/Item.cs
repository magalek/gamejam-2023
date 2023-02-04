using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create Item", fileName = "Item", order = 0)]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public Sprite sprite;
}

public enum ItemType
{
    Projectile
}