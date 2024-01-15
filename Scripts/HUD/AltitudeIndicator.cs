using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class AltitudeIndicator : MonoBehaviour
    {
        [SerializeField] private Transform Drone;
        public delegate void AltitudeUpdateHandler(float altitude);
        public event AltitudeUpdateHandler OnAltitudeUpdate;

        void Update()
        {
            float altitude = Drone.position.y;
            NotifyObservers(altitude);
        }

        public void Subscribe(AltitudeUpdateHandler observer)
        {
            OnAltitudeUpdate += observer;
        }

        public void Unsubscribe(AltitudeUpdateHandler observer)
        {
            OnAltitudeUpdate -= observer;
        }

        private void NotifyObservers(float altitude)
        {
            OnAltitudeUpdate?.Invoke(altitude);
        }
    }

}
