using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;



namespace Rune.Utils
{
    public struct Asset
    {
        public static IEnumerator LoadAsset<T>(string path, Action<AsyncOperationHandle<T>> callback)
        {
            if (string.IsNullOrEmpty(path)) yield break;

            if (callback == null) yield break;


            var handle = Addressables.LoadAssetAsync<T>(path);

            yield return handle;


            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                callback(handle);

                // The caller is responsible for releasing the handle using Addressables.Release(handle).
                // Do NOT release it here, as the caller may still be using the loaded asset.
            }
            else
            {
                Debug.LogError($"Failed to load asset. ({handle.OperationException})\nPath : {path}");
            }
        }

        public static IEnumerator LoadAssets<T>(string path, Action<AsyncOperationHandle<IList<T>>> callback)
        {
            if (string.IsNullOrEmpty(path)) yield break;

            if (callback == null) yield break;


            var handle = Addressables.LoadAssetsAsync<T>(path);

            yield return handle;


            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                callback(handle);

                // The caller is responsible for releasing the handle using Addressables.Release(handle).
                // Do NOT release it here, as the caller may still be using the loaded asset.
            }
            else
            {
                Debug.LogError($"Failed to load asset. ({handle.OperationException})\nPath : {path}");
            }
        }

        public static IEnumerator LoadScene(string name)
        {
            if (!Application.CanStreamedLevelBeLoaded(name))
            {
                Debug.LogError($"Scene \"{name}\" cannot be loaded. Check if it is added to Build Settings.");

                yield break;
            }

            var currentScene = SceneManager.GetActiveScene();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            while (!asyncOperation.isDone) yield return null;

            SceneManager.UnloadSceneAsync(currentScene);
        }
    
        public static bool HasSceneInBuild(string name)
        {
            int count = SceneManager.sceneCountInBuildSettings;
    
            for (int i = 0; i < count; i++)
            {
                string filePath = SceneUtility.GetScenePathByBuildIndex(i);
                string fileName = Path.GetFileNameWithoutExtension(filePath);
    
                if (fileName.Equals(name, System.StringComparison.OrdinalIgnoreCase)) return true;
            }
    
            return false;
        }
    }
}