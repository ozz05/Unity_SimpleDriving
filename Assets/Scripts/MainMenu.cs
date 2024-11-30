using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game";
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] private Image energyImage;

    private int energy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private void Start() {
        //Set the High Score
        int currentHighScore = PlayerPrefs.GetInt(ScoreHandler.HighScoreKey, 0);
        highscoreText.text = currentHighScore.ToString();

        //Get the energy
        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        UpdateEnergyImage();
        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            if(energyReadyString == string.Empty)
            {
                UpdateEnergyRecharge();
            }
            
            DateTime energyReady = DateTime.Parse(energyReadyString);
            //Check if the time has past
            if (DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
        }
        UpdateEnergyImage();
    }
    public void UpdateEnergyRecharge()
    {
        DateTime timeFromNow = DateTime.Now.AddMinutes(energyRechargeDuration);
        PlayerPrefs.SetString(EnergyReadyKey, timeFromNow.ToString());
    }
    private void UpdateEnergyImage()
    {
        if (!(energyImage == null))
        {
            energyImage.fillAmount = (float) energy / maxEnergy;
        }
    }

    private void UpdateEnergy()
    {
        energy --;
        PlayerPrefs.SetInt(EnergyKey, energy);
        if (energy <= 0)
        {
            UpdateEnergyRecharge();
        }
    }
    public void StartGame()
    {
        if(energy > 0)
        {
            UpdateEnergy();
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
