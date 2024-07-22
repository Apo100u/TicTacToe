using System.IO;
using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor
{
    public class AssetBundleBuilder : EditorWindow
    {
        private static readonly string BuildPath = Application.streamingAssetsPath;
        
        private string assetBundleName;
        private Texture2D spriteForSymbolX;
        private Texture2D spriteForSymbolO;
        private Texture2D spriteForBackground;

        [MenuItem("TicTacToe/Asset Bundle Builder")]
        public static void ShowWindow()
        {
            GetWindow(typeof(AssetBundleBuilder));
        }

        private void OnGUI()
        {
            assetBundleName = EditorGUILayout.TextField("Asset bundle name", assetBundleName);
            spriteForSymbolX = (Texture2D)EditorGUILayout.ObjectField("Sprite for symbol X", spriteForSymbolX, typeof(Texture2D), false);
            spriteForSymbolO = (Texture2D)EditorGUILayout.ObjectField("Sprite for symbol O", spriteForSymbolO, typeof(Texture2D), false);
            spriteForBackground = (Texture2D)EditorGUILayout.ObjectField("Sprite for background", spriteForBackground, typeof(Texture2D), false);

            if (GUILayout.Button("Build asset bundle"))
            {
                if (string.IsNullOrEmpty(assetBundleName))
                {
                    Debug.Log("Please assign asset bundle name before building.");
                }
                else
                {
                    Debug.Log("Building asset bundle.");
                    BuildAssetBundle();
                }
            }
        }

        private void BuildAssetBundle()
        {
            if (!Directory.Exists(BuildPath))
            {
                Directory.CreateDirectory(BuildPath);
            }

            string[] assetsPaths =
            {
                AssetDatabase.GetAssetPath(spriteForSymbolX),
                AssetDatabase.GetAssetPath(spriteForSymbolO),
                AssetDatabase.GetAssetPath(spriteForBackground)
            };

            AssetBundleBuild[] abBuilds =
            {
                new()
                {
                    assetBundleName = assetBundleName,
                    assetNames = assetsPaths
                }
            };

            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(BuildPath, abBuilds, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
            AssetDatabase.Refresh();

            string[] builtAssetBundles = manifest.GetAllAssetBundles();
            
            for (int i = 0; i < builtAssetBundles.Length; i++)
            {
                Debug.Log($"Successfully built asset bundle {builtAssetBundles[i]}.");
            }
        }
    }
}
