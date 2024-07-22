using System.Collections;
using TicTacToe.UnitTests;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.ScriptableObjects
{
    public class UnitTestRunner : MonoBehaviour
    {
        [SerializeField] private UnitTest[] unitTests;

        private int successfulTests;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void RunUnitTests()
        {
            if (unitTests.Length == 0)
            {
                Debug.Log($"{nameof(UnitTestRunner)}: No unit tests assigned.");
            }
            else
            {
                StartCoroutine(RunUnitTestsRoutine());
            }
        }

        private IEnumerator RunUnitTestsRoutine()
        {
            Debug.Log("Starting unit tests.");

            successfulTests = 0;
                
            for (int i = 0; i < unitTests.Length; i++)
            {
                yield return RunUnitTest(unitTests[i]);
            }
            
            Debug.Log("Unit tests ended.");
            Debug.Log($"Unit tests summary: [{successfulTests}/{unitTests.Length}] passed tests.");
            
        }

        private IEnumerator RunUnitTest(UnitTest unitTest)
        {
            AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync(unitTest.StartingSceneName, LoadSceneMode.Single);

            while (!sceneLoadingOperation.isDone)
            {
                yield return null;
            }
            
            unitTest.Arrange();
            unitTest.Act();
            bool testSuccessful = unitTest.Assert();

            if (testSuccessful)
            {
                successfulTests++;
            }

            Debug.Log(testSuccessful
                ? $"<color=green>Unit Test [{unitTest.Name}]: Success</color>. ({unitTest.Description})"
                : $"<color=red>Unit Test [{unitTest.Name}]: Fail</color>. ({unitTest.Description})");
        }
    }
}