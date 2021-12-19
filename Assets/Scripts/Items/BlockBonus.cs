using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBonus : MonoBehaviour
{
    //les bonus
    public GameObject[] spawnBonus;
    private int nbrBonus = 5;
    //position
    private Transform cubePos;
    public GameObject particule;
    //le son
    public AudioClip explodeSound;
    private Camera2D cameraScript;

    void Start()
    {
        cameraScript = FindObjectOfType<Camera2D>().GetComponent<Camera2D>();
        cubePos = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D cible)
    {

        if (cible.gameObject.tag == "Sword")
        {
            Destroy(gameObject);
            Explode();
        }
    }
    void Explode()
    {
        Instantiate(particule, transform.position, transform.rotation);
        Destroy(gameObject);
        if (explodeSound)
        {
            AudioSource.PlayClipAtPoint(explodeSound, transform.position);
        }
        cameraScript.ShakeCamera(0.25f, 0.15f);
        SpawnItems();
    }

    void SpawnItems()
    {
        //random les items
        var p = cubePos;
        for (int c = 0; c < nbrBonus; c++)
        {
            p.TransformPoint(0, 10f, 0);
            GameObject coinPrefab = Instantiate(spawnBonus[c], new Vector3(p.transform.position.x,
                p.transform.position.y + 1f, p.transform.position.z), Quaternion.identity) as GameObject;

            coinPrefab.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Random.Range(-200, 100));
            coinPrefab.GetComponent<Rigidbody2D>().AddForce(Vector3.up * Random.Range(200, 200));
        }
    }
}

