using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BernatAEX
{
    [RequireComponent(typeof(DroneInputsSimulation))]
    public class DroneControllerSimulation : DroneBaseRigidbodySimulation
    {
        #region Variables
        [Header("Drone Properties")]
        [SerializeField] private float MinMaxPitch = 50f;
        [SerializeField] private float MinMaxRoll = 30f;
        [SerializeField] private float YawPower = 4f;
        [SerializeField] private float lerpSpeed = 2f;

        private DroneInputsSimulation input;
        private List<IEngineSimulation> engines = new List<IEngineSimulation>();

        private float finalPitch;
        private float finalRoll;
        private float yaw;
        private float finalYaw;

        // Arm&Disarm Parameters
        private bool Armed = false;
        private int i = 0; //counter for arming the drone
        private int j = 0; //counter for disarming the drone

        #endregion

        #region Methods
        void Start()
        {
            input = GetComponent<DroneInputsSimulation>();
            engines = GetComponentsInChildren<IEngineSimulation>().ToList<IEngineSimulation>();
        }

        #endregion

        #region OwnMethods
        protected override void CalcPhysics()
        {
            GetArmedStatus();
            if (Armed)
            {
                HandleEngines();
                HandleControls();
            }
        }

        protected virtual void HandleEngines()
        {
            //Vector3 altitudeHold = Vector3.zero;
            //altitudeHold = transform.up * ((rb.mass * Physics.gravity.magnitude));
            //rb.AddForce(altitudeHold, ForceMode.Force);
            foreach (IEngineSimulation engine in engines)
            {
                engine.UpdateEngine(rb, input);
            }

        }

        protected virtual void HandleControls()
        {
            float pitch = 8 * input.Pitch * MinMaxPitch;
            float roll = -3 * input.Roll * MinMaxRoll;
            yaw += (-input.Yaw) * YawPower;
           

            finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * lerpSpeed);
            finalRoll = Mathf.Lerp(finalRoll, roll, Time.deltaTime * lerpSpeed);
            finalYaw = Mathf.Lerp(finalYaw, yaw, Time.deltaTime * lerpSpeed);

            Quaternion rotation = Quaternion.Euler(finalPitch, finalYaw, finalRoll);

            rb.MoveRotation(rotation); //ARCADE
            //MIRAR D'AFEGIR TORQUE PER EL SIMULATION MODE*/
        }

        private void GetArmedStatus()
        {
            
            if(input.ArmDisarmL.y > 0.75f && input.ArmDisarmR.y > 0.75f)
            {
                i++;
            }
            else if (input.ArmDisarmL.y < -0.75f && input.ArmDisarmR.y < -0.75f)
            {
                j++;
            }
            else
            {
                i = 0;
                j = 0;
            }

            if (i >= 60)
            {
                Armed = true;
                input.Calibrated = 1.0f;
                //Afegir aqui un element al HUD que t'avisi que estas armat
            }
            else if (j >= 60)
            {
                Armed = false;
                //Afegir aqui un element al HUD que t'avisi que estas desarmat
            }
            else
            {
                input.Calibrated = 0.0f;
                return;
            }
        }
        #endregion
    }
}
