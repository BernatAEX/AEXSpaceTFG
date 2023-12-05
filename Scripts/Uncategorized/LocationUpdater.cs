using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationUpdater : MonoBehaviour
{
    public Transform playerTransform; // Referencia al objeto del jugador
    public float earthRadius = 6371000.0f; // Radio medio de la Tierra en metros (puede variar según el modelo de la Tierra)

    public double lat;
    public double lon;
    public double alt;

    private Vector3 position;

    public void UpdatePlayerPosition(double latitude, double longitude, double altitude)
    {
        Vector3 position = LLAtoXYZ(latitude, longitude, altitude);

        playerTransform.position = position;
    }

    private Vector3 LLAtoXYZ(double latitude, double longitude, double altitude)
    {
        double latRad = latitude * Mathf.Deg2Rad;
        double lonRad = longitude * Mathf.Deg2Rad;

        float x = (float)((earthRadius + altitude) * Mathf.Cos((float)latRad) * Mathf.Cos((float)lonRad));
        float y = (float)((earthRadius + altitude) * Mathf.Cos((float)latRad) * Mathf.Sin((float)lonRad));
        float z = (float)((earthRadius + altitude) * Mathf.Sin((float)latRad));

        return new Vector3(x, y, z);
    }
    void update()
    {
        transform.position = position;
    }
}
