using System.Collections;
using TicTacToe.UnitTests;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.ScriptableObjects
{
    public class UnitTestRunner : MonoBehaviour
    {
        [SerializeField] private UnitTest[] unitTests;

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
                Debug.Log("Starting unit tests...");

                StartCoroutine(RunUnitTestsRoutine());
            }
        }

        private IEnumerator RunUnitTestsRoutine()
        {
            for (int i = 0; i < unitTests.Length; i++)
            {
                yield return RunUnitTest(unitTests[i]);
            }
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

            Debug.Log(testSuccessful
                ? $"Unit Test [{unitTest.Name}]: <color=green>Success</color>. ({unitTest.Description})"
                : $"Unit Test {unitTest.Name}: <color=red>[Fail]</color>. See logs for details. ({unitTest.Description})");
        }
    }
}