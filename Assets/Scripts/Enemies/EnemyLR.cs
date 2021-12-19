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
    }
}




