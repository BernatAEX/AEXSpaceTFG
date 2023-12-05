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
                    // Obtener la posici�n del avi�n en el mismo plano horizontal que la c�mara
                    Vector3 targetPosition = new Vector3(obj.position.x, obj.position.y, obj.position.z);

                    // Hacer que la c�mara mire hacia la posici�n del avi�n
                    transform.LookAt(targetPosition);

                    // Calcula la posici�n deseada basada en la posici�n y orientaci�n del objeto
                    Vector3 posicionDeseada = obj.position + new Vector3(ShiftX, ShiftY, ShiftZ);

                    // Asigna directamente la posici�n deseada a la c�mara
                    cam.position = posicionDeseada;
                }
            }
        }
        
    }
}