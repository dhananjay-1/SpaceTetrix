using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    TetrixBoard tetrixBoard;
    float fixedMoveDownTime = 1f;
    float speedUpMoveDownTime = 0.05f;
    float currMoveDownTime;
    bool pieceStopped = false;

    int childBlocks = 4;
    GameObject[] blocks = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {      
        for(int i=0; i<childBlocks; i++)
        {
            blocks[i] = transform.GetChild(i).gameObject;
        }

        currMoveDownTime = fixedMoveDownTime;
        MoveDown();
    }

    private void MoveDown()
    {
        Vector3 origPos = transform.position;
        transform.position -= new Vector3(0, 1, 0);

        for(int i=0; i<blocks.Length; i++)
        {
            int x = Mathf.RoundToInt(blocks[i].transform.position.x);
            int y = Mathf.RoundToInt(blocks[i].transform.position.y);
            if(TetrixBoard.checkBlock(x,y)) // checks whether position (x,y) is occupied or not
            {
                transform.position = origPos;  // undo the down movement
                StopPiece();
                return;
            }
        }

        Invoke("MoveDown", currMoveDownTime);
    }

    private void StopPiece()
    {
        pieceStopped = true;

        for(int i=0; i<blocks.Length; i++)
        {
            int x = Mathf.RoundToInt(blocks[i].transform.position.x);
            int y = Mathf.RoundToInt(blocks[i].transform.position.y);

            TetrixBoard.addBlock(x, y, i==blocks.Length-1);   // occupy position (x,y)
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pieceStopped)
        {
            return;
        }

        Move();
        Rotate();
        SpeedUpDownMovement();
    }

    private void SpeedUpDownMovement()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currMoveDownTime = speedUpMoveDownTime;
        }
        else
        {
            currMoveDownTime = fixedMoveDownTime;
        }
    }

    private void Rotate()
    {
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 rotate = new Vector3(0, 0, 90);

            transform.Rotate(rotate);


            for (int i = 0; i < blocks.Length; i++)
            {
                int x = Mathf.RoundToInt(blocks[i].transform.position.x);
                int y = Mathf.RoundToInt(blocks[i].transform.position.y);

                if (TetrixBoard.checkBlock(x, y)) // checks whether position (x,y) is occupied or not
                {
                    transform.Rotate(-rotate);  // undo rotation
                }
            }

        }

        
    }

    private void Move()
    {
        Vector3 move = new Vector3(1, 0, 0);
        int moveFactor = 0;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveFactor = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveFactor = -1;
        }

        if (moveFactor!=0)
        {
            //print("move function called");
            transform.position += move * moveFactor;

            for (int i = 0; i < blocks.Length; i++)
            {
                //print("insided loop");
                int x = Mathf.RoundToInt(blocks[i].transform.position.x);
                int y = Mathf.RoundToInt(blocks[i].transform.position.y);

                if (TetrixBoard.checkBlock(x, y)) // checks whether position (x,y) is occupied or not
                {
                    transform.position -= move * moveFactor;  // undo movement
                    break;
                }

            }

        }
        
    }
}
