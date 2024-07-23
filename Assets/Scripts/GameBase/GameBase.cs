using System;
using System.Collections;
using TicTacToe.Gameplay.GameParticipants;
using TicTacToe.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.GameBase
{
    public class GameBase : MonoBehaviour
    {
        [field: SerializeField] public Reskinner Reskinner { get; private set; }

        [SerializeField] private GameSettings gameSettings;
        
        [Header("Scene Names")]
        [SerializeField] private string MainMenuSceneName;
        [SerializeField] private string GameplaySceneName;
        
        public GameParticipant[] GameParticipants { get; set; }
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            
            Reskinner.Init(gameSettings.Visuals);

            LoadMainMenu();
        }

        public void LoadMainMenu()
        {
            StartCoroutine(LoadSceneAsync(MainMenuSceneName, OnMainMenuLoaded));
        }

        public void LoadGameplay()
        {
            StartCoroutine(LoadSceneAsync(GameplaySceneName, OnGameplayLoaded));
        }

        private IEnumerator LoadSceneAsync(string sceneName, Action sceneLoadedCallback)
        {
            AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!sceneLoadingOperation.isDone)
            {
                yield return null;
            }
            
            sceneLoadedCallback?.Invoke();
        }

        private void OnMainMenuLoaded()
        {
            MainMenu.MainMenu mainMenu = FindObjectOfType<MainMenu.MainMenu>();
            mainMenu.Init(this);
        }
        
        private void OnGameplayLoaded()
        {
            Gameplay.Gameplay gameplay = FindObjectOfType<Gameplay.Gameplay>();
            gameplay.Init(GameParticipants, gameSettings.Balance, Reskinner.CurrentVisuals);
            gameplay.StartNewTicTacToeGame();
        }
    }
}
