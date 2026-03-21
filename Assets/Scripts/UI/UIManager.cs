using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Main;
using Items;
using Unity.VisualScripting;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Transform itemListContainer;
        [SerializeField] private TextMeshProUGUI numberOfTotalItems;
        [SerializeField] private GameObject itemUIElementPrefab;

        private Dictionary<string, GameObject> items = new();
        private List<GameObject> spawnedItems = new();
        private ItemService currentService;

        private void Start()
        {
            GameManager.Instance.OnTimerUpdated += UpdateTimer;
            GameManager.Instance.GetItemService().PopulateItemsUi += PopulateItemsUI;
            GameManager.Instance.GetItemService().NumberOfTotalItems += UpdateTotalItemsUI;
            GameManager.Instance.GetPlayerController().OnItemFound += MarkItemFound;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnTimerUpdated -= UpdateTimer;
            GameManager.Instance.GetItemService().PopulateItemsUi -= PopulateItemsUI;
            GameManager.Instance.GetItemService().NumberOfTotalItems -= UpdateTotalItemsUI;
            GameManager.Instance.GetPlayerController().OnItemFound -= MarkItemFound;
        }

        private void UpdateTotalItemsUI(int itemCount)
        {
            numberOfTotalItems.text = $"Total Items: {itemCount}";
        }

        private void UpdateTimer(float time)
        {
            timerText.text = time.ToString();
        }

        private void PopulateItemsUI(string item, Sprite itemSprite)
        {
            Debug.Log("UI CALLED");

            if (!items.ContainsKey(item))
            {
                GameObject itemsIcon = Instantiate(itemUIElementPrefab, itemListContainer);
                itemsIcon.GetComponent<Image>().sprite = itemSprite;
                itemsIcon.name = item;
                items.Add(item, itemsIcon);
                AddListenersToHighlight();
            }
        }

        private void AddListenersToHighlight()
        {
            foreach( var item in items)
            {
                item.Value.GetComponent<Button>().onClick.AddListener(() => HighlightItem(item.Key));
            }
        }

        private void HighlightItem(string itemName)
        {
            spawnedItems = GameManager.Instance.GetItemService().GetSpawnedItems();

            foreach (var item in spawnedItems)
            {
                if (item.GetComponent<Item>().itemName == itemName)
                {
                    var color = item.GetComponent<SpriteRenderer>().color;
                    color.a = 0.1f; 
                    item.GetComponent<SpriteRenderer>().color = color;
                }
            }
        }

        public void ClearAllItems()
        {
            foreach (var item in items.Values)
            {
                Destroy(item);
            }

            items.Clear();
        }

        public void Bind(ItemService newService)
        {
            if (currentService != null)
            {
                currentService.PopulateItemsUi -= PopulateItemsUI;
                currentService.NumberOfTotalItems -= UpdateTotalItemsUI;
            }

            currentService = newService;

            currentService.PopulateItemsUi += PopulateItemsUI;
            currentService.NumberOfTotalItems += UpdateTotalItemsUI;
        }

        public void MarkItemFound(string itemName)
        {
            if (items.ContainsKey(itemName))
            {
                GameObject item = items[itemName];
                Destroy(item);
                items.Remove(itemName);
                GameManager.Instance.GetItemService().RemoveItem(itemName);
                UpdateTotalItemsUI(items.Count);

                if (items.Count == 0)
                {
                    GameManager.Instance.EndGame(true);
                }
            }
        }

        public void ShowResult(bool isWin) { }
    }
}


