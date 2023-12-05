using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class VSpeedGauge : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        public delegate void HorizontalSpeedUpdateHandler(float horizontalSpeed);
        public event HorizontalSpeedUpdateHandler OnHorizontalSpeedUpdate;

        void Awake()
        {
            rb = FindObjectOfType<DroneBaseRigidbody>().GetComponent<Rigidbody>();
            if (rb)
            {
                Debug.Log("Rigidbody found");
            }
        }

        void Update()
        {
            if (!rb)
            {
                Debug.Log("Rb not found");
                return;
            }

            float speedY = rb.velocity.y;

            float verticalspeed = speedY + 40; //We add 40 m/s to have capability to add negative numbers
            NotifyObservers(verticalspeed);
        }

        public void Subscribe(HorizontalSpeedUpdateHandler observer)
        {
            OnHorizontalSpeedUpdate += observer;
        }

        public void Unsubscribe(HorizontalSpeedUpdateHandler observer)
        {
            OnHorizontalSpeedUpdate -= observer;
        }

        private void NotifyObservers(float speed)
        {
            OnHorizontalSpeedUpdate?.Invoke(speed);
        }
    }
}
