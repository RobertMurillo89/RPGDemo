using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float playerSpeed;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(directionX, directionY).normalized;
        SpriteFlipper();
    }

    void FixedUpdate()
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

    void SpriteFlipper()
    {
        // Flip the sprite based on movement direction
        if (playerDirection.y < 0)
        {
            spriteRenderer.flipX = true; // Flip the sprite when moving left
        }
        else if (playerDirection.x > 0)
        {
            spriteRenderer.flipX = false; // Unflip the sprite when moving right
        }
    }
}

