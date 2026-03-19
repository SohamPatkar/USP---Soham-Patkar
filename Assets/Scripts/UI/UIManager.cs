using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Main;
using Items;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Transform itemListContainer;
        [SerializeField] private TextMeshProUGUI numberOfTotalItems;
        [SerializeField] private GameObject itemUIElementPrefab;

        private Dictionary<string, GameObject> items = new();
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

        private void PopulateItemsUI(string item)
        {
            Debug.Log("UI CALLED");

            if (!items.ContainsKey(item))
            {
                GameObject itemsText = Instantiate(itemUIElementPrefab, itemListContainer);
                itemsText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item;
                itemsText.name = item;
                items.Add(item, itemsText);
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


