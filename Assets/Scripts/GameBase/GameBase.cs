using System;
using System.Collections;
using TicTacToe.Gameplay.GameParticipants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.GameBase
{
    public class GameBase : MonoBehaviour
    {
        [SerializeField] private string MainMenuSceneName;
        [SerializeField] private string GameplaySceneName;
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            LoadScene(MainMenuSceneName);
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!sceneLoadingOperation.isDone)
            {
                yield return null;
            }

            SceneBase[] sceneBases = FindObjectsOfType<SceneBase>();

            for (int i = 0; i < sceneBases.Length; i++)
            {
                sceneBases[i].Init(this);
            }
        }

        public GameParticipant[] GetParticipants()
        {
            throw new NotImplementedException();
        }
    }
}
