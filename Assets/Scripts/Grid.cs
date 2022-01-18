using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private GridTile[,] tiles;
    private int size;

    public Grid(int size)
    {
        this.size = size;
        tiles = new GridTile[size, size];

        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                tiles[i, j] = new GridTile();
               
            }
        }
    }

    public GridTile[,] GetTiles()
    {
        return tiles;
    }

    public void DoLeft()
    {

    }

    public void DoRight()
    {

    }

    public void DoUp()
    {

    }

    public void DoDown()
    {

    }

    public void GenerateRandomTilesForNextStep()
    {
        int countOfGeneratedTiles = 0;
        while(countOfGeneratedTiles < 2)
        {
            var rndTile = tiles[Random.Range(0, size), Random.Range(0, size)];
            if (rndTile.IsBusy) continue;

            rndTile.TileScore = 2;
            countOfGeneratedTiles++;
        }
    }

    public void DebugGridView()
    {
        string line = string.Empty;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                line += $"{tiles[i, j].TileScore} ";
            }
            Debug.Log($"{i}: {line}");
            line = string.Empty;
        }
    }
}
