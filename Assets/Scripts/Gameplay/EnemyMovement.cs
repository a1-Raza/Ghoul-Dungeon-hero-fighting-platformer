using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float distanceToSeePlayer = 7.5f;
    [SerializeField] float distanceToAttackPlayer = 4;
    [SerializeField] float enemySpeed = 5f;
    [SerializeField] float minIdleWaitTime = 1f;
    [SerializeField] float maxIdleWaitTime = 5f;

    [Header("Attack Cooldown")]
    Cooldown attackCooldown;
    [SerializeField] float attackCooldownTime = 3;

    float distanceToPlayer;
    bool isMovingIdle = false;

    Rigidbody2D enemyRigidbody;
    GameObject playerGameobject;
    Animator animator;
    EnemyAttack enemyAttackComponent;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyAttackComponent = GetComponent<EnemyAttack>();

        attackCooldown = new Cooldown(attackCooldownTime);

        playerGameobject = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Mathf.Abs(Vector2.Distance(transform.position, playerGameobject.transform.position));

        HandleMovementAnimation();

        if (enemyAttackComponent.GetIsIdle()) 
        { 
            return; 
        }

        if (distanceToPlayer <= distanceToAttackPlayer && !FindObjectOfType<Player>().GetPlayerDied())
        {
            StopCoroutine(MoveRandomly());
            isMovingIdle = false;
            FacePlayer();
            if (!attackCooldown.IsInCooldown())
            {
                enemyAttackComponent.Attack();
                StartCoroutine(attackCooldown.StartCoolDown());
            }
        }
        else if (distanceToPlayer <= distanceToSeePlayer && !FindObjectOfType<Player>().GetPlayerDied())
        {
            StopCoroutine(MoveRandomly());
            isMovingIdle = false;
            FacePlayer();
            MoveTowardsPlayer();
        }
        else
        {
            HandleSpriteFlip();
            MoveIdle();
        }
    }

    private void HandleMovementAnimation()
    {
        if (Mathf.Abs(enemyRigidbody.velocity.x) > 0.1f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void HandleSpriteFlip()
    {
        if (Mathf.Abs(enemyRigidbody.velocity.x) <= 0.1f) { return; }
        var sideFacing = Mathf.Sign(enemyRigidbody.velocity.x);
        transform.localScale = new Vector2(-sideFacing, 1);
    }

    void FacePlayer()
    {
        var differenceInXPos = Mathf.Abs(playerGameobject.transform.position.x - transform.position.x);
        if (differenceInXPos <= 0.1f) { return; }

        if (playerGameobject.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (playerGameobject.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1); ;
        }
    }

    void MoveTowardsPlayer()
    {
        var differenceInXPos = Mathf.Abs(playerGameobject.transform.position.x - transform.position.x);

        if (differenceInXPos <= 0.2f)
        {
            enemyRigidbody.velocity = new Vector2(0, enemyRigidbody.velocity.y);
            return;
        }

        if (playerGameobject.transform.position.x < transform.position.x)
        {
            enemyRigidbody.velocity = new Vector2(-enemySpeed, enemyRigidbody.velocity.y);
        }
        else if (playerGameobject.transform.position.x > transform.position.x)
        {
            enemyRigidbody.velocity = new Vector2(enemySpeed, enemyRigidbody.velocity.y);
        }
    }

    void MoveIdle()
    {
        if (isMovingIdle)
        {
            return;
        }
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        isMovingIdle = true;
        var waitTime = Random.Range(minIdleWaitTime, maxIdleWaitTime);
        yield return new WaitForSeconds(waitTime);
        enemyRigidbody.velocity = new Vector2(Random.Range(-enemySpeed, enemySpeed), enemyRigidbody.velocity.y);
        yield return new WaitForSeconds(0.5f);
        isMovingIdle = false;
    }

    public float GetDistanceToSeePlayer()
    {
        return distanceToSeePlayer;
    }
}
