using System;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Player;
using UnityEngine.UI;
using TMPro;
using UI;

namespace Main
{
    public enum GameState
    {
        Playing,
        Win,
        Lose
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private List<ItemData> itemsToFind;
        [SerializeField]private float gameTime = 60f;
        [SerializeField] private PlayerController playerGameObject;
        [SerializeField] private GameObject endPanel;
        [SerializeField] private Button restartButton;
        [SerializeField] private TextMeshProUGUI endMessageText;
        [SerializeField] private UIManager uiManager;

        private float currentTime;
        private GameState currentState;
        private ItemService itemService;

        public event Action<float> OnTimerUpdated;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            itemService = new ItemService(itemsToFind);
        }

        private void Start()
        {
            StartGame();

            restartButton.onClick.AddListener(RestartGame);
        }

        private void Update()
        {
            if (currentState == GameState.Playing)
            {
                currentTime -= Time.deltaTime;
                OnTimerUpdated?.Invoke(Mathf.Round(currentTime));
            }

            if (currentTime <= 0 && currentState == GameState.Playing)
            {
                EndGame(false);
            }
        }

        public void StartGame()
        {
            SetGameState(GameState.Playing);
            currentTime = gameTime;
            itemService.PopulateItems();
        }

        public ItemService GetItemService()
        {
            return itemService;
        }

        public PlayerController GetPlayerController()
        {
            return playerGameObject;
        }

        public void EndGame(bool isWin)
        {
            SetGameState(isWin ? GameState.Win : GameState.Lose);

            if(isWin)
            {
                endMessageText.text = "Congratulations! You found all the items!";        
                endPanel.SetActive(true);
            }
            else
            {
                endMessageText.text = "Time's up! Better luck next time!";
                endPanel.SetActive(true);
            }

            itemService.ClearItems();
            uiManager.ClearAllItems();
        }

        public void RestartGame()
        {
            endPanel.SetActive(false);
    
            itemService = new ItemService(itemsToFind);
            uiManager.Bind(itemService);

            StartGame();

            SetGameState(GameState.Playing);
        }

        public void SetGameState(GameState state)
        {
            currentState = state;
        }
    }
}
