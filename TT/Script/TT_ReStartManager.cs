using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TT_ReStartManager : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; // 혹시 일시정지 돼 있으면 해제
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }
}
