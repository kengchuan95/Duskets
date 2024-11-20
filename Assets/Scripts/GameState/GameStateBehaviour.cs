using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateBehaviour : MonoBehaviour
{
    [SerializeField]
    private int remainingEnemies = 0;
    private float highScore = 9999999;
    public float totalTime = 0f;

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI remainingEnemiesText;

    private MainMenuBehavior mainMenuBehavior;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetFloat("highScore", 9999999);
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingEnemies > 0) 
        { 
            //if there are enemies, increment the current run time
            remainingEnemiesText.text = remainingEnemies.ToString();
            totalTime += Time.deltaTime;
        }
        else 
        { 
            //if there are no enemies, run the win screen
            showWinScreen();
        }
        UpdateGUI();
    }
    public void incrementEnemies(bool add)
    {
        //bool determines if we add or subtract an enemy 
        if (add)
        {
            remainingEnemies++;
        }
        else
        {
            remainingEnemies--;
        }

    }
    private void showWinScreen()
    {
        //set high score
        if (totalTime < highScore) { 
            PlayerPrefs.SetFloat("highScore", totalTime);
        }
        //let the cursor be available
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //load win screen
        SceneManager.LoadScene(3);
    }
    private void UpdateGUI()
    {
        //only update if this is the initially spawned behavior. The script is also linked to the enemy classes so we can increment
        if (highScoreText != null && currentScoreText != null)
        {
            if (highScore == 9999999)
            { //we haven't won before
                highScoreText.text = "Fastest Time: N/A";
            }
            else
            { //set the high score
                highScoreText.text = "Fastest Time: " + FormatTime(highScore);
            }
            //set the current timer
            currentScoreText.text = "Current Time: " + FormatTime(totalTime);
        }
    }
    private string FormatTime(float timeInSeconds) 
    {
        string formattedTime = "00:00.00"; //default
        float minutes = Mathf.FloorToInt(timeInSeconds / 60); // get the lowest int
        float seconds = timeInSeconds % 60; //get the remainder

        formattedTime = string.Format("{0}:{1}", minutes, seconds.ToString("00.00"));
        return formattedTime;
    }
     
}
