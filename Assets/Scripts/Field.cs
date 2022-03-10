using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field instance;


    [Header("Game field properties")]
    public float cellSize;
    public float spacing;
    public int fieldSize;
    public int initCellCount;

    [Space(10)]
    [SerializeField] private Cell cellPref;
    [SerializeField] private RectTransform rect;

    private Cell[,] field;

    private TempCell[,] previousMove;

    private bool isAnyCellMoved;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SwipeDetection.SwipeEvent += OnInput;
    }

    private void CreateField()
    {
        field = new Cell[fieldSize, fieldSize];
        previousMove = new TempCell[fieldSize, fieldSize];

        float fieldWidth = fieldSize * (cellSize + spacing) + spacing;
        rect.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(fieldWidth / 2) + (cellSize / 2) + spacing;
        float startY = (fieldWidth / 2) - (cellSize / 2) - spacing;

        for(int x = 0; x < fieldSize; x++)
        {
            for(int y = 0; y < fieldSize; y++)
            {
                var cell = Instantiate(cellPref, transform, false);
                var position = new Vector2(startX + (x * (cellSize + spacing)), startY - (y * (cellSize + spacing)));
                cell.transform.localPosition = position;

                field[x, y] = cell;

                cell.SetValue(x, y, 0);
            }
        }
    }

    public void GenerateField()
    {
        if(field == null)
        {
            CreateField();
        }

        for(int x = 0; x < fieldSize; x++)
        {
            for(int y = 0; y < fieldSize; y++)
            {
                field[x, y].SetValue(x, y, 0);
            }
        }

        for(int i = 0; i < initCellCount; i++)
        {
            GenerateRandomCell();
        }

    }

    private void GenerateRandomCell()
    {
        var emptyCells = new List<Cell>();

        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                if (field[x, y].IsEmpty)
                    emptyCells.Add(field[x, y]);
            }
        }

        if(emptyCells.Count == 0)
        {
            throw new System.Exception("There is no any empty cell");
        }

        int value = Random.Range(0, 10) == 0 ? 2 : 1;
        bool isBonusTile = Random.Range(0, 10) == 0 ? true : false;

        var cell = emptyCells[Random.Range(0, emptyCells.Count)];
        cell.SetValue(cell.X, cell.Y, value, false, isBonusTile);

        CellAnimationController.instance.SmoothAppear(cell);
    }

    private void OnInput(Vector2 direction)
    {
        if(!GameController.IsGameStarted)
        {
            return;
        }

        SaveField();

        isAnyCellMoved = false;
        ResetCellsFlags();

        Move(direction);

        if(isAnyCellMoved)
        {
            GenerateRandomCell();
            CheckGameResult();
        }
    }

    private void Move(Vector2 direction)
    {
        int startXY = direction.x > 0 || direction.y < 0 ? fieldSize - 1 : 0;
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y;

        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = startXY; j >= 0 && j < fieldSize; j -= dir)
            {
                var cell = direction.x != 0 ? field[j, i] : field[i, j];

                if(cell.IsEmpty)
                {
                    continue;
                }

                var cellToMerge = FindCellToMerge(cell, direction);
                if(cellToMerge != null)
                {
                    cell.MergeWithCell(cellToMerge);
                    isAnyCellMoved = true;
                    continue;
                }

                var emptyCell = FindEmptyCell(cell, direction);
                if(emptyCell != null)
                {
                    cell.MoveToCell(emptyCell);
                    isAnyCellMoved = true;
                    continue;
                }
            }
        }

    }

    private void SaveField()
    {
        GameController.instance.SetPreviousPoints(GameController.Points.Amount);
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                previousMove[x, y] = new TempCell(x, y, field[x, y].Value, field[x, y].IsBonusTile);
            }
        }
    }

    public void UndoMove()
    {
        GameController.instance.SetPoints(GameController.PreviousPoints);
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                field[x, y].SetValue(x, y, previousMove[x, y].Value, true, previousMove[x, y].IsBonusTile);
            }
        }
    }

    private Cell FindCellToMerge(Cell cell, Vector2 direction)
    {
        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for(int x = startX, y = startY;
            x >= 0 && x < fieldSize && y >= 0 && y < fieldSize;
            x += (int)direction.x, y -= (int)direction.y)
        {
            if(field[x, y].IsEmpty)
            {
                continue;
            }

            if(field[x, y].Value == cell.Value && !field[x, y].HasMerged)
            {
                return field[x, y];
            }

            break;
        }

        return null;
    }

    private Cell FindEmptyCell(Cell cell, Vector2 direction)
    {
        Cell emptyCell = null;
        int startX = cell.X + (int)direction.x;
        int startY = cell.Y - (int)direction.y;

        for (int x = startX, y = startY;
           x >= 0 && x < fieldSize && y >= 0 && y < fieldSize;
           x += (int)direction.x, y -= (int)direction.y)
        {
            if (field[x, y].IsEmpty)
            {
                emptyCell = field[x, y];
            }
            else
            {
                break;
            }    

        }

        return emptyCell;
    }

    private void CheckGameResult()
    {
        bool lose = true;

        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                if(field[x, y].Value == Cell.MaxValue)
                {
                    GameController.instance.Win();
                    return;
                }

                if(lose &&
                    field[x, y].IsEmpty ||
                    FindCellToMerge(field[x, y], Vector2.left) ||
                    FindCellToMerge(field[x, y], Vector2.right) ||
                    FindCellToMerge(field[x, y], Vector2.up) ||
                    FindCellToMerge(field[x, y], Vector2.down))
                {
                    lose = false;
                }
            }
        }

        if(lose)
        {
            GameController.instance.Lose();
        }
    }

    private void ResetCellsFlags()
    {
        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                field[x, y].ResetFlag();
            }
        }
    }


    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.A))
        {
            OnInput(Vector2.left);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            OnInput(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnInput(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnInput(Vector2.up);
        }
#endif
    }
}
