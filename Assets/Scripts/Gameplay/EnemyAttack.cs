using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] bool idle = false;
    [SerializeField] bool idleAttack = false;
    [SerializeField] float attackCooldownTime = 3;

    Rigidbody2D enemyRigidbody;
    Collider2D bodyCollider;
    BoxCollider2D feetCollider;
    Animator animator;
    Cooldown attackCooldown;

    PolygonCollider2D attackHitbox;

    //bool toggleAttackHitbox = false;

    bool isAttacking = false;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<Collider2D>();
        feetCollider = GetComponentInParent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        attackCooldown = new Cooldown(attackCooldownTime);
        attackHitbox = GetComponentInChildren<PolygonCollider2D>();
        attackHitbox.enabled = false;
        if (!idle && idleAttack) { Debug.LogError(gameObject.name + " cannot IdleAttack if Idle is disabled!"); }
        if (idle && idleAttack) { StartCoroutine(WaitAndAttackLoop()); }

        //StartCoroutine(WaitAndAttackLoop());
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
        Attack();
        StartCoroutine(WaitAndAttackLoop());
    }

    public void Attack()
    {
        if (isAttacking) { return; }
        animator.SetTrigger("Attack");
        //attackCooldown.StartCoolDown();
    }

    void SetAttackHitboxEnabled(int enabled)
    {
        attackHitbox.enabled = NumberToBool(enabled);
        //Debug.Log(GetComponentInChildren<BoxCollider2D>().enabled);
    }
    
    void SetIsAttacking(int canAttack)
    {
        isAttacking = NumberToBool(canAttack);
    }

    void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    bool NumberToBool(int number)
    {
        if (number == 1) { return true; }
        else { return false; }
    }

    public bool GetIsIdle() { return idle; }
    public bool GetIsIdleAttack() { return idleAttack; }
}
