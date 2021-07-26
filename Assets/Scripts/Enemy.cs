using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    Collider2D bodyCollider;
    BoxCollider2D feetCollider;
    Animator animator;

    PolygonCollider2D attackHitbox;

    bool toggleAttackHitbox = false;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();
        feetCollider = GetComponentInParent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        attackHitbox = GetComponentInChildren<PolygonCollider2D>();
        attackHitbox.enabled = false;

        StartCoroutine(WaitAndAttackLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
    }

    IEnumerator WaitAndAttackLoop()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        animator.SetTrigger("Attack");
        StartCoroutine(WaitAndAttackLoop());
    }

    void ToggleAttackHitbox()
    {
        toggleAttackHitbox = !toggleAttackHitbox;
        attackHitbox.enabled = toggleAttackHitbox;
        //Debug.Log(GetComponentInChildren<BoxCollider2D>().enabled);
    }

    void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
