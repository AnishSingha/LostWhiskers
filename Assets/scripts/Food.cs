using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public float maxFoodStamina = 100f;
    
    bool nearFood = false;
    public float foodStaminaIncreaseValue= 0f;


    
    private float currentFoodStamina;    // The current stamina value
    public float degenRate = 1f;
    public Slider foodSlider;
    StaminaBar staminaBar;


    private void Start()
    {
        currentFoodStamina = maxFoodStamina;
        UpdateFoodUI();
        staminaBar = FindObjectOfType<StaminaBar>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            nearFood = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            nearFood = false;
        }
    }

    private void UpdateFoodUI()
    {
        foodSlider.value = currentFoodStamina / maxFoodStamina;
    }

    private void Update()
    {


        currentFoodStamina -= degenRate * Time.deltaTime;

        currentFoodStamina = Mathf.Clamp(currentFoodStamina, 0f, maxFoodStamina);

        UpdateFoodUI();
        if (currentFoodStamina <= 0.1f)
        {
            staminaBar.StopSprinting();
        }

        else if (Input.GetKeyDown(KeyCode.T)&&nearFood == true)
        {
            
            currentFoodStamina +=  25f;
            UpdateFoodUI();
            Debug.Log("T pressed");
            //Destroy();
            
        }

        
    }
}
