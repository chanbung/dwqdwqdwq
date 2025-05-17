using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 1f;
    public static int height = 20;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];

    //여기부터 수정
    // 기준 좌표와 단위 간격
    Vector2 gridOrigin = new Vector2(-2.5f, -4f); // 좌측 하단이 (0,0)
    float unitSize = 0.4f;                        // 한 칸의 크기
    //여기까지 수정


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-0.4f,0,0);
            if(!ValidMove())
            {
                transform.position -= new Vector3(-0.4f,0,0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.4f,0,0);
             if(!ValidMove())
            {
                transform.position -= new Vector3(0.4f,0,0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
             //회전하기
             transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
             if(!ValidMove())
             {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
             }
        }
        if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/10 : fallTime))
        {
            transform.position += new Vector3(0,-0.4f,0);
             if(!ValidMove())
            {
                transform.position -= new Vector3(0,-0.4f,0);
                AddToGrid();
                CheckForLines(); // 줄이 블록들로 꽉 찼는지 확인인
                this.enabled = false;
                FindObjectOfType<Spawner>().NewTetris();
            }
            previousTime = Time.time;
        }
    }

    void CheckForLines()
    {
        int linesDeleted = 0;  // 지운 줄 개수 기록

        for(int i=height-1; i>=0; i--) //테트리스 블록을 맨 윗줄부터 아래까지 검색한다.
        {
            if(HasLine(i)) //줄이 블록들로 꽉차 있을경우
            {
                DeleteLine(i); //그 줄 삭제하고
                RowDown(i); // 줄 한 칸 내리기기
                linesDeleted++;  // 줄 하나 지웠다!
            }
        }

        if (linesDeleted > 0)
        {
            TT_ScoreManager.Instance.AddScore(linesDeleted * 100);
            TT_CoinManager.Instance.AddCoins(linesDeleted); // 이거만 있으면 UI까지 자동 업데이트됨
        }
    }

    bool HasLine(int i) // 줄이 블록으로 꽉 차 있는지 확인
    {
        for(int j = 0; j < width; j++) //줄을 0~9까지 검색색
        {
            if(grid[j,i] == null) //비어있으면
            return false; //false 리턴턴
        }
        return true; //줄이 꽉차있으면 true 리턴
    }

    void DeleteLine(int i)
    {
        for(int j=0; j< width; j++)
        {
            Destroy(grid[j,i].gameObject);
            grid[j,i]=null;
        }
    }

    void RowDown(int i)
    {
        for(int y=i; y< height; y++)
        {
            for(int j=0; j<width; j++)
            {
                if(grid[j,y] != null) //윗줄을 아랫줄로 복사하는 과정
                {
                    grid[j,y-1] = grid[j,y];
                    grid[j,y] = null;
                    grid[j,y-1].transform.position -= new Vector3(0,0.4f,0);
                }
            }
        }
    }
    // void AddToGrid()
    // {
    //     foreach(Transform children in transform)
    //     {
    //         int roundX = Mathf.RoundToInt(children.transform.position.x);
    //         int roundY = Mathf.RoundToInt(children.transform.position.y);

    //         grid[roundX, roundY] = children;
    //     }
    // }

    // bool ValidMove()
    // {
    //     foreach(Transform children in transform)
    //     {
    //         int roundX = Mathf.RoundToInt(children.transform.position.x);
    //         int roundY = Mathf.RoundToInt(children.transform.position.y);

    //         if(roundX <0 || roundX >= width || roundY < 0 || roundY >= height)
    //         {
    //             return false;
    //         }

    //         if(grid[roundX, roundY] != null)
    //             return false;
    //     }

    //     return true;
    // }

    //여기부터 수정정
    int WorldToGridX(float worldX)
{
    return Mathf.FloorToInt((worldX - gridOrigin.x) / unitSize);
}

int WorldToGridY(float worldY)
{
    return Mathf.FloorToInt((worldY - gridOrigin.y) / unitSize);
}

void AddToGrid()
{
    foreach (Transform children in transform)
    {
        int x = WorldToGridX(children.position.x);
        int y = WorldToGridY(children.position.y);

        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            grid[x, y] = children;
        }
        else
        {
            Debug.LogWarning($"[AddToGrid] 범위 밖: ({x},{y}) → 실제: {children.position}");
        }
    }
}

bool ValidMove()
{
    foreach (Transform children in transform)
    {
        int x = WorldToGridX(children.position.x);
        int y = WorldToGridY(children.position.y);

        if (x < 0 || x >= width || y < 0 || y >= height)
            return false;

        if (grid[x, y] != null)
            return false;
    }

    return true;
}

    //여기까지 수정
}