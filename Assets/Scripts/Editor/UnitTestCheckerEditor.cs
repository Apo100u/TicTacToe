using TicTacToe.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor
{
    [CustomEditor(typeof(UnitTestRunner))]
    public class UnitTestCheckerEditor : UnityEditor.Editor
    {
        private UnitTestRunner unitTestRunner;
        
        private void OnEnable()
        {
            unitTestRunner = serializedObject.targetObject as UnitTestRunner;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Start Unit Tests"))
            {
                if (EditorApplication.isPlaying)
                {
                    unitTestRunner.RunUnitTests();
                }
                else
                {
                    Debug.Log($"{nameof(UnitTestRunner)}: Enter playmode to start unit tests.");
                }
            }
        }
    }
}
