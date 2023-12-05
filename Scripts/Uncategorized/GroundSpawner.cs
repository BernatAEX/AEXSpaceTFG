using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public AbstractMap map; 
    public GameObject dronePrefab;
    public float altOffset = 1.0f;

    private void Awake()
    {
        if (map == null || dronePrefab == null)
        {
            return;
        }

        Vector2d centerLatLong = map.CenterLatitudeLongitude;
        Vector3 mapPosition = Conversions.GeoToWorldPosition(centerLatLong, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();
        float elevation = map.QueryElevationInMetersAt(centerLatLong);
        Vector3 spawnPosition = new Vector3(mapPosition.x, elevation + altOffset, mapPosition.y);
        Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
    }
}

