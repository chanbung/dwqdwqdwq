using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TT_PauseManager : MonoBehaviour
{
    [Header("일시정지 시 활성화할 패널")]
    [SerializeField] private GameObject pausePanel;  // Canvas → TopBarPanel (일시정지 메뉴)

    private bool isPaused = false;

    /// <summary>
    /// PauseBtn OnClick 에 연결
    /// </summary>
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
            Pause();
        else
            Resume();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    private void Resume()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
