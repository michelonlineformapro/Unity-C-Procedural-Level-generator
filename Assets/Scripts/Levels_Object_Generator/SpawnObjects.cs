using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    // Tableau des tiles
    public GameObject[] objects;

    void Start()
    {
        //Randomise les tiles du tableau entre 0 et la longeur du tableau
        int rand = Random.Range(0, objects.Length);
        //Instance de l'objet random stocke dans un gameobject parent
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, transform.rotation);
        //Chaque instance des element de la room sera enfant du prefabs LR ou LRB ou LRT ou LRBT
        //instance.transform.parent = enfant et transform = parent
        instance.transform.parent = transform;
    }

}