using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using TicTacToe.ScriptableObjects.HelperStructs;
using UnityEngine;

namespace TicTacToe.GameBase
{
    public class Reskinner : MonoBehaviour
    {
        [SerializeField] private string backgroundAssetName;
        [SerializeField] private string xSymbolAssetName;
        [SerializeField] private string oSymbolAssetName;
        
        [field: SerializeField] public Visuals CurrentVisuals { get; private set; }

        private byte[] assetBundleFileBytes;
        private AssetBundle assetBundle;
        
        public void Init(Visuals defaultVisuals)
        {
            CurrentVisuals = defaultVisuals;
        }

        public void ReskinFromAssetBundle(string assetBundleName)
        {
            string assetBundlePath = Path.Combine(Application.streamingAssetsPath, assetBundleName);

            if (File.Exists(assetBundlePath))
            {
                StartCoroutine(ReskinFromAssetBundleAsync(assetBundlePath));
            }
            else
            {
                Debug.Log("Couldn't find file with requested asset bundle.");
            }
        }

        private IEnumerator ReskinFromAssetBundleAsync(string assetBundlePath)
        {
            yield return ReadBundleFile(assetBundlePath);
            yield return LoadAssetBundleFromBytes(assetBundleFileBytes);

            yield return LoadSpriteFromBundle(assetBundle, backgroundAssetName, sprite =>
            {
                Visuals updatedVisuals = CurrentVisuals;
                updatedVisuals.Background = sprite;
                CurrentVisuals = updatedVisuals;
            });
            
            yield return LoadSpriteFromBundle(assetBundle, xSymbolAssetName, sprite =>
            {
                Visuals updatedVisuals = CurrentVisuals;
                updatedVisuals.SymbolX = sprite;
                CurrentVisuals = updatedVisuals;
            });
            
            yield return LoadSpriteFromBundle(assetBundle, oSymbolAssetName, sprite =>
            {
                Visuals updatedVisuals = CurrentVisuals;
                updatedVisuals.SymbolO = sprite;
                CurrentVisuals = updatedVisuals;
            });

            assetBundle.UnloadAsync(false);
        }

        private IEnumerator ReadBundleFile(string assetBundlePath)
        {
            Task<byte[]> assetBundleBytesReadTask = File.ReadAllBytesAsync(assetBundlePath);

            while (!assetBundleBytesReadTask.IsCompleted)
            {
                yield return null;
            }

            assetBundleFileBytes = assetBundleBytesReadTask.Result;
        }

        private IEnumerator LoadAssetBundleFromBytes(byte[] bytes)
        {
            AssetBundleCreateRequest assetBundleLoad = AssetBundle.LoadFromMemoryAsync(bytes);

            while (!assetBundleLoad.isDone)
            {
                yield return null;
            }

            assetBundle = assetBundleLoad.assetBundle;
        }


        private IEnumerator LoadSpriteFromBundle(AssetBundle assetBundle, string spriteName, Action<Sprite> onSpriteLoaded)
        {
            AssetBundleRequest request = assetBundle.LoadAssetAsync(spriteName);

            while (!request.isDone)
            {
                yield return null;
            }

            Sprite loadedSprite = CreateSpriteFromTexture2D((Texture2D) request.asset);
            onSpriteLoaded?.Invoke(loadedSprite);
        }

        private Sprite CreateSpriteFromTexture2D(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}
