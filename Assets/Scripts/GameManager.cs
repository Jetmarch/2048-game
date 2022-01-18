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
        if(Input.GetMouseButtonDown(0))
        {
            grid = new Grid(countOfTiles);
            grid.GenerateRandomTilesForNextStep();
            UpdateTiles();
        }
    }

    public void StartGame()
    {

    }

    public void RestartGame()
    {
        grid = new Grid(countOfTiles);
        grid.GenerateRandomTilesForNextStep();
        UpdateTiles();
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
                tile.GetComponentInChildren<TextMeshProUGUI>().text = tiles[i, j].TileScore > 0 ? tiles[i, j].TileScore.ToString() : "";
                tilesUI[i, j] = tile;
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
                tilesUI[i, j].GetComponentInChildren<TextMeshProUGUI>().text = tiles[i, j].TileScore > 0 ? tiles[i, j].TileScore.ToString() : "";
            }
        }
    }
}
