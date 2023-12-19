using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class ActiveHand : MonoBehaviour
    {
        [SerializeField] private DroneInputsSimulation inputs;
        public delegate void HandUpdateHandler(bool hand);
        public event HandUpdateHandler OnHandUpdate;

        void Update()
        {
            NotifyObservers(inputs.rightActive);
        }

        public void Subscribe(HandUpdateHandler observer)
        {
            OnHandUpdate += observer;
        }

        public void Unsubscribe(HandUpdateHandler observer)
        {
            OnHandUpdate -= observer;
        }

        private void NotifyObservers(bool hand)
        {
            OnHandUpdate?.Invoke(hand);
        }
    }
}
