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
        [Header("MQTT")]
        MqttClient client;
        public string brokerAddress = "172.21.71.16";
        public int brokerPort = 1883;
        public string topic = "Aexspace/";

        [Header("Other")]
        private AbstractMap map;
        public GameObject drone;
        

        Vector3 position = Vector3.zero;
        Vector2d LatLong;
        [HideInInspector] public Vector2d formerLatLong;
        float height;

        [HideInInspector] public Vector3 formerPosition;
        Vector3 origin;

        Vector3 SpeedNED;
        public float Hspeed;
        public float Vspeed;
        public bool isonAir;

        float Heading;
        public float Roll;
        public float Pitch;
        float formerHeading;
        private bool firstMessage = true;
        private bool samestring;
        float tileScale;


        void Awake()
        {
            ConnectToBroker();
            map = FindObjectOfType<AbstractMap>();
            string addr = PlayerPrefs.GetString("Broker Address");
            if (addr != "")
            {
                brokerAddress = addr;
            }
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
                ParseData(dataReceived, out LatLong, out height, out SpeedNED, out Heading, out Roll, out Pitch);
            }
        }

        // Use-case specific function, need to re-write this to interpret whatever data is being sent
        public static void ParseData(string dataString, out Vector2d LatLong, out float height, out Vector3 velocity, out float heading, out float roll, out float pitch)
        {

            // Split the elements into an array
            string[] stringArray = dataString.Split(' ');

            // Store as a Vector2d
            LatLong = new Vector2d(
                float.Parse(stringArray[0]),
                float.Parse(stringArray[1]));

            height = float.Parse(stringArray[2]);

            velocity = new Vector3(
                float.Parse(stringArray[3]),
                float.Parse(stringArray[5]),
                float.Parse(stringArray[4]));

            heading = float.Parse(stringArray[6]);
            roll = float.Parse(stringArray[7]);
            pitch = float.Parse(stringArray[8]);
        }

        void Update()
        {
            Vector3 actualPosition;
            int tileZoom = (int)Math.Round(map.Zoom);
            tileScale = map.WorldRelativeScale;
            if (firstMessage)
            {
                map.UpdateMap(LatLong);
                formerLatLong = LatLong;
                firstMessage = false;
                Vector2 tilePosition2D = Conversions.LatitudeLongitudeToUnityTilePosition(LatLong, tileZoom, tileScale);
                actualPosition = new Vector3(tilePosition2D.x, (height*(37/95) + 0.25f), tilePosition2D.y);
                formerPosition = actualPosition;
                transform.position = actualPosition;

                Quaternion desiredRotation = Quaternion.Euler(-Pitch, Heading, Roll);
                transform.rotation = desiredRotation;
                formerHeading = Heading;

                Hspeed = (float)Math.Sqrt(SpeedNED.x * SpeedNED.x + SpeedNED.z * SpeedNED.z);
                Vspeed = SpeedNED.y;
                return;
            }

            Vector2 unityPosition = DistanceBetweenWGS84Points(formerLatLong, LatLong);
            actualPosition = new Vector3((unityPosition.x + formerPosition.x), (height + 1), (unityPosition.y + formerPosition.z));
            transform.position = actualPosition;

            if (Heading != formerHeading)
            {
                if (-1.0f < Roll && Roll < 1.0f)
                {
                    Roll = 0.0f;
                }
                if (-1.0f < Pitch && Pitch < 1.0f)
                {
                    Pitch = 0.0f;
                }
                // Create a Quaternion representing the desired yaw rotation
                Quaternion desiredRotation = Quaternion.Euler(-Pitch, Heading, Roll);

                // Apply the desired rotation to the object's Transform
                transform.rotation = desiredRotation;

                // Update the previousHeading with the new heading value
                formerHeading = Heading;
            }

            Hspeed = (float)Math.Sqrt(SpeedNED.x * SpeedNED.x + SpeedNED.z * SpeedNED.z);
            Vspeed = SpeedNED.y;

            if (height >= 1 || SpeedNED != Vector3.zero)
            {
                isonAir = true;
            }
            else
            {
                isonAir = false;
            }
        }

        void OnDestroy()
        {
            if (client != null && client.IsConnected)
            {
                client.Disconnect();
            }
        }


        private Vector2 DistanceBetweenWGS84Points(Vector2d point1, Vector2d point2)
        {
            int sign = 1;
            float latitudeMeterMultiplier = (float)CalculateLatitudeMeterMultiplier(point2.y);

            // Convertir coordenadas de grados a radianes
            float latitude1Rad = (float)point1.y * Mathf.Deg2Rad;
            float latitude2Rad = (float)point2.y * Mathf.Deg2Rad;
            float deltaLongitudeRad = ((float)point2.x - (float)point1.x) * Mathf.Deg2Rad;

            double horizontalDistance;

            if(point1.y < 0.0f)
            {
                sign = -1;
            }

            if (deltaLongitudeRad > 0.0175)
            {
                double a = Mathf.Sin((float)((latitude2Rad - latitude1Rad) / 2)) * Mathf.Sin((float)((latitude2Rad - latitude1Rad) / 2)) +
                       Mathf.Cos((float)(latitude1Rad)) * Mathf.Cos((float)(latitude2Rad)) *
                       Mathf.Sin((float)(deltaLongitudeRad / 2)) * Mathf.Sin((float)(deltaLongitudeRad / 2));
                double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt(1 - (float)a));
                horizontalDistance = sign * 6371000 * c;
            }
            else
            {
                horizontalDistance = sign * (point2.x - point1.x) * Math.Cos((latitude1Rad + latitude2Rad) / 2.0f) * latitudeMeterMultiplier;
            }

            // Calcular distancia vertical
            double verticalDistance = Mathf.Abs((float)(point2.y - point1.y)) * latitudeMeterMultiplier;

            if ((point2.y - point1.y) < 0)
            {
                verticalDistance = -verticalDistance;
            }

            // Retornar la distancia con ajustes
            Vector2 ret = new Vector2((float)verticalDistance, (float)horizontalDistance);
            float Ct = 0.317f;
            return (Ct * ret);
        }


        private double CalculateLatitudeMeterMultiplier(double latitude)
        {
            double metersPerDegree = 111132.92 - 559.82 * Mathf.Cos((float)(2 * latitude * Mathf.Deg2Rad)) + 1.175 * Mathf.Cos((float)(4 * latitude * Mathf.Deg2Rad));
            return metersPerDegree;
        }


    }
}
