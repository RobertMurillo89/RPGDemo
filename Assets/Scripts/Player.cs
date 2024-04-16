using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPersist
{
    [Header("----- Player Stats -----")]
    public float playerSpeed;

    [Header("----- Player Components -----")]
    public SpriteRenderer spriteRenderer;

    //Player private components
    private Rigidbody2D rb;
    private Vector2 playerDirection;

    //Player animations
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //player directional input check
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        SpriteFlipper();
    }

    void FixedUpdate()
    {
        //player movement output
        rb.velocity = new Vector2(playerDirection.x* playerSpeed, playerDirection.y* playerSpeed);

        PlayerSpriteAnimator();
    }

    void SpriteFlipper()
    {
        // Flip the sprite based on movement direction
        if (playerDirection.x < 0)
        {
            spriteRenderer.flipX = true; // Flip the sprite when moving left
            Debug.Log("Flip left");
        }
        else if (playerDirection.x > 0)
        {
            spriteRenderer.flipX = false; // Unflip the sprite when moving right
            Debug.Log("Flip Right");

        }
    }

    void PlayerSpriteAnimator()
    {
        rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
            if (rb.velocity.sqrMagnitude > 0)
            {
                GetComponent<Animator>().Play("Player_walk");
            }
            else
            {
                GetComponent<Animator>().Play("Player_Idle");
            }
    }

    public void Save()
    {
        throw new System.NotImplementedException();
    }

    public void Load()
    {
        throw new System.NotImplementedException();
    }
}

