using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickUp : MonoBehaviour
{
    //Coin manager
    private CoinsManager coinScript;

    //Son
    public AudioClip coinSound;
    public GameObject plusUnObject;


    void Start()
    {
        coinScript = FindObjectOfType<CoinsManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinScript.addCoins();
            Instantiate(plusUnObject, new Vector3(transform.position.x + 0.5f,
                transform.position.y + 0.5f,
                transform.position.z), Quaternion.identity);
            if (coinSound)
            {
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}