using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace BernatAEX
{

    public class PauseManager : MonoBehaviour
    {
        [Header("UI Pages")]
        public GameObject pauseMenu;
        public GameObject options;

        [Header("Buttons")]
        public Button returnButton;
        public Button optionsButton;
        public Button menuButton;
        public Button quitButton;
        public Button BackBtn;

        [Header("Pause Handler")]
        public PauseEvent gamePause;


        void Start()
        {
            EnablePauseMenu();
            returnButton.onClick.AddListener(returnMenu);
            optionsButton.onClick.AddListener(OptionsMenu);
            menuButton.onClick.AddListener(ReturnMainMenu);
            quitButton.onClick.AddListener(QuitGame);
        }

        public void EnablePauseMenu()
        {
            pauseMenu.SetActive(true);
            options.SetActive(false);
        }

        public void returnMenu()
        {
            gamePause.ResumeGame();
        }

        public void OptionsMenu()
        {
            pauseMenu.SetActive(false);
            options.SetActive(true);
            BackBtn.onClick.AddListener(EnablePauseMenu);
        }

        public void ReturnMainMenu()
        {
            SceneTransitionManager.singleton.GoToSceneAsync(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
