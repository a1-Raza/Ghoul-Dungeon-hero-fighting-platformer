using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    GameObject playerGameObject;
    bool enablePickupEffects = false;

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Health>() || GetComponent<Health>().GetCurrentHealth() <= 0)
        {
            enablePickupEffects = true;
        }
    }

    public bool GetEnabledPickupEffects()
    {
        return enablePickupEffects;
    }

    public GameObject GetPlayerGameObject()
    {
        return playerGameObject;
    }    
}
