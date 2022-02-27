using System;
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
            PlayerPrefs.SetString(highscoreKey, "0");
        }
    }

    public void CheckHighScoreAndSave(ulong currentPoints)
    {
        ulong currentHighscore; 
        bool success = ulong.TryParse((PlayerPrefs.GetString(highscoreKey)), out currentHighscore);
        if(!success)
        {
            currentHighscore = 0;
        }

        if(currentPoints > currentHighscore)
        {
            PlayerPrefs.SetString(highscoreKey, currentPoints.ToString());
        }
    }

    public Points GetHighScore()
    {
        return new Points((ulong)Convert.ToDouble(PlayerPrefs.GetString(highscoreKey)));
    }
}
