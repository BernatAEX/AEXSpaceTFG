using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class Location
{
    public string name;
    public float latitude;
    public float longitude;

    public Location(string _name, float _latitude, float _longitude)
    {
        name = _name;
        latitude = _latitude;
        longitude = _longitude;
    }
}

public class DataManager : MonoBehaviour
{
    // Singleton instance
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("DataManager");
                    _instance = go.AddComponent<DataManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public List<Location> Locations { get; private set; }

    private void Awake()
    {
        // Load locations from PlayerPrefs on startup
        LoadLocations();
    }

    public void AddLocation(string name, float latitude, float longitude)
    {
        Location newLocation = new Location(name, latitude, longitude);
        Locations.Add(newLocation);

        // Save locations to PlayerPrefs after adding a new location
        SaveLocations();
    }

    private void SaveLocations()
    {
        string jsonData = JsonUtility.ToJson(new LocationData(Locations));
        PlayerPrefs.SetString("Locations", jsonData);
        PlayerPrefs.Save();
    }

    private void LoadLocations()
    {
        if (PlayerPrefs.HasKey("Locations"))
        {
            string jsonData = PlayerPrefs.GetString("Locations");
            LocationData locationData = JsonUtility.FromJson<LocationData>(jsonData);
            Locations = locationData.locations;
        }
        else
        {
            Locations = new List<Location>();
        }
    }
}

[System.Serializable]
public class LocationData
{
    public List<Location> locations;

    public LocationData(List<Location> _locations)
    {
        locations = _locations;
    }
}
