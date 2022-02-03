using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : ICloneable
{
    public int X;
    public int Y;
    public int TileScore;

    //Ячейка считается занятой, если в ней есть значение больше нуля
    public bool IsBusy { 
        get
        {
            return TileScore > 0 ? true : false;
        }
        private set { } }
    public GridTile(int x, int y, int score = 0)
    {
        TileScore = score;
        X = x;
        Y = y;
    }

    public void MergeTiles(GridTile other)
    {
        TileScore += other.TileScore;
        other.TileScore = 0;
    }

    public void ReplaceTiles(GridTile other)
    {
        if(this.X == other.X && this.Y == other.Y)
        {
            return;
        }

        TileScore = other.TileScore;
        other.TileScore = 0;
    }

    public object Clone()
    {
        return new GridTile(X, Y, TileScore);
    }
}
