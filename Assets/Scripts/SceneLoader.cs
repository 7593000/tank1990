using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Tank1990
{
    public class SceneLoader : MonoBehaviour
    {
        public static event Action<bool> OnLoadScene;

        public void LoadSceneAsync(string sceneName)
        {
            
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }


        private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
           
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

         
            while (!asyncOperation.isDone)
            {
                OnLoadScene?.Invoke(true);
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                Debug.Log("Прогресс загрузки сцены " + sceneName + ": " + (progress * 100) + "%");
                if(progress * 100 == 100)
                {
                    OnLoadScene?.Invoke(false);
                    Debug.Log("Сцена " + sceneName + " загружена полностью.");
                }
                yield return null;  
            }

          
           

        }
    }
}