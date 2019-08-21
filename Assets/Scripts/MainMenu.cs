using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Toggle XboxController;
    bool SwitchXboxControl;
    public EventSystem eventS;

    //if we just toggle on the xbox switch make the toggle selected
    public GameObject FirstSelectedWithToggle;
    //if we toggled to xbox and saved and exit than start playing again then make the start game button first selected
    public GameObject FirstSelectedGameStarts;
    private bool HasUserPressedButton = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Xbox"))
        {
            eventS.firstSelectedGameObject = FirstSelectedGameStarts;
            XboxController.isOn = true;
            SwitchXboxControl = true;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
    //attached to the toggle in the ui system
    public void ToggleForXboxController()
    {
        SwitchXboxControl = !SwitchXboxControl;
      
        if (SwitchXboxControl)
        {
            PlayerPrefs.SetString("Xbox", SwitchXboxControl.ToString());
            eventS.firstSelectedGameObject = FirstSelectedWithToggle;
        }
        else
        {
            PlayerPrefs.DeleteKey("Xbox");
            eventS.SetSelectedGameObject(null);
            eventS.firstSelectedGameObject = null;
        }
    }
}
