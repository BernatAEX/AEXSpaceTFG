
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BernatAEX
{
    [RequireComponent(typeof(SpeedGaugesIR))]
    [RequireComponent(typeof(HeadingGauge))]
    [RequireComponent(typeof(AltitudeIndicator))]
    public class HUDManagerIR : MonoBehaviour
    {
        [Header("Speed Gauge")]
        [SerializeField] private SpeedGaugesIR speedGaugesIR;
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


        void Start()
        {
            // Subsribe to methods when starting the object
            //speedGauge.Subscribe(UpdateHorizontalSpeed);
            speedGaugesIR.Subscribe(UpdateSpeed);
            headingGauge.Subscribe(UpdateHeading);
            altitudeIndicator.Subscribe(UpdateAltitude);
        }

        void OnDestroy()
        {
            // Unsubscribe to methods when destroying the object
            //speedGauge.Unsubscribe(UpdateHorizontalSpeed);
            speedGaugesIR.Unsubscribe(UpdateSpeed);
            headingGauge.Unsubscribe(UpdateHeading);
            altitudeIndicator.Unsubscribe(UpdateAltitude);
        }

        void UpdateSpeed(Vector2 Speed)
        {
            needleSpeed.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, Speed.x / maxSpeed));
            needleVSpeed.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minVSpeedArrowAngle, maxVSpeedArrowAngle, Speed.y / maxVSpeed));
        }

        void UpdateHeading(float yaw)
        {
            if(HdngLabel != null)
            {
                HdngLabel.text = ((int)yaw) + "º";
            }
        }

        void UpdateAltitude(float alt)
        {
            if (AltLabel != null)
            {
                AltLabel.text = ((int)alt) + " ft";
            }
        }

    }
}
