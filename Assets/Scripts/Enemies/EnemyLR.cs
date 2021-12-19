using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLR : MonoBehaviour
{
    float startPosition;
    float endPosition;
    public float moveSpeed;
    public bool moveRight = true;
    public int unitToMove;
    public Rigidbody2D rb2d;

    private PlayerMoves player;
    private EnemyHealth health;

    //knockBack
    public float knockBack = 3f; // force x et  y  = 3
    public float knockBackCount = 0f;// = 0
    public float knockbackLenght = 0.25f;// temps 0.25s
    public bool knockFromRight;

    //KnockBack Fx

    void Start()
    {
        health = GetComponent<EnemyHealth>();
        player = FindObjectOfType<PlayerMoves>();
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position.x;
        endPosition = startPosition + unitToMove;
    }


    void Update()
    {
        EnemyMove();

    }

    void EnemyMove()
    {
        if (moveRight && unitToMove >= 1)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (transform.position.x >= endPosition && unitToMove >= 1)
        {
            moveRight = false;
            this.transform.localScale = new Vector3((transform.localScale.x == 1f) ? -1f : 1f, 1f);
        }
        if (!moveRight && unitToMove >= 1)
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;

        }
        if (transform.position.x <= startPosition && unitToMove >= 1)
        {
            moveRight = true;
            this.transform.localScale = new Vector3((transform.localScale.x == 1f) ? 1f : 1f, 1f, 1f);
        }


        //KnockBack
        //knockback droite et gauche
        if (knockBackCount <= 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
        }
        else
        {
            if (knockFromRight)
            {
                rb2d.velocity = new Vector2(-knockBack, knockBack);
            }
            if (!knockFromRight)
            {
                rb2d.velocity = new Vector2(knockBack, knockBack);
            }
            knockBackCount -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {


            if (moveRight)
            {
                knockBackRight();

            }

            if (!moveRight)
            {
                knockBackLeft();
            }

        }
    }

    void knockBackRight()
    {
       
        rb2d.velocity = new Vector2(-knockBack, knockBack);
        health.killEnemy();
    }

    void knockBackLeft()
    {
       
        rb2d.velocity = new Vector2(knockBack, knockBack);
        health.killEnemy();
    }

}



