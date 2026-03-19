using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string ItemId;
        public string ItemName;
        public GameObject ItemIcon;
        public ItemState ItemState = ItemState.NotFound;
    }
}

