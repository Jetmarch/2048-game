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
                tiles[i, j] = new GridTile(j, i);
            }
        }
    }

    public GridTile[,] GetTiles()
    {
        return tiles;
    }

    public void DoLeft()
    {
        foreach(var tile in tiles)
        {
            //Проверка на границы
            if (tile.X <= 0) continue;
            if (!tile.IsBusy) continue;

            for(int i = tile.X; i >= 0; i--)
            {
                //Если это крайний тайл и он не занят
                if(i == 0 && !tiles[i, tile.Y].IsBusy)
                {
                    tiles[i, tile.Y].ReplaceTiles(tile);
                    break;
                }

                //Если тайл не последний и не занят
                if (!tiles[i, tile.Y].IsBusy) continue;

                //Если очки в тайлах совпадают
                if(tiles[i, tile.Y].TileScore == tile.TileScore)
                {
                    tiles[i, tile.Y].MergeTiles(tile);
                    break;
                }

                tiles[i, tile.Y].ReplaceTiles(tile);
                break;
            }
        }
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
        /*string line = string.Empty;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                line += $"{tiles[i, j].TileScore} ";
            }
            Debug.Log($"{i}: {line}");
            line = string.Empty;
        }*/

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Debug.Log($"Y: {i} X: {j} Score {tiles[j, i].TileScore}");
            }
        }
        
    }
}
