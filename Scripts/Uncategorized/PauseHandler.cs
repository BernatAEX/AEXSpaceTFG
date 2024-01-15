using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BernatAEX
{
    public class PauseHandler : MonoBehaviour
    {
        [Header("Visuals")]
        public GameObject FirstHUD;
        public GameObject FirstPauseCanvas;
        public CameraSwitcher switcher;
        public GameObject LeftController;
        public GameObject RightController;

        [Header("Inputs")]
        [SerializeField] private InputActionReference PauseButton;
        [SerializeField] private InputActionReference ReturnButton;
        [SerializeField] private InputActionReference MainMenuButton;

        bool isPaused = false;
        bool Switched = false;

        private void Start()
        {
            PauseButton.action.performed += Pause;
            ReturnButton.action.performed += Resume;
            MainMenuButton.action.performed += MainMenuGame;
        }

        private void Pause(InputAction.CallbackContext context)
        {
            Time.timeScale = (isPaused) ? 1.0f : 0.0f;
            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
                MainMenuButton.action.performed += MainMenuGame;
            }

            if (switcher.FPCameraActive)
            {
                FirstHUD.SetActive(!isPaused);
                FirstPauseCanvas.SetActive(isPaused);
                switcher.Available = !isPaused;
                LeftController.SetActive(isPaused);
                RightController.SetActive(isPaused);
            }
            else
            {
                switcher.ExternalSwitch();
                FirstHUD.SetActive(!isPaused);
                FirstPauseCanvas.SetActive(isPaused);
                switcher.Available = !isPaused;
                Switched = true;
                LeftController.SetActive(isPaused);
                RightController.SetActive(isPaused);
            }

            if (Switched)
            {
                switcher.ExternalSwitch();
            }
        }

        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            if (switcher.FPCameraActive)
            {
                FirstHUD.SetActive(!isPaused);
                FirstPauseCanvas.SetActive(isPaused);
                switcher.Available = isPaused;
                LeftController.SetActive(isPaused);
                RightController.SetActive(isPaused);
            }
            else
            {
                switcher.ExternalSwitch();
                FirstHUD.SetActive(!isPaused);
                FirstPauseCanvas.SetActive(isPaused);
                switcher.Available = !isPaused;
                Switched = true;
                LeftController.SetActive(isPaused);
                RightController.SetActive(isPaused);
            }
        }

        public void Resume(InputAction.CallbackContext context)
        {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1.0f;
                if (switcher.FPCameraActive)
                {
                    FirstHUD.SetActive(!isPaused);
                    FirstPauseCanvas.SetActive(isPaused);
                    switcher.Available = isPaused;
                    LeftController.SetActive(isPaused);
                    RightController.SetActive(isPaused);
                }
                else
                {
                    switcher.ExternalSwitch();
                    FirstHUD.SetActive(!isPaused);
                    FirstPauseCanvas.SetActive(isPaused);
                    switcher.Available = !isPaused;
                    Switched = true;
                    LeftController.SetActive(isPaused);
                    RightController.SetActive(isPaused);
                }
            }
        }


        public void MainMenuGame(InputAction.CallbackContext context)
        {
            if (isPaused)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}