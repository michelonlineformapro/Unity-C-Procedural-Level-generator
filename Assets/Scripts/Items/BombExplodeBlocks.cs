using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplodeBlocks : MonoBehaviour
{
    public BoxCollider2D bc2d;

    public GameObject bombsParticles;
    public AudioClip bombsSound;
    private Rigidbody2D rb2d;
    public float roolSpeed = 2f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d.enabled = false;
    }

    IEnumerator triggerBombs()
    {
        yield return new WaitForSeconds(2f);
        bc2d.enabled = true;
        rb2d.AddForce(Vector2.right * roolSpeed);
        Instantiate(bombsParticles, transform.position, transform.rotation);
        if (bombsSound)
        {
            AudioSource.PlayClipAtPoint(bombsSound, transform.position);
        }
        Destroy(gameObject, 0.5f);

        
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            //Debug.Log("cool");        
            Destroy(collision.gameObject);         
        }
    }


    void Update()
    {
        StartCoroutine(triggerBombs());
    }



}
