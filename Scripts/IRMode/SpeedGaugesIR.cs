using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class SpeedGaugesIR : MonoBehaviour
    {
        public PositionListenerMQTT reception;
        public delegate void SpeedUpdateHandler(Vector2 horizontalSpeed);
        public event SpeedUpdateHandler OnSpeedUpdate;
        private Vector2 speed;

        void Update()
        {

            float verticalspeed = (-reception.Vspeed) + 40;
            speed = new Vector2(reception.Hspeed*3.6f, verticalspeed);
            NotifyObservers(speed);
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
