using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BernatAEX
{
    [RequireComponent(typeof(PlayerInput))]
    public class DroneInputs : MonoBehaviour
    {
        #region Variables

        private Vector2 leftcontroller;
        private Vector2 rightcontroller;

        public Vector2 LeftController { get => leftcontroller; }
        public Vector2 RightController { get => rightcontroller; }
        #endregion

        #region Main Methods
        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion

        #region Methods
        private void OnLeftController(InputValue value)
        {
            leftcontroller = value.Get<Vector2>();
        }

        private void OnRightController(InputValue value)
        {
            rightcontroller = value.Get<Vector2>();
        }
        #endregion

    }
}