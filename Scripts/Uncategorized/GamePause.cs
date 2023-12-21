using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BernatAEX
{
    public class GamePause : MonoBehaviour
    {
        [SerializeField] private InputActionReference PauseButton;
        [SerializeField] private GameObject pauseMenu;
        private bool paused = false;

        private void Start()
        {
            PauseButton.action.performed += Pause;
        }

        private void Pause(InputAction.CallbackContext context)
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = 1.0f;
                pauseMenu.SetActive(false);

            }
            else
            {
                paused = true;
                Time.timeScale = 0.0f;
                pauseMenu.SetActive(true);
                //Posar un menu de pausa al hud.
            }
        }
    }
}
