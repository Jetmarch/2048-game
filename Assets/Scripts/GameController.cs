using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static Points Points { get; private set; }
    public static bool IsGameStarted { get; private set; }

    public static ulong PreviousPoints { get; private set; }

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

    [Space(3)]
    [Header("Menu animations")]
    [SerializeField]
    private GameObject gameNameText;
    [SerializeField]
    private GameObject startGameButton;
    [SerializeField]
    private float animationTime = 0.3f;

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
        DOTween.Init();
        //StartGame();
        SwipeDetection.SwipeEvent += OnInput;

        ToggleMenu();
    }

    private void OnInput(Vector2 direction)
    {
        gameResult.text = "";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.F))
        {
            AddPoints(1000);
        }
#endif
    }

    private void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
        IsGameStarted = !IsGameStarted;
        gameField.SetActive(!gameField.activeSelf);

        if(menu.activeSelf)
        {
            PlayMenuAnimation();
        }
    }

    private void PlayMenuAnimation()
    {
        startGameButton.transform.DOScale(1f, 0f);

        startGameButton.transform.DOScale(1.2f, animationTime).SetEase(Ease.OutCubic).SetLoops(-1, LoopType.Yoyo);
    }

    public void StartGame()
    {
        Points = new Points();
        menu.SetActive(false);
        gameField.SetActive(true);

        gameResult.text = string.Empty;

        SetPoints(0);
        SetHighScore();
        IsGameStarted = true;
        isAlreadyWin = false;
        Field.instance.GenerateField();
    }

    public void AddPoints(ulong points)
    {

        SetPoints(Points.Amount + points);

        PointsSaver.instance.CheckHighScoreAndSave(Points.Amount);
        SetHighScore();
    }

    public void SetPoints(ulong points)
    {
        Points.Amount = points;
        pointsText.text = Points.ToString();
    }

    public void SetPreviousPoints(ulong points)
    {
        PreviousPoints = points;
    }

    private void SetHighScore()
    {
        highscoreText.text = PointsSaver.instance.GetHighScore().ToString();
    }

    public void Undo()
    {
        if (!IsGameStarted) return;

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
