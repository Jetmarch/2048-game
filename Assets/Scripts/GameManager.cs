using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Tiles spawn")]
    [SerializeField] private float stepSize;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject backgroundTilePrefab;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject tilesParent;
    [SerializeField] private Vector3 firstTilePos;

    [SerializeField] private int countOfTiles;

    private GameObject[,] tilesUI;
    private GameObject[,] backgroundTilesUI;

    private Grid grid;

    private void Start()
    {
        tilesUI = new GameObject[countOfTiles, countOfTiles];
        backgroundTilesUI = new GameObject[countOfTiles, countOfTiles];

        grid = new Grid(countOfTiles);
        SpawnBackgroundTiles();
        SpawnTiles();
        grid.SetTilesUI(tilesUI);

        grid.OnWin += Grid_OnWin;
        grid.OnLose += Grid_OnLose;
    }

    private void Grid_OnLose()
    {
        Debug.Log("LOSE!");
    }

    private void Grid_OnWin()
    {
        Debug.Log("WIN!");
    }

    private void Update()
    {
        /*if(Input.GetMouseButtonDown(1))
        {
            grid.DoDown();
            *//*grid.DebugGridView();*/
            /*grid.GenerateRandomTilesForNextStep();*//*
            UpdateTiles();
        }*/
        if(Input.GetKeyDown(KeyCode.W))
        {
            grid.DoUp();
            UpdateTiles();
            SpawnTiles();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            grid.DoDown();
            UpdateTiles();
            SpawnTiles();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            grid.DoRight();
            UpdateTiles();
            SpawnTiles();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            grid.DoLeft();
            UpdateTiles();
            SpawnTiles();
        }
    }

    public void StartGame()
    {

    }

    public void Undo()
    {
        grid.ReturnPreviousState();
        UpdateTiles();
    }

    public void RestartGame()
    {
        grid = new Grid(countOfTiles);
        grid.SetTilesUI(tilesUI);
        DeleteAllTiles();
        grid.OnWin += Grid_OnWin;
        grid.OnLose += Grid_OnLose;
        UpdateTiles();
        SpawnTiles();
        grid.DebugGridView();
    }

    public void SpawnBackgroundTiles()
    {
        for(int i = 0; i < countOfTiles; i++)
        {
            for(int j = 0; j < countOfTiles; j++)
            {
                var tile = Instantiate(backgroundTilePrefab, new Vector2(firstTilePos.x + (stepSize * j), firstTilePos.y + (stepSize * -i)), backgroundTilePrefab.transform.rotation);
                tile.transform.SetParent(tilesParent.transform, false);
                //  tile.GetComponentInChildren<TextMeshProUGUI>().text = tiles[j, i].TileScore > 0 ? tiles[j, i].TileScore.ToString() : "";
                backgroundTilesUI[j, i] = tile;
                backgroundTilesUI[j, i].GetComponent<GridTileUI>().x = j;
                backgroundTilesUI[j, i].GetComponent<GridTileUI>().y = i;
            }
        }
    }

    public void SpawnTiles()
    {
        var tiles = grid.GetTiles();

        for (int i = 0; i < countOfTiles; i++)
        {
            for (int j = 0; j < countOfTiles; j++)
            {
                if (!tiles[j, i].IsBusy) continue;
                if (tilesUI[j, i] != null)
                {
                    tilesUI[j, i].GetComponentInChildren<TextMeshProUGUI>().text = tiles[j, i].TileScore.ToString();
                    tilesUI[j, i].GetComponent<GridTileUI>().x = j;
                    tilesUI[j, i].GetComponent<GridTileUI>().y = i;
                }
                else
                {
                    var tile = Instantiate(tilePrefab, new Vector2(firstTilePos.x + (stepSize * j), firstTilePos.y + (stepSize * -i)), tilePrefab.transform.rotation);
                    tile.transform.SetParent(tilesParent.transform, false);
                    tile.GetComponentInChildren<TextMeshProUGUI>().text = tiles[j, i].TileScore.ToString();
                    tilesUI[j, i] = tile;
                    tilesUI[j, i].GetComponent<GridTileUI>().x = j;
                    tilesUI[j, i].GetComponent<GridTileUI>().y = i;
                }
            }
        }
    }

    public void DeleteAllTiles()
    {
        foreach(var tile in tilesUI)
        {
            Destroy(tile);
        }
    }

    public void UpdateTiles()
    {
       /* var tiles = grid.GetTiles();

        for (int i = 0; i < countOfTiles; i++)
        {
            for (int j = 0; j < countOfTiles; j++) 
            {
                tilesUI[j, i].GetComponentInChildren<TextMeshProUGUI>().text = tiles[j, i].TileScore == 0 ? "" : tiles[j, i].TileScore.ToString();
                tilesUI[j, i].GetComponent<GridTileUI>().tileScore = tiles[j, i].TileScore;
            }
        }*/
    }
}
