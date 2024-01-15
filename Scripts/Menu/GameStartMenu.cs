using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace BernatAEX
{
    [RequireComponent(typeof(LoadSaveManager))]
    public class GameStartMenu : MonoBehaviour
    {
        [Header("UI Pages")]
        public GameObject mainMenu;
        public GameObject modeMenu;
        [SerializeField] private GameObject options;
        [SerializeField] private GameObject about;
        [SerializeField] private GameObject DiscoverMenu;
        [SerializeField] private GameObject InverseRealityMenu;
        [SerializeField] private GameObject SimulationMenu;
        [SerializeField] private GameObject NewLocationMenu;
        [SerializeField] private GameObject LocationDWN;

        [Header("Main Menu Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button optionButton;
        [SerializeField] private Button aboutButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button DMButton;
        [SerializeField] private Button IRMButton;
        [SerializeField] private Button SMButton;
        [SerializeField] private Button NewLocationDiscover;
        [SerializeField] private Button DeleteLocationDiscover;
        [SerializeField] private Button BackDiscoverLocation;
        [SerializeField] private Button RunDiscoverMode;
        [SerializeField] private Button SaveLocation;
        [SerializeField] private Button BackLocation;
        [SerializeField] private Button NewLocationSimulation;
        public Button BackSimulation;
        public Button RunSimulationMode;
        public Button RunIP;
        public Button BackIP;

        [Header("Other Features")]
        public LoadSaveManager LSManager;
        public TMP_Dropdown PlacesDropdown;
        public TMP_InputField IPInput;
        public List<Button> returnButtons;


        // Start is called before the first frame update
        void Start()
        {
            EnableMainMenu();

            //Hook events
            startButton.onClick.AddListener(StartGame);
            optionButton.onClick.AddListener(EnableOption);
            aboutButton.onClick.AddListener(EnableAbout);
            quitButton.onClick.AddListener(QuitGame);


            foreach (var item in returnButtons)
            {
                item.onClick.AddListener(EnableMainMenu);
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void StartGame()
        {
            mainMenu.SetActive(false);
            modeMenu.SetActive(true);
            options.SetActive(false);
            about.SetActive(false);
            DiscoverMenu.SetActive(false);
            InverseRealityMenu.SetActive(false);
            SimulationMenu.SetActive(false);
            NewLocationMenu.SetActive(false);
            LocationDWN.SetActive(false);


            DMButton.onClick.AddListener(DiscoverMode);
            IRMButton.onClick.AddListener(IRMode);
            SMButton.onClick.AddListener(SimulationMode);

        }

        public void HideAll()
        {
            mainMenu.SetActive(false);
            modeMenu.SetActive(false);
            options.SetActive(false);
            about.SetActive(false);
            DiscoverMenu.SetActive(false);
            InverseRealityMenu.SetActive(false);
            SimulationMenu.SetActive(false);
            NewLocationMenu.SetActive(false);
            LocationDWN.SetActive(false);

        }

        public void EnableMainMenu()
        {
            mainMenu.SetActive(true);
            modeMenu.SetActive(false);
            options.SetActive(false);
            about.SetActive(false);
            DiscoverMenu.SetActive(false);
            InverseRealityMenu.SetActive(false);
            SimulationMenu.SetActive(false);
            NewLocationMenu.SetActive(false);
            LocationDWN.SetActive(false);

        }
        public void EnableOption()
        {
            mainMenu.SetActive(false);
            modeMenu.SetActive(false);
            options.SetActive(true);
            about.SetActive(false);
            DiscoverMenu.SetActive(false);
            InverseRealityMenu.SetActive(false);
            SimulationMenu.SetActive(false);
            NewLocationMenu.SetActive(false);
            LocationDWN.SetActive(false);


        }
        public void EnableAbout()
        {
            mainMenu.SetActive(false);
            modeMenu.SetActive(false);
            options.SetActive(false);
            about.SetActive(true);
            DiscoverMenu.SetActive(false);
            InverseRealityMenu.SetActive(false);
            SimulationMenu.SetActive(false);
            NewLocationMenu.SetActive(false);
            LocationDWN.SetActive(false);

        }

        public void DiscoverMode()
        {
            mainMenu.SetActive(false);
            modeMenu.SetActive(false);
            options.SetActive(false);
            about.SetActive(false);
            DiscoverMenu.SetActive(true);
            InverseRealityMenu.SetActive(false);
            SimulationMenu.SetActive(false);
            NewLocationMenu.SetActive(false);
            LocationDWN.SetActive(true);

            LSManager.LoadNames(PlacesDropdown);

            NewLocationDiscover.onClick.AddListener(NewLoc);
            DeleteLocationDiscover.onClick.AddListener(EnableMainMenu);
            BackDiscoverLocation.onClick.AddListener(StartGame);
            RunDiscoverMode.onClick.AddListener(RunDiscover);
        }

        public void IRMode()
        {
            mainMenu.SetActive(false);
            modeMenu.SetActive(false);
            options.SetActive(false);
            about.SetActive(false);
            DiscoverMenu.SetActive(false);
            InverseRealityMenu.SetActive(true);
            SimulationMenu.SetActive(false);
            NewLocationMenu.SetActive(false);
            LocationDWN.SetActive(false);


            RunIP.onClick.AddListener(RunIRmode);
            BackIP.onClick.AddListener(StartGame);
            OpenKeyboard();
        }

        public void SimulationMode()
        {
            mainMenu.SetActive(false);
            modeMenu.SetActive(false);
            options.SetActive(false);
            about.SetActive(false);
            DiscoverMenu.SetActive(false);
            InverseRealityMenu.SetActive(false);
            SimulationMenu.SetActive(true);
            NewLocationMenu.SetActive(false);
            LocationDWN.SetActive(true);

            LSManager.LoadNames(PlacesDropdown);


            NewLocationSimulation.onClick.AddListener(NewLoc);
            BackSimulation.onClick.AddListener(StartGame);
            RunSimulationMode.onClick.AddListener(RunSimulation);
        }

        public void NewLoc()
        {
            mainMenu.SetActive(false);
            modeMenu.SetActive(false);
            options.SetActive(false);
            about.SetActive(false);
            DiscoverMenu.SetActive(false);
            InverseRealityMenu.SetActive(false);
            SimulationMenu.SetActive(false);
            NewLocationMenu.SetActive(true);
            LocationDWN.SetActive(false);


            SaveLocation.onClick.AddListener(EndSaveLocation);
            BackLocation.onClick.AddListener(StartGame);
            OpenKeyboard();
        }

        public void RunDiscover()
        {
            LSManager.LoadData(PlacesDropdown.value);
            HideAll();
            //SceneTransitionManager.singleton.GoToSceneAsync(1);
            //SceneManager.LoadScene("DiscoverMode");
        }

        public void RunSimulation()
        {
            LSManager.LoadData(PlacesDropdown.value);
            HideAll();
            //SceneTransitionManager.singleton.GoToSceneAsync(2);
            //SceneManager.LoadScene("SimulationMode");
        }

        public void RunIRmode()
        {
            PlayerPrefs.SetString("Broker Address", JsonUtility.ToJson(IPInput.text));
            HideAll();
            //SceneTransitionManager.singleton.GoToSceneAsync(3);
            //SceneManager.LoadScene("IRMode");
        }

        public void OpenKeyboard()
        {
            TouchScreenKeyboard.Open("");
        }

        public void EndSaveLocation()
        {
            LSManager.SaveData();
            StartGame();
        }
    }
}