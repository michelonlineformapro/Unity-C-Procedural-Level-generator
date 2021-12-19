using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private PlayerMoves player;
    private LevelGeneration levelGen;

    public GameObject spawnParticles;
    public AudioClip spawnSound;

    public bool addFx = false;

    void Start()
    {
        player = FindObjectOfType<PlayerMoves>();
        levelGen = FindObjectOfType<LevelGeneration>();

        player.gameObject.SetActive(false);
        Invoke("displayFx", .75f);

    }


    void displayFx()
    {
        player.gameObject.SetActive(true);
        Instantiate(spawnParticles, transform.position, transform.rotation);
        if (spawnSound)
        {
            AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        }
    }
}
