using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid
{
    private GridTile[,] tiles;
    private int size;
    private Stack<GridTile[,]> previousTilesStates;
    private int winScore = 2048;

    private GameObject[,] tilesUI;


    public delegate void GameOverEvent();
    public event GameOverEvent OnLose;
    public event GameOverEvent OnWin;

    /*
        Реализация пошаговой смены

    
     */

    public Grid(int size)
    {
        this.size = size;
        tiles = new GridTile[size, size];
        previousTilesStates = new Stack<GridTile[,]>();

        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                tiles[i, j] = new GridTile(j, i);
            }
        }
        GenerateRandomTilesForNextStep();

        SaveCurrentTilesStateToStack();
    }

    public GridTile[,] GetTiles()
    {
        return tiles;
    }

    public void SetTilesUI(GameObject[,] mono)
    {
        this.tilesUI = mono;
    }

    private bool LeftAction(bool isActionNeedAffectOnGrid)
    {
        bool isActionTaken = false;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
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

                for (int k = j - 1; k >= 0; k--)
                {
                    //Всё ещё пропускаем пустые значения
                    if (!tiles[k, i].IsBusy && k > 0)
                    {
                        continue;
                    }
                    //Если достигнута первая ячейка
                    if (!tiles[k, i].IsBusy && k == 0)
                    {
                        if (isActionNeedAffectOnGrid)
                        {
                            tiles[k, i].ReplaceTiles(tiles[j, i]);
                            tilesUI[j, i].GetComponent<GridTileUI>().MoveToX(k);
                            tilesUI[k, i] = tilesUI[j, i];
                            tilesUI[j, i] = null;
                        }
                        isActionTaken = true;
                        break;
                    }
                    //Если встречена заполненная ячейка
                    if (tiles[k, i].IsBusy)
                    {
                        //Если ячейки совпадают
                        if (tiles[k, i].TileScore == tiles[j, i].TileScore)
                        {
                            if (isActionNeedAffectOnGrid)
                            {
                                tiles[k, i].MergeTiles(tiles[j, i]);
                                tilesUI[j, i].GetComponent<GridTileUI>().MoveToX(k, true);
                                tilesUI[k, i] = tilesUI[j, i];
                                tilesUI[j, i] = null;
                                if (tiles[k, i].TileScore == winScore)
                                {
                                    OnWin();
                                }
                            }
                            isActionTaken = true;
                        }
                        else
                        {
                            if (isActionNeedAffectOnGrid)
                            {
                                tiles[k + 1, i].ReplaceTiles(tiles[j, i]);
                                tilesUI[j, i].GetComponent<GridTileUI>().MoveToX(k + 1);
                                tilesUI[k + 1, i] = tilesUI[j, i];
                                tilesUI[j, i] = null;
                            }
                        }
                        break;
                    }

                }

            }
        }

        return isActionTaken;

    }

    public void DoLeft()
    {


        bool isActionTaken = LeftAction(true);

        if (isActionTaken)
        {
            GenerateRandomTilesForNextStep();
            SaveCurrentTilesStateToStack();
        }
        else
        {
            if (UpAction(false) || DownAction(false) || RightAction(false) || LeftAction(false))
            {

            }
            else
            {
                OnLose();
            }
        }
    }

    private bool RightAction(bool isActionNeedAffectOnGrid)
    {
        bool isActionTaken = false;
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
                        if (isActionNeedAffectOnGrid)
                        {
                            tiles[k, i].ReplaceTiles(tiles[j, i]);
                            tilesUI[j, i].GetComponent<GridTileUI>().MoveToX(k);
                            tilesUI[k, i] = tilesUI[j, i];
                            tilesUI[j, i] = null;
                        }
                        isActionTaken = true;
                        break;
                    }
                    //Если встречена заполненная ячейка
                    if (tiles[k, i].IsBusy)
                    {
                        //Если ячейки совпадают
                        if (tiles[k, i].TileScore == tiles[j, i].TileScore)
                        {
                            if (isActionNeedAffectOnGrid)
                            {
                                tiles[k, i].MergeTiles(tiles[j, i]);
                                tilesUI[j, i].GetComponent<GridTileUI>().MoveToX(k, true);
                                tilesUI[k, i] = tilesUI[j, i];
                                tilesUI[j, i] = null;
                                if (tiles[k, i].TileScore == winScore)
                                {
                                    OnWin();
                                }
                            }
                            isActionTaken = true;
                        }
                        else
                        {
                            if (isActionNeedAffectOnGrid)
                            {
                                tiles[k - 1, i].ReplaceTiles(tiles[j, i]);
                                tilesUI[j, i].GetComponent<GridTileUI>().MoveToX(k - 1);
                                tilesUI[k - 1, i] = tilesUI[j, i];
                                tilesUI[j, i] = null;
                            }
                        }
                        break;
                    }
                }

            }
        }

        return isActionTaken;
    }

    public void DoRight()
    {
        bool isActionTaken = RightAction(true);

        if (isActionTaken)
        {
            GenerateRandomTilesForNextStep();
            SaveCurrentTilesStateToStack();
        }
        else
        {
            if (UpAction(false) || DownAction(false) || RightAction(false) || LeftAction(false))
            {

            }
            else
            {
                OnLose();
            }
        }
    }

    private bool UpAction(bool isActionNeedAffectOnGrid)
    {
        bool isActionTaken = false;
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
                        if (isActionNeedAffectOnGrid)
                        {
                            tiles[j, k].ReplaceTiles(tiles[j, i]);
                            tilesUI[j, i].GetComponent<GridTileUI>().MoveToY(k);
                            tilesUI[j, k] = tilesUI[j, i];
                            tilesUI[j, i] = null;
                        }
                        isActionTaken = true;
                        break;
                    }
                    //Если встречена заполненная ячейка
                    if (tiles[j, k].IsBusy)
                    {
                        //Если ячейки совпадают
                        if (tiles[j, k].TileScore == tiles[j, i].TileScore)
                        {
                            if (isActionNeedAffectOnGrid)
                            {
                                tiles[j, k].MergeTiles(tiles[j, i]);
                                tilesUI[j, i].GetComponent<GridTileUI>().MoveToY(k, true);
                                tilesUI[j, k] = tilesUI[j, i];
                                tilesUI[j, i] = null;
                                if (tiles[k, i].TileScore == winScore)
                                {
                                    OnWin();
                                }
                            }
                            isActionTaken = true;
                        }
                        else
                        {
                            if (isActionNeedAffectOnGrid)
                            {
                                tiles[j, k + 1].ReplaceTiles(tiles[j, i]);
                                tilesUI[j, i].GetComponent<GridTileUI>().MoveToY(k + 1);
                                tilesUI[j, k + 1] = tilesUI[j, i];
                                tilesUI[j, i] = null;
                            }
                        }
                        break;
                    }
                }

            }
        }

        return isActionTaken;
    }

    public void DoUp()
    {
        bool isActionTaken  = UpAction(true);
        if (isActionTaken)
        {
            GenerateRandomTilesForNextStep();
            SaveCurrentTilesStateToStack();
        }
        else
        {
            if (UpAction(false) || DownAction(false) || RightAction(false) || LeftAction(false))
            {

            }
            else
            {
                OnLose();
            }
        }
    }

    private bool DownAction(bool isActionNeedAffectOnGrid)
    {
        bool isActionTaken = false;
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
                        if (isActionNeedAffectOnGrid)
                        {
                            tiles[j, k].ReplaceTiles(tiles[j, i]);
                            tilesUI[j, i].GetComponent<GridTileUI>().MoveToY(k);
                            tilesUI[j, k] = tilesUI[j, i];
                            tilesUI[j, i] = null;
                        }
                        isActionTaken = true;
                        break;
                    }
                    //Если встречена заполненная ячейка
                    if (tiles[j, k].IsBusy)
                    {
                        //Если ячейки совпадают
                        if (tiles[j, k].TileScore == tiles[j, i].TileScore)
                        {
                            if (isActionNeedAffectOnGrid)
                            {
                                tiles[j, k].MergeTiles(tiles[j, i]);
                                tilesUI[j, i].GetComponent<GridTileUI>().MoveToY(k, true);
                                tilesUI[j, k] = tilesUI[j, i];
                                tilesUI[j, i] = null;
                                if (tiles[k, i].TileScore == winScore)
                                {
                                    OnWin();
                                }
                            }
                            isActionTaken = true;
                        }
                        else
                        {
                            if (isActionNeedAffectOnGrid)
                            {
                                tiles[j, k - 1].ReplaceTiles(tiles[j, i]);
                                tilesUI[j, i].GetComponent<GridTileUI>().MoveToY(k - 1);
                                tilesUI[j, k - 1] = tilesUI[j, i];
                                tilesUI[j, i] = null;
                            }
                        }
                        break;
                    }
                }

            }
        }

        return isActionTaken;
    }

    public void DoDown()
    {
        bool isActionTaken  = DownAction(true);

        if (isActionTaken)
        {
            GenerateRandomTilesForNextStep();
            SaveCurrentTilesStateToStack();
        }
        else
        {
            if (UpAction(false) || DownAction(false) || RightAction(false) || LeftAction(false))
            {

            }
            else
            {
                OnLose();
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

    private void SaveCurrentTilesStateToStack()
    {
        var backup = new GridTile[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                backup[i, j] = new GridTile(i, j, tiles[i, j].TileScore);
            }
        }

        previousTilesStates.Push(backup);
    }

    public void ReturnPreviousState()
    {

        if (previousTilesStates.Count == 1) return;

        previousTilesStates.Pop();

        var newCurrentState = previousTilesStates.Peek();

        var stateCopy = new GridTile[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                stateCopy[i, j] = new GridTile(i, j, newCurrentState[i, j].TileScore);
            }
        }

        tiles = stateCopy;
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

       /* for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Debug.Log($"Y: {i} X: {j} Score {tiles[j, i].TileScore}");
            }
        }*/
        
    }
}
