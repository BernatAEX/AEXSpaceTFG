using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class SpeedGauges : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        public delegate void SpeedUpdateHandler(Vector2 horizontalSpeed);
        public event SpeedUpdateHandler OnSpeedUpdate;

        void Start()
        {
            DroneBaseRigidbody droneRigidbody = FindObjectOfType<DroneBaseRigidbody>();

            if (droneRigidbody != null)
            {
                rb = droneRigidbody.GetComponent<Rigidbody>();
                if (rb)
                {
                    Debug.Log("Rigidbody found");
                }
            }
            else
            {
                Debug.Log("DroneBaseRigidbody not found");
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
            float speedY = rb.velocity.y;
            float speedZ = rb.velocity.z;

            float horizontalSpeed = Mathf.Sqrt((speedX * speedX + speedZ * speedZ));
            float verticalspeed = speedY + 40; //We add 40 m/s to have capability to add negative numbers
            Vector2 speeds = new Vector2(horizontalSpeed, verticalspeed);
            NotifyObservers(speeds);
        }

        public void Subscribe(SpeedUpdateHandler observer)
        {
            OnSpeedUpdate += observer;
        }

        public void Unsubscribe(SpeedUpdateHandler observer)
        {
            OnSpeedUpdate -= observer;
        }

        private void NotifyObservers(Vector2 speed)
        {
            OnSpeedUpdate?.Invoke(speed);
        }   
    }
}
