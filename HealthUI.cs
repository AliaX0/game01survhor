using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab; // Reference our health bar prefab
    public Transform target; // Position of character
    float visibleTime = 5; // Time for after the character has taken damage, for how long should the health bar be visible.
    float lastMadeVisibleTime; // When the health bar was last made visible


    Transform ui; // Position of character's health UI
    Image healthSlider; // Green part of the health, that fills the health bar
    Transform cam; // Reference to the camera's position


    void Start()
    {
        cam = Camera.main.transform;

        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
	}

    void OnHealthChanged(int maxHealth, int currentHealth) {
        if (ui != null)
        {
            ui.gameObject.SetActive(true); // Ensure the health bar is visible when we have a health change
            lastMadeVisibleTime = Time.time; // Set equal to current time

            float healthPercent = (float)currentHealth / maxHealth; // Since they are both integers, we cast one of them to a float
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
   
    

    void LateUpdate()
    {
        if (ui != null){
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastMadeVisibleTime > visibleTime){
                ui.gameObject.SetActive(false);
            }
        }

    }

    
}
