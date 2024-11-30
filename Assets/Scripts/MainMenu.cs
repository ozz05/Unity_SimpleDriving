using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game";
    private const string HighScoreKey = "HighScore";
    [SerializeField] private TMP_Text highscoreText;

    private void Start() {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        highscoreText.text = currentHighScore.ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
