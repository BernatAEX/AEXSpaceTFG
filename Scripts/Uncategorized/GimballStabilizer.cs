using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    public class GimballStabilizer : MonoBehaviour
    {
        [SerializeField] private Transform obj;
        [SerializeField] private float lerpspeed = 5.0f;
        [SerializeField] private float desiredPitch = 0.0f;

        void Update()
        {
            float pitch = obj.rotation.eulerAngles.z;

            float pitchADJ = pitch - desiredPitch;

            Quaternion desired_rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -pitchADJ);

            transform.rotation = Quaternion.Lerp(transform.rotation, desired_rotation, Time.deltaTime * lerpspeed);
        }
    }
}

