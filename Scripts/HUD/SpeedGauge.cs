using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class SpeedGauge : MonoBehaviour
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

            float speedX = rb.velocity.x;
            float speedZ = rb.velocity.z;

            float horizontalSpeed = Mathf.Sqrt((speedX * speedX + speedZ * speedZ));
            NotifyObservers(horizontalSpeed* 3.6f);
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
