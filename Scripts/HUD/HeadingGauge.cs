using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class HeadingGauge : MonoBehaviour
    {
        [SerializeField] private Transform Drone;
        public delegate void HeadingUpdateHandler(float Heading);
        public event HeadingUpdateHandler OnHeadingUpdate;

        void Update()
        {
            float yaw = Drone.eulerAngles.y;
            Debug.Log(yaw);
            NotifyObservers(yaw);
        }

        public void Subscribe(HeadingUpdateHandler observer)
        {
            OnHeadingUpdate += observer;
        }

        public void Unsubscribe(HeadingUpdateHandler observer)
        {
            OnHeadingUpdate -= observer;
        }

        private void NotifyObservers(float yaw)
        {
            OnHeadingUpdate?.Invoke(yaw);
        }
    }

}