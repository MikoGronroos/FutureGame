using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessController : MonoBehaviour
{

    private int[,] ChessBoard = new int[8,8] { 
    { 2, 3, 4, 5, 6, 4, 3, 2 },
    { 1, 1, 1, 1, 1, 1, 1, 1 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0 },
    { 7, 7, 7, 7, 7, 7, 7, 7 },
    { 8, 9, 10, 11, 12, 10, 9, 8 }
    };

    private void Start()
    {
        InitializeBoard();
    }

    private bool InitializeBoard()
    {

        

        return true;
    }

}
