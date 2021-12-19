using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public int coins;
    private int maxCoins = 999;
    public Text coinsText;

    void Start()
    {
        PlayerPrefs.GetInt("coins");
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "X " + coins;
        if (coins < 0)
        {
            coins = 0;
        }

        if (coins >= maxCoins)
        {
            ResetCoins();
        }
    }

    public void addCoins()
    {
        coins++;
        PlayerPrefs.SetInt("coins", coins);
    }

    void ResetCoins()
    {
        coins = 0;
    }
}
