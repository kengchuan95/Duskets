using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerValues : HealthScript
{
    private float currentHealthLerpTimer;
    private float chipSpeed = 10f;
    
    public TextMeshProUGUI currentHealthText;
    public Image healthBar;
    public Image healthBarUnderlay;

    private System.Random rand = new System.Random();


    private CharacterController playerController;

    private float airTime = 0f;
    private float fallDamageModifier = 0.5f;
    private float safeFallTime = 1.5f;
    private float playerKillHeight = -20; 

    // Start is called before the first frame update
    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        currentHealthText.text = currentHealth.ToString();
    }
    // Update is called once per frame
    void Update()
    { 
        FallFromHeight(); //checks fall height and damages if necessary
        UpdateHealthUI(); //updates the healthbar
        UpdateScene(); //move to game over screen if dead
    }

    private void FallDamage(float velocity)
    {
        float fallDamage = velocity / fallDamageModifier;
        int fallDamageNormalized = (int)fallDamage;
        PlayerDamaged(fallDamageNormalized);
    }
    private void FallFromHeight()
    {
        //check jump/fall logic
        if (!playerController.isGrounded)
        {
            if (playerController.velocity.y < 0.01f)
                airTime += Time.deltaTime;
        }
        else
        {
            if (airTime > safeFallTime)
            {
                FallDamage(airTime);
            }
            airTime = 0f;
        }
        if (playerController.transform.position.y < playerKillHeight)
        {
            PlayerDamaged(200);
        }
    }
    public void PlayerDamaged(int damage)
    {
        Damaged(damage);
        currentHealthLerpTimer = 0f;
        //update UI
        currentHealthText.text = currentHealth.ToString();
    }
    private void UpdateHealthUI()
    {
        float healthPercentage = (float)currentHealth / (float)maxHealth;
        float fill = healthBarUnderlay.fillAmount;
        healthBar.fillAmount = healthPercentage ;
        currentHealthLerpTimer += Time.deltaTime;
        float percentComplete = currentHealthLerpTimer / chipSpeed;
        healthBarUnderlay.fillAmount = Mathf.Lerp(fill, (healthPercentage - 0.01f), percentComplete);
    }
    private void UpdateScene()
    {
        if (!isLive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(4);
        }
    }
}
