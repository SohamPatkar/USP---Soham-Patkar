using TMPro;
using UnityEngine;
using Main;
using System;
using System.Text;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Transform itemListContainer;
        [SerializeField] private TextMeshProUGUI numberOfTotalItems;
        

        private void Start()
        {
            GameManager.Instance.OnTimerUpdated += UpdateTimer;
            GameManager.Instance.GetItemService().PopulateItemsUi += PopulateItemsUI;
            GameManager.Instance.GetItemService().NumberOfTotalItems += UpdateTotalItemsUI;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnTimerUpdated -= UpdateTimer;
            GameManager.Instance.GetItemService().PopulateItemsUi -= PopulateItemsUI;
            GameManager.Instance.GetItemService().NumberOfTotalItems -= UpdateTotalItemsUI;
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
            TextMeshProUGUI itemsText = Instantiate(gameObject.AddComponent<TextMeshProUGUI>(), itemListContainer);
            itemsText.text = item;
        }


        public void MarkItemFound(string itemId) { }
        public void ShowResult(bool isWin) { }
    }
}


