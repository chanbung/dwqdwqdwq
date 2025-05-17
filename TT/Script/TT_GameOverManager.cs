using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TT_GameOverManager : MonoBehaviour
{
    public static TT_GameOverManager Instance;

    [Header("게임오버 패널")]
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        Debug.Log("💀 게임 오버!");
        Time.timeScale = 0f; // 게임 정지
        gameOverPanel.SetActive(true);
    }
}

