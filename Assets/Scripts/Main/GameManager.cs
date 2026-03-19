using System;
using System.Collections.Generic;
using UnityEngine;
using Items;

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
        [SerializeField] private float gameTime = 60f;
        
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
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            OnTimerUpdated?.Invoke(Mathf.Round(currentTime));

            if (currentTime >= gameTime)
            {
                EndGame(false);
            }
        }

        public void StartGame()
        {
            SetGameState(GameState.Playing);

            itemService.PopulateItems();
        }

        public ItemService GetItemService()
        {
            return itemService;
        }

        public void EndGame(bool isWin)
        {
            SetGameState(isWin ? GameState.Win : GameState.Lose);
        }

        public void RestartGame()
        {
            currentTime = 0f;
            SetGameState(GameState.Playing);
        }

        public void SetGameState(GameState state)
        {
            currentState = state;
        }
    }
}
