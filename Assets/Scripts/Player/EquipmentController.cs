using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemSpriteRenderer;
    
    public static EquipmentController Instance => instance;

    private static EquipmentController instance;
    
    public Item CurrentItem { get; private set; }

    public bool Has(Item item) => CurrentItem.itemType == item.itemType;

    public bool Has<T>() where T : Item => CurrentItem is T;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }

        PutDownItem();
    }

    public void PickUpItem(Item item)
    {
        if (CurrentItem != null) return;

        CurrentItem = item;
        
        itemSpriteRenderer.sprite = CurrentItem.sprite;
        itemSpriteRenderer.enabled = true;
    }

    public void PutDownItem()
    {
        itemSpriteRenderer.sprite = null;
        itemSpriteRenderer.enabled = false;
        
        CurrentItem = null;
    }
}