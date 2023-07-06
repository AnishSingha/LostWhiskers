using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaBar : MonoBehaviour
{
    public float maxStamina = 100f;    // The maximum stamina value
    public float sprintStaminaCost = 1f;   // The amount of stamina consumed per second while sprinting
    public float staminaRegenRate = 0f;    // The rate at which stamina regenerates per second

    public Slider staminaSlider;    // Reference to the UI Slider component representing the stamina bar

    private float currentStamina;    // The current stamina value
    //private bool isSprinting;    // Flag indicating if the player is currently sprinting

    PlayerMovement playerMovement;

    private void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        
        

        if (playerMovement.isSprinting)
        {
            currentStamina -= sprintStaminaCost * Time.deltaTime;

            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

            UpdateStaminaUI();

            if (currentStamina <= 0f)
            {
                StopSprinting();
            }
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;

            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

            UpdateStaminaUI();
        }
    }

    private void UpdateStaminaUI()
    {
        staminaSlider.value = currentStamina / maxStamina;
    }

    public void StartSprinting()
    {
        if (currentStamina > 0f)
        {
            playerMovement.isSprinting = true;
        }
    }

    public void StopSprinting()
    {
        playerMovement.isSprinting = false;
    }
}
