using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    [RequireComponent(typeof(BoxCollider))]
    public class DroneEngine : MonoBehaviour, IEngine
    {
        #region Variables
        [Header("Engine Properties")]
        private float maxThrust = 470.8f; //Thrust total del drone

        [Header("Propeller Properties")]
        [SerializeField] private Transform propeller;
        [SerializeField] private float rotationSpeed = 300f;
        #endregion
        #region Interface Methods
        public void InitEngine()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEngine(Rigidbody rb, DroneInputs input)
        {
            Vector3 upVec = transform.up;
            upVec.x = 0f;
            upVec.z = 0f;
            float difference = 1 - upVec.magnitude;
            float differencefinal = Physics.gravity.magnitude * difference;
            
            Vector3 engineThrust = Vector3.zero;
            engineThrust = transform.up * ((rb.mass * Physics.gravity.magnitude - differencefinal) + (input.LeftController.y * maxThrust)) / 4f;

            rb.AddForce(-engineThrust, ForceMode.Force);
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
