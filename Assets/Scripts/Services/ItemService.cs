using System;
using System.Collections.Generic;

namespace Items
{
    public enum ItemState
    {
        NotFound,
        Found
    }

    public class ItemService
    {
        private List<ItemData> items = new();

        public event Action<string> PopulateItemsUi;
        public event Action<int> NumberOfTotalItems;

        public ItemService(List<ItemData> itemsToFind)
        {
            items = itemsToFind;
        }

        public void PopulateItems()
        {
            NumberOfTotalItems?.Invoke(items.Count);

            foreach (var item in items)
            {
                PopulateItemsUi?.Invoke(item.ItemName);
            }
        }
    }
}


