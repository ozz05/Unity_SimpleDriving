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
    [SerializeField] private Button playButton;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private IOSNotificationHandler iOSNotificationHandler;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] private Image energyImage;

    private int energy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private void Start()
    {
        OnApplicationFocus(true);
    }
    private void OnApplicationFocus(bool focusStatus){
        
        if(!focusStatus) return;
        //This method cancels all invokes that are currently running if you have more the one use a string to specify which one
        CancelInvoke();
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
                RechargeEnergy();
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(RechargeEnergy), (energyReady - DateTime.Now).Seconds);
            }
        }
    }
    private void RechargeEnergy()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        UpdateEnergyImage();
    }
    public void UpdateEnergyRecharge()
    {
        DateTime timeFromNow = DateTime.Now.AddMinutes(energyRechargeDuration);
#if UNITY_ANDROID
        androidNotificationHandler.ScheduleNotification(energyReady);
#elif UNITY_IOS
        iOSNotificationHandler.ScheduleNotification(energyRechargeDuration);
#endif
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
