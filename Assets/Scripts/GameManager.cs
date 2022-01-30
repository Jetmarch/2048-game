using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Tiles spawn")]
    [SerializeField] private float stepSize;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject tilesParent;
    [SerializeField] private Vector3 firstTilePos;

    [SerializeField] private int countOfTiles;

    private GameObject[,] tilesUI;


    private Grid grid;

    private void Start()
    {
        tilesUI = new GameObject[countOfTiles, countOfTiles];

        grid = new Grid(countOfTiles);
        grid.GenerateRandomTilesForNextStep();
        SpawnTiles();
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
            grid.GenerateRandomTilesForNextStep();
            UpdateTiles();
            Debug.Log($"Free tiles: {grid.GetCountOfFreeTiles()}");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            grid.DoDown();
            grid.GenerateRandomTilesForNextStep();
            UpdateTiles();
            Debug.Log($"Free tiles: {grid.GetCountOfFreeTiles()}");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            grid.DoRight();
            grid.GenerateRandomTilesForNextStep();
            UpdateTiles();
            Debug.Log($"Free tiles: {grid.GetCountOfFreeTiles()}");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            grid.DoLeft();
            grid.GenerateRandomTilesForNextStep();
            UpdateTiles();
            Debug.Log($"Free tiles: {grid.GetCountOfFreeTiles()}");
        }
    }

    public void StartGame()
    {

    }

    public void NextStep()
    {
        grid.GenerateRandomTilesForNextStep();
        UpdateTiles();
    }

    public void RestartGame()
    {
        grid = new Grid(countOfTiles);
        grid.GenerateRandomTilesForNextStep();
        UpdateTiles();
        grid.DebugGridView();
    }

    public void SpawnTiles()
    {
        var tiles = grid.GetTiles();

        for(int i = 0; i < countOfTiles; i++)
        {
            for(int j = 0; j < countOfTiles; j++)
            {
                var tile = Instantiate(tilePrefab, new Vector2(firstTilePos.x + (stepSize * j), firstTilePos.y + (stepSize * -i)), tilePrefab.transform.rotation);
                tile.transform.SetParent(tilesParent.transform, false);
                tile.GetComponentInChildren<TextMeshProUGUI>().text = tiles[j, i].TileScore > 0 ? tiles[j, i].TileScore.ToString() : "";
                tilesUI[j, i] = tile;
                tilesUI[j, i].GetComponent<GridTileUI>().x = j;
                tilesUI[j, i].GetComponent<GridTileUI>().y = i;
            }
        }
    }

    public void UpdateTiles()
    {
        var tiles = grid.GetTiles();

        for (int i = 0; i < countOfTiles; i++)
        {
            for (int j = 0; j < countOfTiles; j++) 
            {
                tilesUI[j, i].GetComponentInChildren<TextMeshProUGUI>().text = tiles[j, i].TileScore == 0 ? "" : tiles[j, i].TileScore.ToString();
                tilesUI[j, i].GetComponent<GridTileUI>().tileScore = tiles[j, i].TileScore;
            }
        }
    }
}
