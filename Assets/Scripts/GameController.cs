using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static Points Points { get; private set; }
    public static bool IsGameStarted { get; private set; }

    public static ulong PreviousPoints { get; private set; }

    [Header("Games results")]
    [SerializeField]
    private CanvasGroup resultsScreen;
    [SerializeField]
    private GameObject resultsImage;
    [SerializeField]
    private TextMeshProUGUI loseText;
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private TextMeshProUGUI highscoreText;
    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private GameObject gameField;
    [SerializeField]
    private GameObject menu;

    [Space(3)]
    [Header("Menu animations")]
    [SerializeField]
    private TextMeshProUGUI gameNameText;
    [SerializeField]
    private GameObject startGameButton;
    [SerializeField]
    private float animationTime = 0.3f;
    [SerializeField]
    private float fadeInOutAnimationTime = 0.3f;

    [Space(3)]
    [Header("Events")]
    [SerializeField]
    private SOEvent gameWin;
    [SerializeField]
    private SOEvent gameLose;


    private bool isAlreadyWin;

    private Sequence sequence;

    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

#if UNITY_ANDROID
        Screen.fullScreen = false;
#endif
    }

    private void Start()
    {
        DOTween.Init();
        //StartGame();
        SwipeDetection.SwipeEvent += OnInput;

        ToggleMenu(false);
    }

    private void OnInput(Vector2 direction)
    {
        if (!IsGameStarted) return;

        loseText.text = "";
        resultsScreen.alpha = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu(false);
        }
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.F))
        {
            Lose();
        }
#endif
    }

    /// <summary>
    /// Отображение меню или игрового поля
    /// True - игровое поле
    /// False - меню
    /// </summary>
    /// <param name="state"></param>
    private void ToggleMenu(bool state)
    {
        IsGameStarted = state;

        if (state)
         {
            if (sequence != null)
            {
                sequence.Complete();
            }
            sequence = DOTween.Sequence();
            gameField.GetComponent<CanvasGroup>().interactable = true;
            menu.GetComponent<CanvasGroup>().interactable = false;
            sequence.Append(menu.GetComponent<CanvasGroup>().DOFade(0f, fadeInOutAnimationTime));
            sequence.Join(gameField.GetComponent<CanvasGroup>().DOFade(1f, fadeInOutAnimationTime));
        }
         else
         {
            if(sequence != null)
            {
                sequence.Complete();
            }
            sequence = DOTween.Sequence();
            menu.GetComponent<CanvasGroup>().interactable = true;
            gameField.GetComponent<CanvasGroup>().interactable = false;
            sequence.Append(gameField.GetComponent<CanvasGroup>().DOFade(0f, fadeInOutAnimationTime));
            sequence.Join(menu.GetComponent<CanvasGroup>().DOFade(1f, fadeInOutAnimationTime));

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
        resultsScreen.alpha = 0f;
        Points = new Points();

        loseText.text = string.Empty;
        winText.SetActive(false);

        SetPoints(0);
        SetHighScore();
        IsGameStarted = true;
        isAlreadyWin = false;
        Field.instance.GenerateField();
        ToggleMenu(true);
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
        /*losePanel.text = "You win!";*/

        winText.SetActive(true);

        isAlreadyWin = true;

        gameWin.Raise();
    }

    public void Lose()
    {
        IsGameStarted = false;
        float fieldWidth = Field.instance.fieldSize * (Field.instance.cellSize + Field.instance.spacing) + Field.instance.spacing;
        resultsImage.GetComponent<RectTransform>().sizeDelta = new Vector2(fieldWidth, fieldWidth);

        resultsScreen.DOFade(1.0f, fadeInOutAnimationTime);

        if (isAlreadyWin)
        {
            loseText.text = "Game Over!";
        }
        else
        {
            loseText.text = "You lose!";
        }

        gameLose.Raise();
    }
}
