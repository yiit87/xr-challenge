using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool Pause { get; private set; }

    public Text ScoreText;
    private float TotalScore { get; set; }

    private int HealthValue;
    public Image HealthTotalRepresenter;

    GameObject Player;

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
        UpdateScoreValue();

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

    public void HealthBarAdjuster()
    {
        HealthTotalRepresenter.fillAmount = HealthValue / 100f;
    }
    public void UpdateScoreValue()
    {
        ScoreText.text = "Score: " + TotalScore.ToString();
    }
}
