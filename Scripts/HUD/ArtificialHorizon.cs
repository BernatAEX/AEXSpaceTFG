using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class ArtificialHorizon : MonoBehaviour
    {
        [SerializeField] private Transform Drone;
        public delegate void HorizonUpdateHandler(Vector2 RollPitch);
        public event HorizonUpdateHandler OnHorizonUpdate;

        void Update()
        {
            Vector2 RollPitch = new Vector2(Drone.eulerAngles.x, Drone.eulerAngles.z);
            NotifyObservers(RollPitch);
        }

        public void Subscribe(HorizonUpdateHandler observer)
        {
            OnHorizonUpdate += observer;
        }

        public void Unsubscribe(HorizonUpdateHandler observer)
        {
            OnHorizonUpdate -= observer;
        }

        private void NotifyObservers(Vector2 RollPitch)
        {
            OnHorizonUpdate?.Invoke(RollPitch);
        }
    }
}
