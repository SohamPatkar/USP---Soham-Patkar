using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.GameObject;


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
        private List<GameObject> spawnedItems = new();

        public event Action<string , Sprite> PopulateItemsUi;
        public event Action<int> NumberOfTotalItems;

        public Vector2 minBounds = new Vector2(-8f, -4f);
        public Vector2 maxBounds = new Vector2(8f, 4f);

        public void SpawnRandomPositions()
        {
            foreach (var item in items)
            {
                float x = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
                float y = UnityEngine.Random.Range(minBounds.y, maxBounds.y);            

                GameObject spawnedItem = Object.Instantiate(item.ItemIcon, new Vector3(x, y, 0f), quaternion.identity);
                
                spawnedItems.Add(spawnedItem);
            }
        }

        public void RemoveItem(string itemName)
        {
            foreach (var item in spawnedItems)
            {
                if (item.GetComponent<Item>().itemName == itemName)
                {
                    spawnedItems.Remove(item);
                    Object.Destroy(item);
                    break;
                }
            }
        }

        public void ClearItems()
        {
            foreach (var item in spawnedItems)
            {
                Object.Destroy(item);
            }

            spawnedItems.Clear();
            items.Clear();
        }

        public List<GameObject> GetSpawnedItems()
        {
            return spawnedItems;
        }

        public ItemService(List<ItemData> itemsToFind)
        {
            items = new List<ItemData>(itemsToFind);
        }

        public void PopulateItems()
        {
            NumberOfTotalItems?.Invoke(items.Count);
            Debug.Log("PopulateItems CALLED");

            foreach (var item in items)
            {
                PopulateItemsUi?.Invoke(item.ItemName,item.ItemIcon.GetComponent<SpriteRenderer>().sprite);
                
                Debug.Log($"Sending item: {item.ItemName}");
            }

            SpawnRandomPositions();
        }
    }
}


