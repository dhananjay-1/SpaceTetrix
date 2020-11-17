
using UnityEngine;

public class TetrixBoard : MonoBehaviour
{
    const int totalTetrixPieces = 7;
    public GameObject[] tetrixPieces = new GameObject[totalTetrixPieces]; // for assigning in the inspector as static variables are not accessible in inspector so had to create a public version of this variable so that its value can be assigned to the static variable in Start()
    static GameObject[] staticTetrixPieces;
    public GameObject tetrixLine;
    static GameObject staticTetrixLine;

    static Vector3 spawnPosition = new Vector3(10, 20, 0);
    static Vector3 lineCompleteLoc = new Vector3(1, 0, -1);

    const int boardHeight = 20;
    const int boardWidth = 20;  // it is equal to the x pos of right boundary of board
    static int[] freqInEachRow = new int[boardHeight+1];
    static bool[,] board = new bool[boardHeight + 1, boardWidth + 1];  // to keep track which position is occupied and which is empty

    // Start is called before the first frame update
    void Start()
    {
        staticTetrixPieces = tetrixPieces;
        staticTetrixLine = tetrixLine;
       
        for(int i=0; i<=boardHeight; i++)
        {
            freqInEachRow[i] = 0;

            for(int j=0; j<=boardWidth; j++)
            {
                board[i, j] = false;

                if (j == 0 || j==boardWidth || i==0)
                {
                    board[i, j] = true;
                }
                
            }
        }

        SpawnNewPiece();
    }

    private static void addBlockInRow(int idx)
    {
        if(idx>boardHeight || idx<0)
        {
            Debug.LogError("Invalid index encountered in addBlockInRow() function in TetrixBoard.cs");
            return;
        }
        freqInEachRow[idx]++;

        if(freqInEachRow[idx]==boardWidth-1)
        {
            print("Line complete at row = "+idx+" with block frequency "+freqInEachRow[idx]);
            
            lineCompleteLoc.y = idx;
            SpawnNewLine();
        }
    }

    public static bool checkBlock(int col, int row)
    {
        //print("psition checked at x = " + col + ",y = " + row);

        if (row > boardHeight)  // piece may be spawned above board height
        {
            return false;
        }

        if(col>boardWidth || row<0 || col < 0)
        {
            //Debug.LogError("Index : "+row+" , "+col+" Invalid index encountered in checkBlock() function in TetrixBoard.cs");
            return true;
        }

        //print("psition checked at x = " + col + ",y = " + row + " status = "+board[row,col]);
        return board[row, col];


    }

    public static void addBlock(int col, int row, bool spawnNow)
    {
        if (row >= boardHeight)
        {
            // Stop the game
            print("Game stopped");
            return;
        }

        if (row > boardHeight || col > boardWidth || row < 0 || col < 0)
        {
            Debug.LogError("Invalid index encountered in addBlock() function in TetrixBoard.cs");
            return;
        }
        board[row, col] = true;
        addBlockInRow(row);

        if (spawnNow)
        {
            Debug.Log("spawned after adding the block : x = "+col+" y = "+row);

            SpawnNewPiece();
        }
    }


    // Update is called once per frame
    void Update()
    {
      
    }

    private static void SpawnNewPiece()
    {
        //Debug.Log("spawning new tetrix piece");

        int idx = Random.Range(0, staticTetrixPieces.Length);
        Instantiate(staticTetrixPieces[idx], spawnPosition, Quaternion.identity);
    }

    private static void SpawnNewLine()
    {
        Instantiate(staticTetrixLine, lineCompleteLoc, Quaternion.identity);
    }
}
