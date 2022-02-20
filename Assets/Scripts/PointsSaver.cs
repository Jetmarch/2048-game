using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSaver : MonoBehaviour
{
    private string highscoreKey = "Highscore";
    public static PointsSaver instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        if (!PlayerPrefs.HasKey(highscoreKey))
        {
            PlayerPrefs.SetInt(highscoreKey, 0);
        }
    }

    public void CheckHighScoreAndSave(int currentPoints)
    {
        int currentHighscore = PlayerPrefs.GetInt(highscoreKey);

        if(currentPoints > currentHighscore)
        {
            PlayerPrefs.SetInt(highscoreKey, currentPoints);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(highscoreKey);
    }
}
