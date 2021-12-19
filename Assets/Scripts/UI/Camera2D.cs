using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    private Vector2 velocity;
    private float smoothTimeX = 0;
    private float smoothTimeY = 0;
    public GameObject player;

    public LevelGeneration levelGen;


    //limites
    public bool bounds = true;
    public Vector3 minCamPos;
    public Vector3 maxCamPos;

    //shake
    private float shakeTime;
    private float shakeForce;


    void Start()
    {


    }

    void FixedUpdate()
    {
        if (levelGen.stopGeneration == true)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

            transform.position = new Vector3(posX, posY, transform.position.z);

            if (bounds)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamPos.x, maxCamPos.x),
                   Mathf.Clamp(transform.position.y, minCamPos.y, maxCamPos.y),
                   Mathf.Clamp(transform.position.z, minCamPos.z, maxCamPos.z));
            }

            if (shakeTime >= 0)
            {
                Vector2 ShakePos = Random.insideUnitCircle * shakeForce;
                transform.position = new Vector3(transform.position.x + ShakePos.x,
                    transform.position.y + ShakePos.y, transform.position.z);

                shakeTime -= Time.deltaTime;
            }
        }

    }

    public void ShakeCamera(float shakePower, float shakeDuration)
    {
        shakeForce = shakePower;
        shakeTime = shakeDuration;
    }

    public void SetMinCamPosition()
    {
        minCamPos = gameObject.transform.position;
    }

    public void SetMaxCamPosition()
    {
        maxCamPos = gameObject.transform.position;
    }
}



