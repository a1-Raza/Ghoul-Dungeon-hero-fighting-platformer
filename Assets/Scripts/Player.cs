using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float dragSpeedOnAttack = 1f;

    [Header("Sounds")]
    [SerializeField] AudioClip footstepSound;

    PolygonCollider2D attackHitbox;

    Rigidbody2D playerRigidbody;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    Animator animator;
    AudioSource audioSource;

    bool isAttacking = false;
    bool isDead = false;
    bool toggleAttackHitbox = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponentInParent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        attackHitbox = GetComponentInChildren<PolygonCollider2D>();
        attackHitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (!isAttacking)
        {
            HandleHorizontalMovement();
            HandleJump();
            HandleSpriteFlip();
        }
        HandleAttack();
    }

    void HandleHorizontalMovement()
    {
        playerRigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRigidbody.velocity.y);
        if (playerRigidbody.velocity.x != 0) 
        {
            animator.SetBool("isRunning", true); 
        }
        else { animator.SetBool("isRunning", false); }
    }
    void HandleJump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) //if player not touching surface
        {
            animator.SetBool("isJumping", true);
            return;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
        //if player touching surface and jump pressed
        if (Input.GetAxisRaw("Jump") != 0 && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
        }
    }

    void HandleAttack()
    {
        if (Input.GetAxisRaw("Fire1") == 0 || animator.GetBool("isJumping"))
        {
            return;
        }
        else if (!isAttacking)
        {
            animator.SetTrigger("Attack");
        }
    }

    void HandleSpriteFlip()
    {
        if (playerRigidbody.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (playerRigidbody.velocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void ToggleAttackHitbox() // animation event for player attack
    {
        toggleAttackHitbox = !toggleAttackHitbox;
        attackHitbox.enabled = toggleAttackHitbox;
        //Debug.Log(GetComponentInChildren<BoxCollider2D>().enabled);
    }

    void ToggleIsAttackingVariable() // animation event for player attack
    {
        isAttacking = !isAttacking;
        //Debug.Log(isAttacking);
    }

    void AttackInDirectionFacing() // animation event for player attack
    {
        playerRigidbody.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * dragSpeedOnAttack, playerRigidbody.velocity.y);
    }

    void TurnOffAttackHitboxAndVariable() // animation event for player attack
    {
        toggleAttackHitbox = false;
        attackHitbox.enabled = toggleAttackHitbox;
        isAttacking = false;
    }

    void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void Die()
    {
        isDead = true;
    }

    public bool GetPlayerDied()
    {
        return isDead;
    }
}
