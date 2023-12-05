using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform cam;
        [SerializeField] private Transform obj;
        [SerializeField] private float lerpspeed = 5f;
        [SerializeField] private float ShiftX = 0.0f;
        [SerializeField] private float ShiftY = 0.0f;
        [SerializeField] private float ShiftZ = 0.0f;

        void FixedUpdate()
        {
            if (obj != null && cam != null)
            {
                if (obj != null && cam != null)
                {
                    // Obtener la posición del avión en el mismo plano horizontal que la cámara
                    Vector3 targetPosition = new Vector3(obj.position.x, obj.position.y, obj.position.z);

                    // Hacer que la cámara mire hacia la posición del avión
                    transform.LookAt(targetPosition);

                    // Calcula la posición deseada basada en la posición y orientación del objeto
                    Vector3 posicionDeseada = obj.position + new Vector3(ShiftX, ShiftY, ShiftZ);

                    // Asigna directamente la posición deseada a la cámara
                    cam.position = posicionDeseada;
                }
            }
        }
        
    }
}