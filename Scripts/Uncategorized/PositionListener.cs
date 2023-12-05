using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;


public class PositionListener : MonoBehaviour
{
    Thread thread;
    public int connectionPort = 25001;
    TcpListener server;
    TcpClient client;
    bool running;

    public AbstractMap map;

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


        // Receive on a separate thread so Unity doesn't freeze waiting for data
        ThreadStart ts = new ThreadStart(GetData);
        thread = new Thread(ts);
        thread.Start();

    }

    void GetData()
    {

        // Create the server
        server = new TcpListener(IPAddress.Any, connectionPort);
        server.Start();

        // Create a client to get the data stream
        client = server.AcceptTcpClient();

        // Start listening
        running = true;
        while (running)
        {
            Connection();
        }

        CloseConnection();
    }

    void Connection()
    {

        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead;
        try
        {
            bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
        }
        catch (SocketException ex)
        {
            // Handle the exception (e.g., log an error, clean up, etc.)
            Debug.LogError("SocketException: " + ex.Message);
            CloseConnection(); // Close the connection
            return;
        }

        // Decode the bytes into a string
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        // Make sure we're not getting an empty string
        if (string.IsNullOrEmpty(dataReceived))
        {
            return;
        }

        // Convert the received string of data to the format we are using
        ParseData(dataReceived, out LatLong, out height, out SpeedNED, out Heading);


    }

    // Use-case specific function, need to re-write this to interpret whatever data is being sent
    public static void ParseData(string dataString, out Vector2d LatLong, out float height, out Vector3 velocity, out float heading)
    {
        Debug.Log(dataString);

        // Remove the parentheses
        if (dataString.StartsWith("(") && dataString.EndsWith(")"))
        {
            dataString = dataString.Substring(1, dataString.Length - 2);
        }

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

    void CloseConnection()
    {
        if (client != null)
        {
            client.Close();
            client = null;
            Debug.Log("Client Closed");
        }

        if (server != null)
        {
            server.Stop();
            server = null;
        }
    }

    /*void OnApplicationQuit()
    {
        thread.Abort(); // Stop the thread when the application quits
        CloseConnection(); // Close the connection
    }*/


    void Update()
    {
        int tileZoom = (int)Math.Round(map.Zoom);
        float tileScale = map.WorldRelativeScale;

        Vector2 tilePosition2D = Conversions.LatitudeLongitudeToUnityTilePosition(LatLong, tileZoom, tileScale);
        Vector3 mapPosition = new Vector3(tilePosition2D.x, height, tilePosition2D.y);

        if (mapPosition != formerPosition)
        {
            transform.position = mapPosition;
            formerPosition = mapPosition;
        }

        if (Heading != formerHeading)
        {
            // Calculate the desired yaw rotation based on the heading
            float desiredYaw = 360.0f + Heading; // Invert if necessary

            // Create a Quaternion representing the desired yaw rotation
            Quaternion desiredRotation = Quaternion.Euler(0, desiredYaw, 0);

            // Apply the desired rotation to the object's Transform
            transform.rotation = desiredRotation;

            // Update the previousHeading with the new heading value
            formerHeading = Heading;
        }

        Vector3 localSpeedNED = transform.InverseTransformDirection(SpeedNED);
        transform.Translate(Time.deltaTime * localSpeedNED);
    }
}
