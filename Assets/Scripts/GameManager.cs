using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Tiles spawn")]
    [SerializeField] private float stepSize;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject tilesParent;
    [SerializeField] private Vector3 firstTilePos;


    private Grid grid;

    private void Start()
    {
        SpawnTiles();


        grid = new Grid(4);
        grid.GenerateRandomTilesForNextStep();
        grid.DebugGridView();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            grid.GenerateRandomTilesForNextStep();
            grid.DebugGridView();
        }
    }

    public void StartGame()
    {

    }

    public void SpawnTiles()
    {

        for (int i = 0; i < 16; i++)
        {
            var tile = Instantiate(tilePrefab);
            tile.transform.SetParent(tilesParent.transform, false);
        }
    }
}
