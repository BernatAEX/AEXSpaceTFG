using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BernatAEX
{
    [RequireComponent(typeof(SpeedGauges))]
    [RequireComponent(typeof(HeadingGauge))]
    [RequireComponent(typeof(AltitudeIndicator))]
    [RequireComponent(typeof(ArtificialHorizon))]
    public class HUDManagerDM : MonoBehaviour
    {
        [Header("Speed Gauge")]
        [SerializeField] private SpeedGauges speedGauges;
        [SerializeField] private RectTransform needleSpeed;
        private float maxSpeed = 160.0f;
        private float minSpeedArrowAngle = -90.0f;
        private float maxSpeedArrowAngle = -410.0f;

        [Header("Vertical Speed Gauge")]
        [SerializeField] private RectTransform needleVSpeed;
        private float maxVSpeed = 80.0f; //it is 40 but we make all values positive, so if the range is +-40, it is 80.
        private float minVSpeedArrowAngle = 170.0f;
        private float maxVSpeedArrowAngle = -170.0f;

        [Header("Heading Gauge")]
        [SerializeField] private HeadingGauge headingGauge;
        [SerializeField] private TextMeshProUGUI HdngLabel;

        [Header("Altitude Gauge")]
        [SerializeField] private AltitudeIndicator altitudeIndicator;
        [SerializeField] private TextMeshProUGUI AltLabel;

        [Header("Artifitial Horizon")]
        [SerializeField] private ArtificialHorizon artificialHorizon;
        [SerializeField] private RectTransform horizonBall;

        void Start()
        {
            // Subsribe to methods when starting the object
            //speedGauge.Subscribe(UpdateHorizontalSpeed);
            speedGauges.Subscribe(UpdateSpeed);
            headingGauge.Subscribe(UpdateHeading);
            altitudeIndicator.Subscribe(UpdateAltitude);
            artificialHorizon.Subscribe(UpdateHorizon);
        }

        void OnDestroy()
        {
            // Unsubscribe to methods when destroying the object
            //speedGauge.Unsubscribe(UpdateHorizontalSpeed);
            speedGauges.Unsubscribe(UpdateSpeed);
            headingGauge.Unsubscribe(UpdateHeading);
            altitudeIndicator.Unsubscribe(UpdateAltitude);
            artificialHorizon.Unsubscribe(UpdateHorizon);
        }

        void UpdateSpeed(Vector2 Speed)
        {
            needleSpeed.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, Speed.x / maxSpeed));
            needleVSpeed.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minVSpeedArrowAngle, maxVSpeedArrowAngle, Speed.y / maxVSpeed));
        }

        void UpdateHeading(float yaw)
        {
            if (HdngLabel != null)
            {
                HdngLabel.text = ((int)yaw) + "º";
            }
        }

        void UpdateAltitude(float alt)
        {
            if (AltLabel != null)
            {
                AltLabel.text = ((int)(alt * (1.38f * 95) / 37)) + " m";
            }
        }

        void UpdateHorizon(Vector2 RollPitch)
        {
            float roll = RollPitch.x;
            float pitch = RollPitch.y;
            float value;
            if (pitch >= 335.0f && pitch <= 360.0f)
            {
                value = -(360.0f - pitch);
            }
            else if (pitch >= 25.0f && pitch <= 180.0f)
            {
                value = 25.0f;
            }
            else if (pitch < 335.0f && pitch >= 180.0f)
            {
                value = -25.0f;
            }
            else
            {
                value = pitch;
            }
            horizonBall.localEulerAngles = new Vector3(0, 0, roll);
            horizonBall.localPosition = new Vector3(0, -0.826f * value + 0.13f, 0);
        }
    }
}
