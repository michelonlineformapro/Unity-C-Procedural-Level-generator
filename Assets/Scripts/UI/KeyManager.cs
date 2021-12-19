using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public int nbrKey = 0;
    private int maxNbrKey = 1;

    public Text keyCounterText;

    void Start()
    {
        keyCounterText.text = " X " + nbrKey;
    }

    void Update()
    {
        keyCounterText.text = " X " + nbrKey;
        if (nbrKey >= maxNbrKey)
        {
            nbrKey = maxNbrKey;
        }

        if (nbrKey <= 0)
        {
            nbrKey = 0;
        }
    }

    public void addKey()
    {
        nbrKey++;
    }

    public void removeKey()
    {
        nbrKey--;
    }
}
