using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    [RequireComponent(typeof(BoxCollider))]
    public class DroneEngineSimulation : MonoBehaviour, IEngineSimulation
    {
        #region Variables
        [Header("Engine Properties")]
        private float maxThrust = 600.0f; //Thrust total del drone

        [Header("Propeller Properties")]
        [SerializeField] private Transform propeller;
        [SerializeField] private float rotationSpeed = 300f;
        #endregion

        #region Interface Methods
        public void InitEngine()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEngine(Rigidbody rb, DroneInputsSimulation input)
        {
            Vector3 upVec = transform.up;
            upVec.x = 0f;
            upVec.z = 0f;
            float difference = 1 - upVec.magnitude;
            float differencefinal = Physics.gravity.magnitude * difference;

            Vector3 engineThrust = Vector3.zero;
            engineThrust = transform.up * ((rb.mass * Physics.gravity.magnitude + differencefinal) + ((input.ThrottleUp-input.ThrottleDown) * maxThrust)) / 4f;
            //Debug.Log((input.ThrottleUp - input.ThrottleDown));

            rb.AddForce(-engineThrust, ForceMode.Force);

            // Maintain drone's height
            /*Vector3 engineThrust = Vector3.zero;
            engineThrust = transform.up * ((rb.mass * Physics.gravity.magnitude));
            rb.AddForce(-engineThrust, ForceMode.Force);*/
            HandlePropellers();
        }

        void HandlePropellers()
        {
            if (!propeller)
            {
                return;
            }

            propeller.Rotate(Vector3.up, rotationSpeed);
        }
        #endregion
    }
}
