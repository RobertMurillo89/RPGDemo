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
    }

    void FixedUpdate()
    {
        //player movement output
        rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);

        PlayerSpriteAnimator();
    }


    void PlayerSpriteAnimator()
    {
        GetComponent<Animator>().SetFloat("MoveX", rb.velocity.x);
        GetComponent<Animator>().SetFloat("MoveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            GetComponent<Animator>().SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            GetComponent<Animator>().SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));

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
