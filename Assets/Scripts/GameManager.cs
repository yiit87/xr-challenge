using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool Pause { get; private set; }
    public GameObject PauseMenu;

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

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            switchTheGameState = !switchTheGameState;
            PauseTheGame(switchTheGameState);
        }

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

        UpdateScoreValue();
        UpdateCollectedStarValue();

        WarningTextShut();
    }

    public void PauseTheGame(bool value)
    {
        Pause = value;
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
}
