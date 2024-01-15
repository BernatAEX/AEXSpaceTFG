using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BernatAEX
{
    public class GamePause : MonoBehaviour
    {
        public GameObject drone;
        public GameObject XRigMenu;
        [SerializeField] private InputActionReference PauseButton;
        bool isPaused = false;


        private void Start()
        {
            PauseButton.action.performed += Pause;
        }

        private void Pause(InputAction.CallbackContext context)
        {
            /*if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
            Debug.Log(isPaused);

            if (drone == null || XRigMenu == null)
            {
                Debug.LogError("Either drone or XRig are unassigned.");
                return;
            }

            drone.SetActive(!drone.activeSelf);
            XRigMenu.SetActive(!XRigMenu.activeSelf);
            Debug.Log(drone.activeSelf);
            Debug.Log(XRigMenu.activeSelf); */
        }

        public void PauseGame()
        {
            //drone = GameObject.FindGameObjectWithTag("Drone");

            if (drone != null)
            {
                if (isPaused)
                {
                    Debug.LogWarning("Already paused");
                }
                else
                {
                    isPaused = true;
                    drone.SetActive(!isPaused);
                    XRigMenu.SetActive(isPaused);
                }

                Time.timeScale = (isPaused) ? 1.0f : 0.0f;

            }
            else
            {
                return;
            }
        }

        public void ResumeGame()
        {
            drone.SetActive(true);
            XRigMenu.SetActive(false);
            Debug.Log("Resumed");
        }
    }
}