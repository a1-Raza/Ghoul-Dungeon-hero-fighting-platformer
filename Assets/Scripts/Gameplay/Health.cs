using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] int maxHealth = 1000;
    [SerializeField] bool canBeAffectedByHazards = false;
    [SerializeField] bool destroyWhenNoMoreHealth = false;

    Animator animator;
    Collider2D[] objectColliders;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        objectColliders = GetComponents<Collider2D>();
        
        StartCoroutine(CheckIfCollidingWithHazard());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            if (animator) { animator.SetTrigger("takeDamage"); }
        }
        else
        {
            if (animator) { animator.SetTrigger("Die"); }
            Destroy(this);
            if (destroyWhenNoMoreHealth) { Destroy(gameObject); }
        }
    }

    public void AddToHealth(int healthToAdd)
    {
        health += healthToAdd;
        if (health >= maxHealth) { health = maxHealth; }
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    IEnumerator CheckIfCollidingWithHazard()
    {
        if (canBeAffectedByHazards)
        {
            foreach(Collider2D collider in objectColliders)
            {
                if (collider.GetType() == typeof(PolygonCollider2D))
                {
                    break;
                }
                if (collider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
                {
                    TakeDamage(50);
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
            }
        }
        yield return new WaitForSeconds(0f);
        StartCoroutine(CheckIfCollidingWithHazard());
    }
}
