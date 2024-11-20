using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuBehavior : MonoBehaviour
{
    private int startScreen = 0;
    private int options = 1;
    private int mainGame = 2;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(mainGame);
    }
    public void OpenOptions()
    {
        SceneManager.LoadScene(options);
    }
    public void ReturnToStart()
    {
        SceneManager.LoadScene(startScreen);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
