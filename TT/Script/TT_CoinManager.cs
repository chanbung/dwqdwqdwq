using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TT_CoinManager : MonoBehaviour
{
    public static TT_CoinManager Instance;

    public int coins = 0;

    [Header("코인 텍스트")]
    public TextMeshProUGUI topBarCoinText;
    public TextMeshProUGUI gameOverCoinText;

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else 
            Destroy(gameObject);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log($"💰 현재 코인: {coins}");

        topBarCoinText.text = coins.ToString();
        gameOverCoinText.text = coins.ToString();
        //UpdateCoinUI();
    }

    // private void UpdateCoinUI()
    // {
    //     if (topBarCoinText != null)
    //         topBarCoinText.text = coins.ToString();

    //     if (gameOverCoinText != null)
    //         gameOverCoinText.text = coins.ToString();
    // }
}
