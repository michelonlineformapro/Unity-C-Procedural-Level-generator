using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth = 1;
    public GameObject deathFX;
    //les bonus
    public GameObject[] spawnBonus;
    private int nbrBonus = 5;
    //position
    public Transform enemyPos;
    public AudioClip enemyDeathSound;
    private Camera2D cameraScript;
    private Rigidbody2D rb2d;
    private EnemyLR enemyMove;

    private PlayerMoves player;

    //fx
    //Dégats
    public Material hitEnemy;
    public Material defaultHitEnemy;
    public GameObject enemy;


    void Start()
    {
        cameraScript = FindObjectOfType<Camera2D>();
        rb2d = GetComponent<Rigidbody2D>();
        enemyMove = GetComponent<EnemyLR>();
        player = FindObjectOfType<PlayerMoves>();

    }


    void Update()
    {
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            Instantiate(deathFX, transform.position, transform.rotation);
            Destroy(gameObject);
            SpawnItems();
            cameraScript.ShakeCamera(0.25f, 0.15f);

            if (enemyDeathSound)
            {
                AudioSource.PlayClipAtPoint(enemyDeathSound, transform.position);
            }
        }

    }



    public void SpawnItems()
    {
        //random les items
        var p = enemyPos;
        for (int c = 0; c < nbrBonus; c++)
        {
            p.TransformPoint(0, 10f, 0);
            GameObject coinPrefab = Instantiate(spawnBonus[c], new Vector3(p.transform.position.x,
                p.transform.position.y + 1f, p.transform.position.z), Quaternion.identity) as GameObject;

            coinPrefab.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Random.Range(-200, 100));
            coinPrefab.GetComponent<Rigidbody2D>().AddForce(Vector3.up * Random.Range(200, 200));
        }
    }

    public IEnumerator HitDelayPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = hitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = defaultHitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = hitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = defaultHitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = hitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = defaultHitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = hitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = defaultHitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = hitEnemy;
        yield return new WaitForSeconds(0.1f);
        enemy.GetComponent<SpriteRenderer>().material = defaultHitEnemy;

    }


    public void killEnemy()
    {
        if (enemyDeathSound)
        {
            AudioSource.PlayClipAtPoint(enemyDeathSound, transform.position);
        }
        StartCoroutine(HitDelayPlayer());
        Invoke("hitTheEnemy", .5f);
    }

    public void hitTheEnemy()
    {

        enemyHealth--;

    }
}




