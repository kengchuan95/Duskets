using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreMainMenuScript : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        GetHighScore();   
    }
    public void GetHighScore()
    {
        var highScore = PlayerPrefs.GetFloat("highScore", 9999999);
        if (highScore == 9999999)
        {
            highScoreText.text = "High Score: N/A";
        }
        else
        {
            highScoreText.text = "High Score: " + FormatTime(highScore);
        }
    }
    private string FormatTime(float timeInSeconds)
    {
        string formattedTime = "00:00.00";
        float minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float seconds = timeInSeconds % 60;

        formattedTime = string.Format("{0}:{1}", minutes, seconds.ToString("00.00"));
        return formattedTime;
    }
}
