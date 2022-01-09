using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseItems : MonoBehaviour
{
    // Les variables
    //Tableau des bonnus
    public GameObject[] spawnBonus;
    //Nombre de bonus
    private int nbrBonus = 4;
    //Position de l'objet
    public Transform vasePos;
    //Particule fx
    public GameObject vaseparticles;
    //son
    public AudioClip explodeSound;
    //Camera shake
    private Camera2D camScript;

    void Start()
    {
        //Recup du script de la camera
        camScript = FindObjectOfType<Camera2D>().GetComponent<Camera2D>();
        vasePos = GetComponent<Transform>();
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
        Instantiate(vaseparticles, transform.position, transform.rotation);
        Destroy(gameObject);
        if (explodeSound)
        {
            AudioSource.PlayClipAtPoint(explodeSound, transform.position);
        }
        //camScript.ShakeCamera(0.25f, 0.15f);
        SpawnItems();
    }

    void SpawnItems()
    {
        //random les items
        var p = vasePos;
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
