using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BernatAEX
{
    public class PauseEvent : MonoBehaviour
    {
        public GameObject FirstHUD;
        public GameObject FirstPauseCanvas;
        public CameraSwitcher switcher;
        [SerializeField] private InputActionReference PauseButton;
        public GameObject LeftController;
        public GameObject RightController;

        bool isPaused = false;
        bool Switched = false;
        
        private void Start()
        {
            PauseButton.action.performed += Pause;
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
    }
}
