using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject modeMenu;
    public GameObject options;
    public GameObject about;
    public GameObject DiscoverMenu;
    public GameObject InverseRealityMenu;
    public GameObject SimulationMenu;
    public GameObject NewLocationMenu;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;
    public Button DMButton;
    public Button IRMButton;
    public Button SMButton;
    public Button NewLocationDiscover;
    public Button DeleteLocationDiscover;
    public Button BackDiscoverLocation;
    public Button RunDiscoverMode;
    public Button SaveLocation;


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

    /*public void StartGame()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }*/

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

        SaveLocation.onClick.AddListener(DiscoverMode);
        OpenKeyboard();
    }



    public void RunDiscover()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void OpenKeyboard()
    {
        TouchScreenKeyboard.Open("");
    }
}