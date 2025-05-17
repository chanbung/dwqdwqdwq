using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TT_ScoreManager : MonoBehaviour
{
    public static TT_ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI highScoreTextInGameOver; // 게임오버 패널 최고 점수


    private int score = 0;
    private int highScore = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreTextInGameOver != null)
            highScoreTextInGameOver.text = "최고: " + highScore;
    }

    public void AddScore(int amount)
    {
        score += amount;

        // 콘솔 출력
        Debug.Log("현재 점수: " + score);

        // UI 업데이트
        scoreText.text = "점수: " + score;
        scoreText2.text = "점수: " + score;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            if (highScoreTextInGameOver != null)
                highScoreTextInGameOver.text = "최고: " + highScore;
        }
    }
}
