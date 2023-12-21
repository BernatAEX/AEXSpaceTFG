using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace BernatAEX
{
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

            string savedLocationJson = PlayerPrefs.GetString("Location");
            Vector2 savedLocation = JsonUtility.FromJson<Vector2>(savedLocationJson);
            Vector2d savedLocation2d = new Vector2d(savedLocation.x, savedLocation.y);

            map.UpdateMap(savedLocation2d);

            Vector3 mapPosition = Conversions.GeoToWorldPosition(savedLocation2d, map.CenterMercator, map.WorldRelativeScale).ToVector3xz();
            float elevation = map.QueryElevationInMetersAt(savedLocation2d);
            Vector3 spawnPosition = new Vector3(mapPosition.x, elevation + altOffset, mapPosition.y);
            Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
