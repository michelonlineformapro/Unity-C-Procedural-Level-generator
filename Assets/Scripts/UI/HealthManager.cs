using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // script
    private GameObject playerGO;
    private KillPlayer killScript;

    public GameObject gameOverPanel;
    //La vie = (entier) les coeurs
    public int life = 5;
    public int maxLife = 5;
    public int minLife = 0;
    //Status
    public bool isDead = false;
    //Dégats
    public Material hitPlayer;
    public Material defaultHitPlayer;
    public bool isInvincible = false;

    //Tableau des coeurs
    public Sprite[] heartArray;
    //Image des coeurs dans le panel
    public Image heartImage;

    //Le player
    private GameObject player;


    //Le son
    public AudioClip hurtSound;
    public AudioClip lifeSound;
    public AudioClip explodeSound;

    //Fx
    public GameObject deathParticles;


    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        killScript = FindObjectOfType<KillPlayer>();
        gameOverPanel.SetActive(false);
        //Caste du gameObject player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Player fx damage
    //Dégats Sprite renderer soit 1f d'invincibilité donc 0.1f * 10 (clignote jaune puis sprite lite par defaut en boucle pendand 1 seconde)
    public IEnumerator HitDelayPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = hitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = defaultHitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = hitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = defaultHitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = hitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = defaultHitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = hitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = defaultHitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = hitPlayer;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().material = defaultHitPlayer;

    }


    // 60 fps
    void Update()
    {
        //La mort & evite une vie negative
        if (life <= 0)
        {
            life = 0;
            KillPlayer();
            Invoke("OpenGameOverPanel", .5f);

        }

        //Eviter la vie max > 5
        if (life >= maxLife)
        {
            life = maxLife;

        }

        //Afficher les coeur et mise a jour
        heartImage.sprite = heartArray[life];
    }

    public void KillPlayer()
    {
        //Instance des particules
        Instantiate(deathParticles, transform.position, transform.rotation);
        //Le player devient inactif (je ne detruis pas l'objet car il est invoquer par la camera ???????)
        Invoke("PlayerDeath", 0.5f);

    }

    void PlayerDeath()
    {
        playerGO.SetActive(false);

        if (explodeSound)
        {
            AudioSource.PlayClipAtPoint(explodeSound, transform.position);
        }
    }

    public void AddDamage()
    {
        //Si différent de invicible
        if (!isInvincible)
        {
            isInvincible = true;
            life--;
            StartCoroutine(HitDelayPlayer());
            if (hurtSound)
            {
                AudioSource.PlayClipAtPoint(hurtSound, transform.position);
            }
            Invoke("ResetInvincible", 1f);
        }
    }

    //Invincible
    public void ResetInvincible()
    {
        isInvincible = false;
    }

    /*Donne 1 point de vie*/
    public void GiveLifePlayer()
    {
        life++;
        if (lifeSound)
        {
            AudioSource.PlayClipAtPoint(lifeSound, transform.position);
        }
    }

    void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
