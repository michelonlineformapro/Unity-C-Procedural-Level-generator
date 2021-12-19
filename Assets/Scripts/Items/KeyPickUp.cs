using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public AudioClip keySound;

    private KeyManager keyScript;

    //Son
    public GameObject keyParticles;
    public GameObject plusUn;

    public void Start()
    {
        keyScript = FindObjectOfType<KeyManager>();
    }

    void OnTriggerEnter2D(Collider2D cible)
    {
        if (cible.CompareTag("Player"))
        {
            keyScript.addKey();
            if (keySound)
            {
                AudioSource.PlayClipAtPoint(keySound, transform.position);
            }
            Instantiate(plusUn, transform.position, transform.rotation);
            Instantiate(keyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

