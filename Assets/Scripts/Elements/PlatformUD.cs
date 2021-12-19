using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUD : MonoBehaviour
{
    private float startPosition;
    private float endPosition;
    public float moveSpeed;
    public bool moveUp = true;
    public int unitToMove;

    void Start()
    {
        startPosition = transform.position.y;
        endPosition = startPosition + unitToMove;
    }

    void Update()
    {
        PlatformeUpDown();
    }

    void PlatformeUpDown()
    {
        if (moveUp)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (transform.position.y >= endPosition)
        {
            moveUp = false;
        }
        if (!moveUp)
        {
            transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (transform.position.y <= startPosition)
        {
            moveUp = true;
        }
    }
}

