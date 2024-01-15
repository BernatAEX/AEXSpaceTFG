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

namespace BernatAEX {
    public class DetectionListenerMQTT : MonoBehaviour
    {
        [Header("MQTT")]
        MqttClient client;
        public string brokerAddress = "172.21.71.16";
        public int brokerPort = 1883;
        public string topic = "Detections/";

        [Header("Types of detection")]
        [SerializeField] GameObject Person;
        [SerializeField] GameObject Vehicle;
        [SerializeField] GameObject Warning;
        [SerializeField] GameObject Hazard;
        [SerializeField] GameObject Other;

        [Header("Other")]
        private AbstractMap map;
        public PositionListenerMQTT PosListener;

        Vector2d LatLong;
        int type;
        Vector3 oldPosition;
        float tileScale;
        float initialHeight;
        Vector3 hazardOffset = new Vector3(0.0f, 36.0f, 0.0f);

        // Start is called before the first frame update
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
                ParseData(dataReceived, out LatLong, out type);
            }
        }

        public static void ParseData(string dataString, out Vector2d LatLong, out int type)
        {
            //Debug.Log(dataString);
            // Split the elements into an array
            string[] stringArray = dataString.Split(' ');

            // Store as a Vector2d
            LatLong = new Vector2d(
                float.Parse(stringArray[0]),
                float.Parse(stringArray[1]));

            type = int.Parse(stringArray[2]);

            //Debug.Log(LatLong);
        }

        // Update is called once per frame
        void Update()
        {

            initialHeight = Get_Ground_Height(PosListener.formerLatLong);
            int tileZoom = (int)Math.Round(map.Zoom);
            tileScale = map.WorldRelativeScale;
            float height = Get_Ground_Height(LatLong);
            float unityHeight = (height - initialHeight) * (37 / (95 * 1.38f));

            Vector2 unityPosition = DistanceBetweenWGS84Points(PosListener.formerLatLong, LatLong);
            Vector3 actualPosition = new Vector3((unityPosition.x + PosListener.formerPosition.x), (unityHeight + 1), (unityPosition.y + PosListener.formerPosition.z));

            if (actualPosition != oldPosition)
            {
                //Debug.Log(actualPosition);
                switch (type)
                {
                    case 0:
                        Instantiate(Person, actualPosition, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(Vehicle, actualPosition, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Warning, (actualPosition + hazardOffset), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Hazard, (actualPosition + hazardOffset), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(Other, actualPosition, Quaternion.identity);
                        break;
                    default:
                        Instantiate(Other, actualPosition, Quaternion.identity);
                        break;
                }
                oldPosition = actualPosition;
                return;
            }
            else
            {
                return;
            }
        }

        private float Get_Ground_Height(Vector2d savedLocation2d)
        {
            float elevation = map.QueryElevationInMetersAt(savedLocation2d);
            
            return elevation;
        }

        private Vector2 DistanceBetweenWGS84Points(Vector2d point1, Vector2d point2)
        {
            int signLong = 1;
            int signLat = 1;
            float latitudeMeterMultiplier = (float)CalculateLatitudeMeterMultiplier(point2.y);

            // Convertir coordenadas de grados a radianes
            float latitude1Rad = (float)point1.y * Mathf.Deg2Rad;
            float latitude2Rad = (float)point2.y * Mathf.Deg2Rad;
            float deltaLongitudeRad = ((float)point2.x - (float)point1.x) * Mathf.Deg2Rad;

            double horizontalDistance;

            if (point2.y < 0.0f)
            {
                signLong = -1;
            }

            if (point2.x < 0.0f)
            {
                signLat = -1;
            }

            if (deltaLongitudeRad > 0.0175)
            {
                double a = Mathf.Sin((float)((latitude2Rad - latitude1Rad) / 2)) * Mathf.Sin((float)((latitude2Rad - latitude1Rad) / 2)) +
                       Mathf.Cos((float)(latitude1Rad)) * Mathf.Cos((float)(latitude2Rad)) *
                       Mathf.Sin((float)(deltaLongitudeRad / 2)) * Mathf.Sin((float)(deltaLongitudeRad / 2));
                double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt(1 - (float)a));
                horizontalDistance = signLong * 6371000 * c;
            }
            else
            {
                horizontalDistance = signLong * (point2.x - point1.x) * Math.Cos((latitude1Rad + latitude2Rad) / 2.0f) * latitudeMeterMultiplier;
            }

            double verticalDistance = signLat * Mathf.Abs((float)(point2.y - point1.y)) * latitudeMeterMultiplier;

            if ((point2.y - point1.y) < 0)
            {
                verticalDistance = -verticalDistance;
            }

            if ((point2.x - point1.x) < 0)
            {
                horizontalDistance = -horizontalDistance;
            }


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
