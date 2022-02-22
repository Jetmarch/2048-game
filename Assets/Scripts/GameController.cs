using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static int Points { get; private set; }
    public static bool IsGameStarted { get; private set; }

    public static int PreviousPoints { get; private set; }

    [SerializeField]
    private TextMeshProUGUI gameResult;
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI highscoreText;

    [SerializeField]
    private GameObject gameField;
    [SerializeField]
    private GameObject menu;

    private bool isAlreadyWin;

    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //StartGame();
        SwipeDetection.SwipeEvent += OnInput;
    }

    private void OnInput(Vector2 direction)
    {
        gameResult.text = "";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
            gameField.SetActive(!gameField.activeSelf);
        }
    }

    public void StartGame()
    {
        menu.SetActive(false);
        gameField.SetActive(true);

        gameResult.text = string.Empty;

        SetPoints(0);
        SetHighScore();
        IsGameStarted = true;
        isAlreadyWin = false;
        Field.instance.GenerateField();
    }

    public void AddPoints(int points)
    {
        SetPoints(Points + points);

        PointsSaver.instance.CheckHighScoreAndSave(Points);
        SetHighScore();
    }

    public void SetPoints(int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }

    public void SetPreviousPoints(int points)
    {
        PreviousPoints = points;
    }

    private void SetHighScore()
    {
        highscoreText.text = PointsSaver.instance.GetHighScore().ToString();
    }

    public void Undo()
    {
        Field.instance.UndoMove();
    }

    public void Win()
    {
        if (isAlreadyWin) return;

        //IsGameStarted = false;
        gameResult.text = "You win!";
        isAlreadyWin = true;
    }

    public void Lose()
    {
        IsGameStarted = false;
        gameResult.text = "You lose!";
    }
}
