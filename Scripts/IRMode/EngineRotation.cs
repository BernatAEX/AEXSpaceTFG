using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class EngineRotation : MonoBehaviour
    {
        [Header("Propeller Properties")]
        [SerializeField] private Transform propeller;
        [SerializeField] private float rotationSpeed = 300f;

        // Update is called once per frame
        void Update()
        {
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

    }
}
