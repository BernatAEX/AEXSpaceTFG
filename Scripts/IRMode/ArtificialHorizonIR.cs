using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class ArtificialHorizonIR : MonoBehaviour
    {
        public PositionListenerMQTT reception;
        public delegate void AngleUpdateHandler(Vector2 horizonAngles);
        public event AngleUpdateHandler OnAngleUpdate;
        private Vector2 RollPitch;

        void Update()
        {
            RollPitch = new Vector2(reception.Roll, reception.Pitch);
            NotifyObservers(RollPitch);
        }

        public void Subscribe(AngleUpdateHandler observer)
        {
            OnAngleUpdate += observer;
        }

        public void Unsubscribe(AngleUpdateHandler observer)
        {
            OnAngleUpdate -= observer;
        }

        private void NotifyObservers(Vector2 RollPitch)
        {
            OnAngleUpdate?.Invoke(RollPitch);
        }
    }
}
