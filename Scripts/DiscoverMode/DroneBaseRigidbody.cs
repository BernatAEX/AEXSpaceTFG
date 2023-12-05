using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BernatAEX
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroneBaseRigidbody : MonoBehaviour
    {
        #region Variables
        [Header("Rigidbody Properties")]
        [SerializeField] private float weight = 1f; //mass in Kg


        protected Rigidbody rb;
        protected float Drag0;
        protected float AngularDrag0;
        #endregion

        #region Methods

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (rb)
            {
                rb.mass = weight;
                Drag0 = rb.drag;
                AngularDrag0 = rb.angularDrag;
            }
        }

        void FixedUpdate()
        {
            if (!rb)
            {
                return;
            }

            CalcPhysics();
        }
        #endregion

        #region OwnMethods
        protected virtual void CalcPhysics()
        {

        }

        #endregion
    }

}
