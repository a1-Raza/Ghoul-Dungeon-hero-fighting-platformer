using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    [Header("Sounds")]
    [SerializeField] AudioClip footstepSound;
    [SerializeField] AudioClip jumpSound;

    [Header("Attack")]
    //[SerializeField] int regularAttackDamage = 50;
    //[SerializeField] int specialAttackDamage = 150;
    [SerializeField] float attackMultiplier = 1.0f;

    [SerializeField] float specialAttackCooldownTime = 6;
    [SerializeField] float dashCooldownTime = 3;

    [SerializeField] GameObject attackHitbox;
    [SerializeField] GameObject specialAttackHitbox;

    Rigidbody2D playerRigidbody;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    Animator animator;
    AudioSource audioSource;
    DamageDealer damageDealer;
    Cooldown dashCooldown;
    Cooldown specialAttackCooldown;

    bool isAttacking = false;
    bool isDashing = false;
    bool isDead = false;
    bool toggleAttackHitbox = false;
    bool toggleBodyCollider = false;

    //bool isSpecialAttackCooldown = false;
    //bool isDashCooldown = false;

    bool touchingGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponentInParent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        dashCooldown = new Cooldown(dashCooldownTime);
        specialAttackCooldown = new Cooldown(specialAttackCooldownTime);


        damageDealer = GetComponentInChildren<DamageDealer>();
        attackHitbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            return;
        }

        touchingGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || feetCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")) || feetCollider.IsTouchingLayers(LayerMask.GetMask("Gate"));

        if (isDashing)
        {
            Physics2D.IgnoreLayerCollision(7, 8, true); // 7 is player layer, 8 is enemy layer
        }
        else
        {
            Physics2D.IgnoreLayerCollision(7, 8, false);
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
        if (!touchingGround) //if player not touching surface
        {
            animator.SetBool("isJumping", true);
            return;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
        //if player touching surface and jump pressed
        if (touchingGround)
        {
            if (Input.GetAxisRaw("Jump") != 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpSpeed);
                if (!jumpSound) { return; }
                PlaySound(jumpSound);
            }
        }
    }

    void HandleAttack()
    {
        if (!touchingGround)
        {
            return;
        }
        
        if (!isAttacking)
        {
            if (Input.GetAxisRaw("Fire1") != 0)
            {
                //damageDealer.SetDamageToDeal(regularAttackDamage);
                animator.SetTrigger("Attack");
            }
            else if ((Input.GetAxisRaw("Fire2") != 0) && !IsInSpecialAttackCooldown())
            {
                //damageDealer.SetDamageToDeal(specialAttackDamage);
                animator.SetTrigger("specialAttack");
                StartCoroutine(specialAttackCooldown.StartCoolDown());
                
            }
            else if ((Input.GetAxisRaw("Fire3") != 0) && !IsInDashCooldown())
            {
                animator.SetTrigger("Dash");
                StartCoroutine(dashCooldown.StartCoolDown());
            }
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

    void ToggleAttackHitbox(int hitboxNum) // animation event for player attack
    {
        //if (hitboxNum != 0 || hitboxNum != 1) { return; }

        toggleAttackHitbox = !toggleAttackHitbox;

        if (hitboxNum == 0) { attackHitbox.SetActive(toggleAttackHitbox); }
        else if (hitboxNum == 1) { specialAttackHitbox.SetActive(toggleAttackHitbox); }

        //Debug.Log(GetComponentInChildren<BoxCollider2D>().enabled);
    }

    void ToggleIsAttackingVariable() // animation event for player attack
    {
        isAttacking = !isAttacking;
        //Debug.Log(isAttacking);
    }

    void ToggleIsDashing()
    {
        isDashing = !isDashing;
    }

    void ToggleBodyCollider()
    {
        toggleBodyCollider = !toggleBodyCollider;
        bodyCollider.enabled = !toggleBodyCollider;
    }

    void AttackInDirectionFacing(float dragSpeedOnAttack) // animation event for player attack
    {
        playerRigidbody.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * dragSpeedOnAttack, playerRigidbody.velocity.y);
    }

    void TurnOffAttackHitboxAndVariable() // animation event for player attack
    {
        toggleAttackHitbox = false;
        attackHitbox.SetActive(toggleAttackHitbox);
        specialAttackHitbox.SetActive(toggleAttackHitbox);
        isAttacking = false;
        toggleBodyCollider = false;
        isDashing = false;
        bodyCollider.enabled = true;
    }

    void PlaySound(AudioClip audioClip)
    {
        if (!audioClip) { return; }
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

    public float GetAttackMultiplier()
    {
        return attackMultiplier;
    }

    public void SetAttackMultiplier(float newAttackMultiplier)
    {
        attackMultiplier = newAttackMultiplier;
    }

    public void AddToAttackMultiplier(float addedAttackMultiplier)
    {
        attackMultiplier += addedAttackMultiplier;
    }

    public bool IsInDashCooldown()
    {
        return dashCooldown.IsInCooldown();
    }

    public bool IsInSpecialAttackCooldown()
    {
        return specialAttackCooldown.IsInCooldown();
    }
}
