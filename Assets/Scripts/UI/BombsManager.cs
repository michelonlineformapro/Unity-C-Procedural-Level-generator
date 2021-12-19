using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombsManager : MonoBehaviour
{
    public int nbrBombs = 5;
    private int maxNbrBombs = 20;

    public Text bombsCounterText;

    void Start()
    {
        bombsCounterText.text = " X " + nbrBombs;
    }


    void Update()
    {
        bombsCounterText.text = " X " + nbrBombs;
        if (nbrBombs >= maxNbrBombs)
        {
            nbrBombs = maxNbrBombs;
        }

        if (nbrBombs <= 0)
        {
            nbrBombs = 0;
        }
    }

    public void addBombs()
    {
        nbrBombs += 5;
    }

    public void removeBombs()
    {
        nbrBombs--;
    }
}

