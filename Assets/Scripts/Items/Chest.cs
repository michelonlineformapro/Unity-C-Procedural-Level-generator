using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    //les bonus
    public GameObject keys;
    //position
    private Transform coffrePos;
    public BoxCollider2D bc2d;
    //son
    public AudioClip openSound;

    public bool chestCanBeOpen;
    public GameObject chestParticles;

    void Start()
    {
        animator = GetComponent<Animator>();
        coffrePos = GetComponent<Transform>();
        animator.SetBool("CoffreIsOpen", false);
        bc2d = GetComponent<BoxCollider2D>();
        bc2d.enabled = true;
        chestCanBeOpen = false;
        if (bc2d == null)
        {
            bc2d.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D cible)
    {
        if (cible.gameObject.tag == "Sword")
        {
            chestCanBeOpen = true;
            if (chestCanBeOpen)
            {
                TriggerOpenChest();

            }
            else
            {
                chestCanBeOpen = false;
            }
        }
    }

    public void TriggerOpenChest()
    {
        bc2d.enabled = false;
        animator.SetBool("CoffreIsOpen", true);
        Instantiate(chestParticles, transform.position, transform.rotation);
        if (openSound)
        {
            AudioSource.PlayClipAtPoint(openSound, transform.position);
        }
        bc2d.isTrigger = false;
        SpawnItems();
    }

    void SpawnItems()
    {
        Instantiate(keys, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
    }
}





