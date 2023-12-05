using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InputLocation : MonoBehaviour
{
    [Header("Input Fields")]
    public GameObject inputFieldName;
    public TMP_InputField inputFieldLatitude;
    public TMP_InputField inputFieldLongitude;
    public TMP_Dropdown dropdownNorthSouth;
    public TMP_Dropdown dropdownEastWest;
    public TMP_Dropdown dropdownLocations;

    [Header("Error Logs")]
    public GameObject Log;

    [Header("UI Pages")]
    public GameObject AddLocationMenu;
    public GameObject SelectLocationMenu;


    public void AddLocation()
    {
        Log.SetActive(false);
        if (AreFieldsFilled())
        {
            try
            {
                // Creates an instance for the DataManager
                DataManager dataManager = DataManager.Instance;

                // Constructs a new location
                string name = inputFieldName.GetComponent<TMP_InputField>().text;
                //string name = inputFieldName.text;
                Debug.Log(name);

                float latitude = float.Parse(inputFieldLatitude.text);
                
                float longitude = float.Parse(inputFieldLongitude.text);

                Debug.Log($"Name: {name}, latlong: {latitude.ToString()}");

                // Obtains and computes the dropdown results
                bool isNorth = (dropdownNorthSouth.value == 0); // 0: North, 1: South
                if (!isNorth)
                {
                    latitude = latitude * -1;
                }

                bool isEast = (dropdownEastWest.value == 0);    // 0: East, 1: West
                if (!isEast)
                {
                    longitude = longitude * -1;
                }

                // Adds a new location to the datamanager instance
                dataManager.AddLocation(name, latitude, longitude);
                LoadDropdown();
                AddLocationMenu.SetActive(false);
                SelectLocationMenu.SetActive(true);
                Debug.Log("LOCATION SAVED");
                

            }

            catch (FormatException)
            {
                Log.SetActive(true);
            }
        }
        else
        {
            Log.SetActive(true);
        }
    }

    // This function verifies if all fields are filled
    private bool AreFieldsFilled()
    {
        return !string.IsNullOrEmpty(inputFieldName.GetComponent<TMP_InputField>().text) &&
               !string.IsNullOrEmpty(inputFieldLatitude.text) &&
               !string.IsNullOrEmpty(inputFieldLongitude.text);
    }

    public void LoadDropdown()
    {
        // Clear the actual options
        dropdownLocations.ClearOptions();

        // Load the locations from the datamanager
        DataManager dataManager = DataManager.Instance;
        List<Location> locations = dataManager.Locations;

        // Create an array of names of the locations
        List<string> locationNames = new List<string>();
        foreach (Location location in locations)
        {
            locationNames.Add(location.name);
        }

        // Assign the options of the dropdown
        dropdownLocations.AddOptions(locationNames);
    

        /*public void LoadDropdown()
        {
            // Limpiar las opciones actuales
            dropdownLocations.ClearOptions();

            // Cargar las ubicaciones desde el datamanager
            List<Location> locations = dataManager.Locations;

            // Crear una lista de opciones de texto para el dropdown
            List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();
            foreach (Location location in locations)
            {
                // Crea una nueva opción de texto con el nombre de la ubicación
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(location.name);
                dropdownOptions.Add(option);
            }

            // Asignar las opciones del dropdown
            dropdownLocations.options = dropdownOptions;*/
    }
}