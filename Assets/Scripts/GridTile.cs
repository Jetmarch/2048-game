using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }
    public int TileScore { get; set; }

    //Ячейка считается занятой, если в ней есть значение больше нуля
    public bool IsBusy { 
        get
        {
            return TileScore > 0 ? true : false;
        }
        private set { } }
    public GridTile(int score = 0)
    {
        TileScore = score;
    }

    public void MergeCells(GridTile other)
    {
        TileScore += other.TileScore;
    }
}
