using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGO : MonoBehaviour
{

    public int delayDestroy;

    void Update()
    {
        Destroy(gameObject, delayDestroy);
    }
}
