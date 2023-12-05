using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BernatAEX
{
    [RequireComponent(typeof(PlayerInput))]
    public class DroneInputsSimulation : MonoBehaviour
    {

        #region Variables

        //LOCAL VARIABLES
        private float yaw;
        private float pitch;
        private float roll;

        private float throttleup;
        private float throttledown;
        private Vector3 devicepositionR;
        private float calibrated;
        private float cameraswitcher;
        private float handswap;
        private Vector2 armR;
        private Vector2 armL;
        private float pause;

        // PUBLIC VARIABLES
        public float Yaw { get => yaw; }
        public float Pitch { get => pitch; }
        public float Roll { get => roll; }
        public float YawLeft { get => yaw; }
        public float PitchLeft { get => pitch; }
        public float RollLeft { get => roll; }

        public float ThrottleUp { get => throttleup; }
        public float ThrottleDown { get => throttledown; }
        public Vector3 DevicePositionRight { get => devicepositionR; }
        public float Calibrated { get => calibrated; set => calibrated = value; }
        public float CameraSwitcher { get => cameraswitcher; }
        public float HandSwap { get => HandSwap; }
        public Vector2 ArmDisarmR { get => armR; }
        public Vector2 ArmDisarmL { get => armL; }
        public float Pause { get => pause; }

        // CALIBRATION VARIABLES
        private Vector3 origin;
        private float YawOG;
        private float PitchOG;
        private float RollOG;

        private float initialvalue;
        private bool rightActive = true;

        #endregion

        #region Main Methods
        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion

        #region Methods
        private void OnYaw(InputValue value)
        {
            if (rightActive)
            {
                //Rotates the yaw angle of the drone
                yaw = value.Get<float>();

                if (calibrated >= 0.5f)
                {
                    YawOG = yaw;
                }
                yaw = yaw - YawOG;

                if (yaw < 0.15f && yaw > -0.15f)
                {
                    yaw = 0;
                }
            }
        }

        private void OnRoll(InputValue value)
        {
            if (rightActive)
            {
                roll = value.Get<float>();

                if (calibrated >= 0.5f)
                {
                    RollOG = roll;
                }
                roll = roll - RollOG;

                if (roll < 0.35f && roll > -0.35f)
                {
                    roll = 0;
                }
                //Debug.Log(RollOG);
            }
        }

        private void OnPitch(InputValue value)
        {
            if (rightActive)
            {
                    //Rotates the pitch angle of the drone
                pitch = value.Get<float>();
                if (calibrated >= 0.5f)
                {
                    PitchOG = pitch;
                }

                pitch = pitch - PitchOG;
                //Debug.Log(pitch);
            }
        }

        private void OnYawLeft(InputValue value)
        {
            if (!rightActive)
            {
                //Rotates the yaw angle of the drone
                yaw = value.Get<float>();

                if (calibrated >= 0.5f)
                {
                    YawOG = yaw;
                }
                yaw = yaw - YawOG;

                if (yaw < 0.15f && yaw > -0.15f)
                {
                    yaw = 0;
                }
            }
        }

        private void OnRollLeft(InputValue value)
        {
            if (!rightActive)
            {
                roll = value.Get<float>();

                if (calibrated >= 0.5f)
                {
                    RollOG = roll;
                }
                roll = roll - RollOG;

                if (roll < 0.35f && roll > -0.35f)
                {
                    roll = 0;
                }
                //Debug.Log(RollOG);
            }
        }

        private void OnPitchLeft(InputValue value)
        {
            if (!rightActive)
            {
                //Rotates the pitch angle of the drone
                pitch = value.Get<float>();
                if (calibrated >= 0.5f)
                {
                    PitchOG = pitch;
                }

                pitch = pitch - PitchOG;
                //Debug.Log(pitch);
            }
        }

        private void OnThrottleUp(InputValue value)
        {
            //Triggers makes advance the drone forward
            throttleup = value.Get<float>();
            //Debug.Log(throttlehorizontal);
        }

        private void OnThrottleDown(InputValue value)
        {
            throttledown = value.Get<float>();
        }

        private void OnDevicePositionRight(InputValue value)
        {
            //Determines the complete position of the controller
            devicepositionR = value.Get<Vector3>();
            if (calibrated >= 0.5f)
            {
                origin = devicepositionR;
            }
            devicepositionR = devicepositionR - origin;
            //Debug.Log((devicepositionR.x - origin.x));
        }

        private void OnCalibrated(InputValue value)
        {
            calibrated = value.Get<float>();
        }

        private void OnCameraSwitcher(InputValue value)
        {
            cameraswitcher = value.Get<float>();

            //Debug.Log(cameraswitcher);
        }

        private void OnHandSwap(InputValue value)
        {
            handswap = value.Get<float>();
            if (rightActive)
            {
                rightActive = false;
            }
            else
            {
                rightActive = true;
                
            }
            //Debug.Log(rightActive);
        }

        private void OnArmDisarmR(InputValue value)
        {
            armR = value.Get<Vector2>();
        }

        private void OnArmDisarmL(InputValue value)
        {
            armL = value.Get<Vector2>();
        }

        private void OnPause(InputValue value)
        {
            pause = value.Get<float>();
        }

        #endregion
    }
}
