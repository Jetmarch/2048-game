using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static int Points { get; private set; }
    public static bool IsGameStarted { get; private set; }

    [SerializeField]
    private TextMeshProUGUI gameResult;
    [SerializeField]
    private TextMeshProUGUI pointsText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gameResult.text = string.Empty;

        SetPoints(0);
        IsGameStarted = true;

        Field.instance.GenerateField();
    }

    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    private void SetPoints(int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }

    public void Win()
    {
        IsGameStarted = false;
        gameResult.text = "You win!";
    }

    public void Lose()
    {
        IsGameStarted = false;
        gameResult.text = "You lose!";
    }
}
