using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace BernatAEX
{

    public class PositionListenerMQTT : MonoBehaviour
    {
        MqttClient client;
        public string brokerAddress = "172.21.71.16";
        public int brokerPort = 1883;
        public string topic = "Aexspace/";

        private AbstractMap map;
        public GameObject drone;

        Vector3 position = Vector3.zero;
        Vector2d LatLong;
        float height;

        Vector3 formerPosition;

        Vector3 SpeedNED;

        float Heading;
        float formerHeading;

        /*public Transform playerTransform;
        public float earthRadius = 6371000.0f;*/


        void Start()
        {
            ConnectToBroker();
            map = FindObjectOfType<AbstractMap>();
        }

        void ConnectToBroker()
        {
            client = new MqttClient(brokerAddress);

            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
        }

        void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string dataReceived = Encoding.UTF8.GetString(e.Message);

            if (!string.IsNullOrEmpty(dataReceived))
            {
                ParseData(dataReceived, out LatLong, out height, out SpeedNED, out Heading);
            }
        }

        // Use-case specific function, need to re-write this to interpret whatever data is being sent
        public static void ParseData(string dataString, out Vector2d LatLong, out float height, out Vector3 velocity, out float heading)
        {
            Debug.Log(dataString);

            // Split the elements into an array
            string[] stringArray = dataString.Split(' ');

            // Store as a Vector3
            LatLong = new Vector2d(
                float.Parse(stringArray[0]),
                float.Parse(stringArray[1]));

            height = float.Parse(stringArray[2]);

            velocity = new Vector3(
                float.Parse(stringArray[3]),
                float.Parse(stringArray[4]),
                float.Parse(stringArray[5]));

            heading = float.Parse(stringArray[6]);
        }

        void Update()
        {
            int tileZoom = (int)Math.Round(map.Zoom);
            float tileScale = map.WorldRelativeScale;

            Vector2 tilePosition2D = Conversions.LatitudeLongitudeToUnityTilePosition(LatLong, tileZoom, tileScale);
            Vector3 actualPosition = new Vector3(tilePosition2D.x, (height + 1), tilePosition2D.y);

            if (actualPosition != formerPosition)
            {
                transform.position = actualPosition;
                formerPosition = actualPosition;
            }

            if (Heading != formerHeading)
            {
                // Calculate the desired yaw rotation based on the heading
                float desiredYaw = Heading; // Invert if necessary

                // Create a Quaternion representing the desired yaw rotation
                Quaternion desiredRotation = Quaternion.Euler(0, desiredYaw, 0);

                // Apply the desired rotation to the object's Transform
                transform.rotation = desiredRotation;

                // Update the previousHeading with the new heading value
                formerHeading = Heading;
            }

            Vector3 localSpeedNED = transform.InverseTransformDirection(SpeedNED);
            transform.Translate(Time.deltaTime * localSpeedNED * tileScale);
        }

        void OnDestroy()
        {
            if (client != null && client.IsConnected)
            {
                client.Disconnect();
            }
        }
    }
}
