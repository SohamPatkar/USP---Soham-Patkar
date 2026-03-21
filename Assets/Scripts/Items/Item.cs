using UnityEngine;


namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData itemData;

        private string itemId;
        public string itemName;
        private GameObject itemIcon;
        private Sprite itemSprite;
        private ItemState itemState;

        private void Start()
        {
            itemName = itemData.ItemName;
            itemIcon = itemData.ItemIcon;
            itemState = itemData.ItemState;

            if(itemData.ItemSprite == null)
            {
                itemSprite = itemIcon.GetComponent<SpriteRenderer>().sprite;
            }
        }

        public void ChangeItemState(ItemState newState)
        {
            itemState = newState;
        }
    }
}
