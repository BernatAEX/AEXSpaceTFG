using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Globalization;


namespace BernatAEX
{
    public class LoadSaveManager : MonoBehaviour
    {
        [Header("Input Fields")]
        public TMP_InputField NameInput;
        public TMP_InputField LatInput;
        public TMP_InputField LongInput;
        public TMP_Dropdown NSDropdown;
        public TMP_Dropdown EWDropdown;


        private string savePath;

        private void Start()
        {
            savePath = Application.persistentDataPath + "/Locations.json";
            Debug.Log(savePath);
        }

        public void SaveData()
        {
            int isNorth = NSDropdown.value; // 0: North, 1: South
            if (isNorth == 0)
            {
                isNorth = 1;
            }
            else
            {
                isNorth = -1;
            }

            int isEast = EWDropdown.value;  // 0: East, 1: West
            if (isEast == 0)
            {
                isEast = 1;
            }
            else
            {
                isEast = -1;
            }

            SaveDataListModel data = new SaveDataListModel();
            if (File.Exists(savePath))
            {
                string jsonData = File.ReadAllText(savePath);
                data = JsonUtility.FromJson<SaveDataListModel>(jsonData);
            }

            float lat = isNorth * float.Parse(LatInput.text, CultureInfo.InvariantCulture);
            float lon = isEast * float.Parse(LongInput.text, CultureInfo.InvariantCulture);

            SaveDataModel save = new SaveDataModel(NameInput.text, lat, lon);
            data.dataList.Add(save);

            string finalJsonData = JsonUtility.ToJson(data);
            File.WriteAllText(savePath, finalJsonData);

            Debug.Log("Data saved!");
        }


        public void LoadNames(TMP_Dropdown LocationsDropdown)
        {

            LocationsDropdown.ClearOptions();
            if (File.Exists(savePath))
            {
                string jsonData = File.ReadAllText(savePath);
                SaveDataListModel data = JsonUtility.FromJson<SaveDataListModel>(jsonData);
                if (data != null && data.dataList != null)
                {
                    List<string> names = new List<string>();
                    foreach (var saveData in data.dataList)
                    {
                        names.Add(saveData.Name);
                    }

                    LocationsDropdown.AddOptions(names);
                }
            }
        }

        public void LoadData(int selectedIndex)
        {
            Vector2 loc = Vector2.zero;

            if (File.Exists(savePath))
            {
                string jsonData = File.ReadAllText(savePath);
                SaveDataListModel data = JsonUtility.FromJson<SaveDataListModel>(jsonData);

                if (data != null && data.dataList != null)
                {
                    SaveDataModel selectedData = data.dataList[selectedIndex];
                    loc.x = selectedData.Latitude;
                    loc.y = selectedData.Longitude;

                    PlayerPrefs.SetString("Location", JsonUtility.ToJson(loc));
                    Debug.Log("Data loaded!");
                }
                else
                {
                    Debug.Log("Saved data is corrupt or empty.");
                }
            }
            else
            {
                Debug.Log("No saved data found.");
            }
        }
    }

    [System.Serializable]
    public class SaveDataListModel
    {
        public List<SaveDataModel> dataList;

        public SaveDataListModel()
        {
            dataList = new List<SaveDataModel>();
        }
    }

    [System.Serializable]
    public class SaveDataModel
    {
        public string Name;
        public float Latitude;
        public float Longitude;

        public SaveDataModel(string name, float latitude, float longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}