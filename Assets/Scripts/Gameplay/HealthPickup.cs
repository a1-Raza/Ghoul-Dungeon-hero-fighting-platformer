using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthToGive = 100;
    [SerializeField] AudioClip sfx;

    Pickup pickupClass;

    // Start is called before the first frame update
    void Start()
    { 
        pickupClass = GetComponent<Pickup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupClass.GetEnabledPickupEffects())
        {
            pickupClass.GetPlayerGameObject().GetComponent<Health>().AddToHealth(healthToGive);
            if (sfx) { AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position); }
            Destroy(gameObject);
        }
    }
}
