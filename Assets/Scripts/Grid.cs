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
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                //Все первые значения пропускаются
                if (j == 0)
                {
                    continue;
                }
                //Пустые значения пропускаются
                if (!tiles[j, i].IsBusy)
                {
                    continue;
                }

                for(int k = j - 1; k >= 0; k--)
                {
                    //Всё ещё пропускаем пустые значения
                    if(!tiles[k, i].IsBusy && k > 0)
                    {
                        continue;
                    }
                    //Если достигнута первая ячейка
                    if (!tiles[k, i].IsBusy && k == 0)
                    {
                        tiles[k, i].ReplaceTiles(tiles[j, i]);
                        break;
                    }
                    //Если встречена заполненная ячейка
                    if(tiles[k,i].IsBusy)
                    {
                        //Если ячейки совпадают
                        if(tiles[k, i].TileScore == tiles[j,i].TileScore)
                        {
                            tiles[k, i].MergeTiles(tiles[j, i]);
                        }
                        else
                        {
                            tiles[k + 1, i].ReplaceTiles(tiles[j, i]);
                        }
                        break;
                    }
                }

            }
        }
    }

    public void DoRight()
    {

        for (int i = 0; i < size; i++)
        {
            for (int j = size - 1; j >= 0; j--)
            {
                //Все первые значения пропускаются
                if (j == size - 1)
                {
                    continue;
                }
                //Пустые значения пропускаются
                if (!tiles[j, i].IsBusy)
                {
                    continue;
                }

                for (int k = j + 1; k < size; k++)
                {
                    //Всё ещё пропускаем пустые значения
                    if (!tiles[k, i].IsBusy && k < size - 1)
                    {
                        continue;
                    }
                    //Если достигнута первая ячейка
                    if (!tiles[k, i].IsBusy && k == size - 1)
                    {
                        tiles[k, i].ReplaceTiles(tiles[j, i]);
                        break;
                    }
                    //Если встречена заполненная ячейка
                    if (tiles[k, i].IsBusy)
                    {
                        //Если ячейки совпадают
                        if (tiles[k, i].TileScore == tiles[j, i].TileScore)
                        {
                            tiles[k, i].MergeTiles(tiles[j, i]);
                        }
                        else
                        {
                            tiles[k - 1, i].ReplaceTiles(tiles[j, i]);
                        }
                        break;
                    }
                }

            }
        }
    }

    public void DoUp()
    {

    }

    public void DoDown()
    {

    }

    public void GenerateRandomTilesForNextStep()
    {
        //TODO: Фикс ситуации, когда остаётся одна ячейка
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
