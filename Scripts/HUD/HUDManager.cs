
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BernatAEX
{
    [RequireComponent(typeof(SpeedGauge))]
    [RequireComponent(typeof(VSpeedGauge))]
    [RequireComponent(typeof(HeadingGauge))]
    public class HUDManager : MonoBehaviour
    {
        [Header("Speed Gauge")]
        [SerializeField] private SpeedGauge speedGauge;
        [SerializeField] private RectTransform needleSpeed;
        private float maxSpeed = 160.0f;
        private float minSpeedArrowAngle = -90.0f;
        private float maxSpeedArrowAngle = -410.0f;

        [Header("Vertical Speed Gauge")]
        [SerializeField] private VSpeedGauge VspeedGauge;
        [SerializeField] private RectTransform needleVSpeed;
        private float maxVSpeed = 80.0f; //it is 40 but we make all values positive, so if the range is +-40, it is 80.
        private float minVSpeedArrowAngle = 170.0f;
        private float maxVSpeedArrowAngle = -170.0f;


        [Header("Heading Gauge")]
        [SerializeField] private HeadingGauge headingGauge;
        [SerializeField] private RectTransform HdngIndicator;
        private float maxYawSpeed = 10.0f;
        

        void Start()
        {
            // Subsribe to methods when starting the object
            speedGauge.Subscribe(UpdateHorizontalSpeed);
            VspeedGauge.Subscribe(UpdateVerticalSpeed);
            headingGauge.Subscribe(UpdateHeading);
        }

        void OnDestroy()
        {
            // Unsubscribe to methods when destroying the object
            speedGauge.Unsubscribe(UpdateHorizontalSpeed);
            VspeedGauge.Unsubscribe(UpdateVerticalSpeed);
            headingGauge.Unsubscribe(UpdateHeading);
        }

        void UpdateHorizontalSpeed(float horizontalSpeed)
        {
            needleSpeed.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, horizontalSpeed / maxSpeed));
        }

        void UpdateVerticalSpeed(float verticalSpeed)
        {
            needleVSpeed.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minVSpeedArrowAngle, maxVSpeedArrowAngle, verticalSpeed / maxVSpeed));
        }

        void UpdateHeading(float yaw)
        {
            HdngIndicator.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, 360, yaw));
        }

    }
}
