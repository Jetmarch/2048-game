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

    /*public void GenerateRandomTilesForNextStep(int countOfNewTiles = 2)
    {
        int countOfGeneratedTiles = 0;
        foreach(var tile in tiles)
        {
            if (tile.IsBusy) continue;

            if (Random.value > 0.5)
            {
                tile.TileScore = 2;
                countOfGeneratedTiles++;
                if (countOfGeneratedTiles >= countOfNewTiles)
                {
                    break;
                }
            }
        }

        if(countOfGeneratedTiles < countOfNewTiles)
        {
            GenerateRandomTilesForNextStep(countOfNewTiles - countOfGeneratedTiles);
        }
    }*/

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
