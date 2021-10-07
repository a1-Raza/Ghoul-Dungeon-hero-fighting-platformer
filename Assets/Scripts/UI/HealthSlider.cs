using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    GameObject playerGameObject;
    Health playerHealth;
    Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        playerGameObject = FindObjectOfType<Player>().gameObject;
        playerHealth = playerGameObject.GetComponent<Health>();
        if (!playerGameObject || !playerHealth) { return; }

        slider.maxValue = playerHealth.GetMaxHealth();
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth) 
        {
            slider.value = 0;
            return; 
        }
        slider.value = playerHealth.GetCurrentHealth();
    }
}
