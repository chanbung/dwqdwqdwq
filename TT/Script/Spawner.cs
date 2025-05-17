using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Tetris;    // 에디터에서 7개 프리팹 할당
    private GameObject nextBlockPrefab; // 다음 블록을 저장할 변수
    public Transform nextBlockPosition; // 다음 블록을 보여줄 위치
    private GameObject currentPreviewBlock; // 현재 미리보기 블록



    void Start()
    {
        // 처음 시작할 때 nextBlockPrefab 먼저 뽑기
        nextBlockPrefab = Tetris[Random.Range(0, Tetris.Length)];
        NewTetris();
    }

    public void NewTetris()
    {
        // 1️⃣ 블록 생성될 위치 확인    
        Vector2 spawnPos = transform.position;
        // 2️⃣ 생성 위치를 Grid 좌표로 변환 (기준 좌표와 unitSize를 사용)
        int x = Mathf.FloorToInt((spawnPos.x - (-2.5f)) / 0.4f);
        int y = Mathf.FloorToInt((spawnPos.y - (-4f)) / 0.4f);

        // 3️⃣ 해당 위치에 이미 블록이 있으면 → 게임오버
        if (x >= 0 && x < 10 && y >= 0 && y < 20)
        {
            if (TetrisBlock.grid[x, y] != null)
            {
                TT_GameOverManager.Instance.GameOver();
                return;
            }
        }    
        // 다음 블록 생성
        GameObject obj = Instantiate(nextBlockPrefab, transform.position, Quaternion.identity);
        // 다음 블록 다시 랜덤으로 뽑기
        nextBlockPrefab = Tetris[Random.Range(0, Tetris.Length)];
        // 이전 미리보기 블록이 있으면 삭제
        if (currentPreviewBlock != null)
        {
            Destroy(currentPreviewBlock);
        }

        // 새로운 미리보기 블록 생성
        currentPreviewBlock = Instantiate(nextBlockPrefab, nextBlockPosition.position, Quaternion.identity);
        currentPreviewBlock.transform.localScale = Vector3.one * 0.25f;
        currentPreviewBlock.GetComponent<TetrisBlock>().enabled = false;

    }
}