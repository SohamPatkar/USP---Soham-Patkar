using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string ItemId;
        public string ItemName;
        public GameObject ItemIcon;
        public Sprite ItemSprite;
        public ItemState ItemState = ItemState.NotFound;
    }
}

