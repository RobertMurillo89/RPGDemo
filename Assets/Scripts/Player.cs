using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour//, IPersist
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

    public static Player instance;

    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool canMove = true;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        //player directional input check
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

    }

    void FixedUpdate()
    {
        PlayerSpriteAnimator();

        if (canMove)
        {
            //player movement output
            rb.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }



    }


    void PlayerSpriteAnimator()
    {
        GetComponent<Animator>().SetFloat("MoveX", rb.velocity.x);
        GetComponent<Animator>().SetFloat("MoveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                GetComponent<Animator>().SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                GetComponent<Animator>().SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, .75f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -.75f, 0f);
    }

    //public void Save()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void Load()
    //{
    //    throw new System.NotImplementedException();
    //}
}

