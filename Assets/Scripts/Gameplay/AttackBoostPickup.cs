using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoostPickup : MonoBehaviour
{
    [SerializeField] float attackMultiplierToAdd = 0.2f;
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
            pickupClass.GetPlayerGameObject().GetComponent<Player>().AddToAttackMultiplier(attackMultiplierToAdd);
            //Debug.Log(pickupClass.GetPlayerGameObject().GetComponent<Player>().GetAttackMultiplier());
            if (sfx) { AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position); }
            Destroy(gameObject);
        }
    }
}
