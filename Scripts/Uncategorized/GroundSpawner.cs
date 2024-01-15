using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace BernatAEX
{
    public class GroundSpawner : MonoBehaviour
    {
        public AbstractMap map;
        public Transform dronePrefab;
        public float altOffset = 1.0f;

        private void Start()
        {
            if (map == null || dronePrefab == null)
            {
                return;
            }
            if (PlayerPrefs.GetString("Location") == null || PlayerPrefs.GetString("Location") == "")
            {
                Vector2d centerLatLong = map.CenterLatitudeLongitude;
                Vector3 mapPosition = Conversions.GeoToWorldPosition(centerLatLong, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();
                float elevation = map.QueryElevationInMetersAt(centerLatLong);
                Vector3 spawnPosition = new Vector3(mapPosition.x, elevation + altOffset, mapPosition.y);
                Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
                return;
            }
            else
            {
                string savedLocationJson = PlayerPrefs.GetString("Location");
                Vector2 savedLocation = JsonUtility.FromJson<Vector2>(savedLocationJson);
                Vector2d savedLocation2d = new Vector2d(savedLocation.x, savedLocation.y);

                map.UpdateMap(savedLocation2d);

                Vector3 mapPosition = Conversions.GeoToWorldPosition(savedLocation2d, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();
                float elevation = map.QueryElevationInMetersAt(savedLocation2d);
                Vector3 spawnPosition = new Vector3(mapPosition.x, elevation + altOffset, mapPosition.y);
                dronePrefab.position = spawnPosition;
                dronePrefab.rotation = Quaternion.identity;
                Debug.Log("Map loaded");
            }
        }
    }
}
