using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public EventSystem eventS;
    public GameObject FirstSelected;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Xbox"))
        {
            eventS.SetSelectedGameObject(FirstSelected);
            eventS.firstSelectedGameObject = FirstSelected;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
}
