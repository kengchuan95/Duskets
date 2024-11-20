using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehavior : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
    }
}
