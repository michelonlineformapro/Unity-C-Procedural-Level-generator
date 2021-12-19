using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour
{
    public AudioClip playerDamageSound;
    private Camera2D cameraScript;
    private HealthManager healthScript;

    void Start()
    {

        cameraScript = FindObjectOfType<Camera2D>().GetComponent<Camera2D>();
        healthScript = FindObjectOfType<HealthManager>();
    }


    void OnTriggerEnter2D(Collider2D cible)
    {
        if (cible.gameObject.tag == "Player")
        {
            healthScript.AddDamage();
            cameraScript.ShakeCamera(0.25f, 0.15f);
            if (playerDamageSound)
            {
                AudioSource.PlayClipAtPoint(playerDamageSound, transform.position);
            }

            var player = cible.GetComponent<PlayerMoves>();
            player.knockBackCount = player.knockbackLenght;
            if (cible.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }
        }
    }
}


