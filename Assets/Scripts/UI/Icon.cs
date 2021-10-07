using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    [SerializeField] bool specialAttack;
    [SerializeField] bool dash;

    Player playerClass;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        if (specialAttack && dash)
        {
            Debug.LogError(gameObject.name + ": Special Attack and Dash can't both be checked!");
            return;
        }
        else if (!specialAttack && !dash)
        {
            Debug.LogError(gameObject.name + ": Either Special Attack or Dash have to be checked!");
            return;
        }
        image = GetComponent<Image>();
        playerClass = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (specialAttack)
        {
            if (playerClass.IsInSpecialAttackCooldown())
            {
                image.color = new Color32(90, 90, 90, 150);
            }
            else
            {
                image.color = new Color32(255, 255, 255, 255);
            }
        }
        else if (dash)
        {
            if (playerClass.IsInDashCooldown())
            {
                image.color = new Color32(90, 90, 90, 150);
            }
            else
            {
                image.color = new Color32(255, 255, 255, 255);
            }
        }
    }
}
