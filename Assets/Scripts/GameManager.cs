using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool Pause { get; private set; }
    public GameObject PauseMenu;


    public bool PlayerDead { get; private set; }

    public Text ScoreText;
    private float TotalScore { get; set; }

    private int HealthValue;
    public Image HealthTotalRepresenter;

    private int StarCollect;
    public Text StarCollectedText;

    public GameObject WarningNotEnoughStar;

    float timer = 5f;

    GameObject Player;
    private bool switchTheGameState = false;
    public AudioSource MainMusic;

    public GameObject PausePanel;
    public GameObject YouDiedPanel;
    public GameObject EndGamePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Pause = false;
        TotalScore = 0f;
        HealthValue = 100;
        HealthBarAdjuster();
        PlayerDeadCondition(false);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OptionsMenuButton();
        }


        PauseConditions();
        UpdateScoreValue();
        UpdateCollectedStarValue();

        WarningTextShut();
    }

    
    public void PauseTheGame(bool value)
    {
        Pause = value;
    }

    public void PlayerDeadCondition(bool value)
    {
        PlayerDead = value;
    }
    private void PauseConditions()
    {
        if (Pause)
        {
            MainMusic.Pause();
            PauseMenu.SetActive(true);
        }
        else
        {
            if (!MainMusic.isPlaying)
            {
                MainMusic.UnPause();
            }
            PauseMenu.SetActive(false);
        }
    }

    public void AddToTotalScore(float value)
    {
        TotalScore += value;
    }   

    public void PlayerTakeDamage(int damage)
    {
        HealthValue -= damage;
        HealthBarAdjuster();
    }

    public void PlayerAddHealth(int heal)
    {
        HealthValue += heal;

        HealthBarAdjuster();
    }

    public void PlayerSpeedIncrease(int value)
    {
        Player.GetComponent<PlayerMovement>().AdjustSpeed(value);
    }

    public void AddCollectedStar()
    {
        StarCollect++;
    }

    public int Return_CollectedStar()
    {
        return StarCollect;
    }

    public void UpdateCollectedStarValue()
    {
        StarCollectedText.text = "Collected Stars: " + StarCollect.ToString() + "/5";
    }

    public void HealthBarAdjuster()
    {
        HealthTotalRepresenter.fillAmount = HealthValue / 100f;
    }
    public void UpdateScoreValue()
    {
        ScoreText.text = "Score: " + TotalScore.ToString();
    }

    public void WarningTextActivate()
    {
        WarningNotEnoughStar.SetActive(true);
    }

    public void WarningTextShut()
    {
        if (WarningNotEnoughStar.activeSelf)
        {
            timer -= 1 * Time.deltaTime;

            if (timer <= 0)
            {
                WarningNotEnoughStar.SetActive(false);
                timer = 5f;
            }
        }
    }

    public void PlayerDiedPanelActivation()
    {
        YouDiedPanel.SetActive(true);
    }

    public void ActivateEndGamePanel()
    {
        EndGamePanel.SetActive(true);
    }

    //Menu Button Functionalities
    public void OptionsMenuButton()
    {
        switchTheGameState = !switchTheGameState; // Change between true or false
        PauseTheGame(switchTheGameState); // Depending of the true or false value pause or unpause the game
        PausePanel.SetActive(switchTheGameState); // Depending of the true or false value turn on or off the pause menu
    }

    #region Button Functionalities
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeButton()
    {
        OptionsMenuButton();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    #endregion
}
