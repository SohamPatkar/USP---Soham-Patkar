using UnityEngine;


namespace Items
{
    public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

     public string itemId;
    public string itemName;
    public GameObject itemIcon;
    public ItemState itemState;

    private void Start()
    {
        itemName = itemData.ItemName;
        itemIcon = itemData.ItemIcon;
        itemState = itemData.ItemState;
    }

    public void ChangeItemState(ItemState newState)
    {
        itemState = newState;
    }
}
}
