using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BernatAEX
{
    public interface IEngineSimulation
    {
        void InitEngine();
        void UpdateEngine(Rigidbody rb, DroneInputsSimulation input);
    }
}
