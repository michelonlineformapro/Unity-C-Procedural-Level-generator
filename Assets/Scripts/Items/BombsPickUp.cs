using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsPickUp : MonoBehaviour
{

    private BombsManager bombScript;

    //Son
    public AudioClip bombsSound;
    public GameObject plusUnObject;


    void Start()
    {
        bombScript = FindObjectOfType<BombsManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bombScript.addBombs();
            Instantiate(plusUnObject, new Vector3(transform.position.x + 0.5f,
                transform.position.y + 0.5f,
                transform.position.z), Quaternion.identity);
            if (bombsSound)
            {
                AudioSource.PlayClipAtPoint(bombsSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
