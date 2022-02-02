using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private GridTile[,] tiles;
    private int size;

    /*
     Реализовать функцию UNDO
     -- Стэк GridTile[,] в который будет загружаться каждый ход текущее состояние GridTile[,]
     -- При нажатии на UNDO к текущему состоянию GridTile[,] будет присваиваться предыдущее значение из стэка

     Реализовать событие победы и событие проигрыша
     -- Победа наступает, когда два тайла, соединившись, образуют сумму 2048
     -- Поражение наступает, когда при следующем любом ходе не происходит ни движение тайлов, ни их соединение
     */

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

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                //Все первые значения пропускаются
                if (i == 0)
                {
                    continue;
                }
                //Пустые значения пропускаются
                if (!tiles[j, i].IsBusy)
                {
                    continue;
                }

                for (int k = i - 1; k >= 0; k--)
                {
                    //Всё ещё пропускаем пустые значения
                    if (!tiles[j, k].IsBusy && k > 0)
                    {
                        continue;
                    }
                    //Если достигнута первая ячейка
                    if (!tiles[j, k].IsBusy && k == 0)
                    {
                        tiles[j, k].ReplaceTiles(tiles[j, i]);
                        break;
                    }
                    //Если встречена заполненная ячейка
                    if (tiles[j, k].IsBusy)
                    {
                        //Если ячейки совпадают
                        if (tiles[j, k].TileScore == tiles[j, i].TileScore)
                        {
                            tiles[j, k].MergeTiles(tiles[j, i]);
                        }
                        else
                        {
                            tiles[j, k + 1].ReplaceTiles(tiles[j, i]);
                        }
                        break;
                    }
                }

            }
        }
    }

    public void DoDown()
    {
        for (int i = size - 1; i >= 0; i--)
        {
            for (int j = 0; j < size; j++)
            {
                //Все первые значения пропускаются
                if (i == size - 1)
                {
                    continue;
                }
                //Пустые значения пропускаются
                if (!tiles[j, i].IsBusy)
                {
                    continue;
                }

                for (int k = i + 1; k < size; k++)
                {
                    //Всё ещё пропускаем пустые значения
                    if (!tiles[j, k].IsBusy && k < size - 1)
                    {
                        continue;
                    }
                    //Если достигнута первая ячейка
                    if (!tiles[j, k].IsBusy && k == size - 1)
                    {
                        tiles[j, k].ReplaceTiles(tiles[j, i]);
                        break;
                    }
                    //Если встречена заполненная ячейка
                    if (tiles[j, k].IsBusy)
                    {
                        //Если ячейки совпадают
                        if (tiles[j, k].TileScore == tiles[j, i].TileScore)
                        {
                            tiles[j, k].MergeTiles(tiles[j, i]);
                        }
                        else
                        {
                            tiles[j, k - 1].ReplaceTiles(tiles[j, i]);
                        }
                        break;
                    }
                }

            }
        }
    }

    public void GenerateRandomTilesForNextStep()
    {
        int countOfFreeTiles = GetCountOfFreeTiles();
        if (countOfFreeTiles == 0) return;

        int countOfTilesToCreate = Mathf.Clamp(countOfFreeTiles, 1, 2);
        //TODO: Добавить тайлов из списка пустых
        int countOfGeneratedTiles = 0;
        while(countOfGeneratedTiles < countOfTilesToCreate)
        {
            var rndTile = tiles[Random.Range(0, size), Random.Range(0, size)];
            if (rndTile.IsBusy) continue;

            if (Random.Range(0, 11) < 2)
            {
                rndTile.TileScore = 4;
            }
            else
            {
                rndTile.TileScore = 2;
            }
            countOfGeneratedTiles++;
        }
    }

    public int GetCountOfFreeTiles()
    {
        int countOfFreeTiles = GetCountOfMaxTiles();
        foreach(var tile in tiles)
        {
            if(tile.IsBusy)
            {
                countOfFreeTiles--;
            }
        }

        return countOfFreeTiles;
    }

    private int GetCountOfMaxTiles()
    {
        return size * size;
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
