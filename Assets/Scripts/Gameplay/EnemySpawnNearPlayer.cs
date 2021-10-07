using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnNearPlayer : MonoBehaviour
{
    [SerializeField] bool spawnWhenPlayerNear;
    [SerializeField] bool useEnemyMovementDistanceToSeePlayer;
    [SerializeField] float xDistanceToSpawnFromPlayer = 5;
    [SerializeField] float yDistanceToSpawnFromPlayer = 2;

    EnemyAttack enemyAttackScript;
    EnemyMovement enemyMovementScript;
    Health healthScript;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    SpriteRenderer spriteRenderer;

    Player player;

    bool scriptsEnabled;

    // Start is called before the first frame update
    void Start()
    {
        if (!spawnWhenPlayerNear) { return; }

        enemyAttackScript = GetComponent<EnemyAttack>();
        enemyMovementScript = GetComponent<EnemyMovement>();
        healthScript = GetComponent<Health>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        player = FindObjectOfType<Player>();

        if (useEnemyMovementDistanceToSeePlayer)
        {
            xDistanceToSpawnFromPlayer = enemyMovementScript.GetDistanceToSeePlayer();
            yDistanceToSpawnFromPlayer = enemyMovementScript.GetDistanceToSeePlayer();
        }

        EnableNecessaryScripts(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnWhenPlayerNear) { return; }
        if (scriptsEnabled) { return; }

        bool playerCloseEnoughX = Mathf.Abs(transform.position.x - player.gameObject.transform.position.x) <= xDistanceToSpawnFromPlayer;
        bool playerCloseEnoughY = Mathf.Abs(transform.position.y - player.gameObject.transform.position.y) <= yDistanceToSpawnFromPlayer;
        if (playerCloseEnoughX && playerCloseEnoughY)
        {
            if (animator.enabled && spriteRenderer.enabled) { return; }
            //EnableNecessaryScripts();
            animator.enabled = true;
            spriteRenderer.enabled = true;
            animator.SetTrigger("Spawn");
        }
    }

    void EnableNecessaryScripts()
    {
        EnableNecessaryScripts(true);
    }
    void EnableNecessaryScripts(bool tOf)
    {
        enemyAttackScript.enabled = tOf;
        enemyMovementScript.enabled = tOf;
        healthScript.enabled = tOf;
        animator.enabled = tOf;
        bodyCollider.enabled = tOf;
        spriteRenderer.enabled = tOf;

        scriptsEnabled = tOf;
    }
    
}
