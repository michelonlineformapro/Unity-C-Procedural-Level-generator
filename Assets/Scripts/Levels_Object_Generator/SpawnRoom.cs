using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    //Script
    private LevelGeneration levelGen;

    private void Start()
    {
        levelGen = FindObjectOfType<LevelGeneration>();
    }

    // 60fps
    void Update()
    {
        //Physics2D + cercle (collider2d)
        //3 paramètres 1 = position de room + 2 radius = rayon + masque de detection (layermask)
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);

        //On creer un cercle qui detecte si il ya une piece ou non
        //si y en a pas roomDetection  == null
        //on instance une nouvelle piece a l'aide de la methode de la classe 
        //LevelGeneration.levelGen 
        //On appel cette condition quand le path est terminer c a dire stopGeneration = true
        if (roomDetection == null && levelGen.stopGeneration == true)
        {
            //on stanck dans un entier un random des pieces du tableu rooms en 0 et sa longueur totale
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
