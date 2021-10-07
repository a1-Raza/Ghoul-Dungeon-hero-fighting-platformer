using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackMultiplierText : MonoBehaviour
{
    Player playerClass;

    TMP_Text multiplierText;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = FindObjectOfType<Player>();
        multiplierText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        multiplierText.text = "x" + playerClass.GetAttackMultiplier().ToString();
    }
}
