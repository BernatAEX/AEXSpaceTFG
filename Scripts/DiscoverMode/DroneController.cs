using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BernatAEX
{
    [RequireComponent(typeof(DroneInputs))]
    public class DroneController : DroneBaseRigidbody
    {
        #region Variables
        [Header("Drone Properties")]
        [SerializeField] private float MinMaxPitch = 30f;
        [SerializeField] private float MinMaxRoll = 30f;
        [SerializeField] private float YawPower = 4f;
        [SerializeField] private float lerpSpeed = 2f;

        private DroneInputs input;
        private List<IEngine> engines = new List<IEngine>();

        private float finalPitch;
        private float finalRoll;
        private float yaw;
        private float finalYaw;

        #endregion

        #region Methods
        void Start()
        {
            input = GetComponent<DroneInputs>();
            engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
        }

        #endregion

        #region OwnMethods
        protected override void CalcPhysics()
        {
            HandleEngines();
            HandleControls();
        }

        protected virtual void HandleEngines()
        {
            foreach(IEngine engine in engines)
            {
                engine.UpdateEngine(rb, input);
            }
        }

        protected virtual void HandleControls()
        {
            float pitch = input.RightController.y * MinMaxPitch;
            float roll = -input.RightController.x * MinMaxRoll;
            yaw += input.LeftController.x * YawPower;

            finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * lerpSpeed);
            finalRoll = Mathf.Lerp(finalRoll, roll, Time.deltaTime * lerpSpeed);
            finalYaw = Mathf.Lerp(finalYaw, yaw, Time.deltaTime * lerpSpeed);

            Quaternion rotation = Quaternion.Euler(finalPitch, finalYaw, finalRoll);

            rb.MoveRotation(rotation); //ARCADE
            //MIRAR D'AFEGIR TORQUE PER EL SIMULATION MODE
        }
        #endregion
    }
}
