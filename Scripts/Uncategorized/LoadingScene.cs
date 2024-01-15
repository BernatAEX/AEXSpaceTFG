using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BernatAEX
{
    public class LoadingScene : MonoBehaviour
    {
        public GameObject LoadingScreen;
        public Image LoadingBarFill;
        public GameObject Menu;

        public void LoadScene(int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        IEnumerator LoadSceneAsync(int sceneId)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

            LoadingScreen.SetActive(true);
            Menu.SetActive(false);

            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
                LoadingBarFill.fillAmount = progressValue;

                yield return null;
            }
        }
    }
}