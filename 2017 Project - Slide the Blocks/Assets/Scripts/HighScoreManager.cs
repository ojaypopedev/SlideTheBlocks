using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{

    [SerializeField] private Text hiScoreText;

    private int Best;

    private readonly string highScore = "HIGH_SCORE";

    [ContextMenu("Reset High Score")] public void ResetHighScore()
    {
        Best = 0;
        PlayerPrefs.SetInt(highScore, 0);
    }
   

    private void Awake()
    {
        Best = PlayerPrefs.GetInt(highScore);
        

    }

    public void testScore(int score)
    {
        if(score > Best)
        {
            PlayerPrefs.SetInt(highScore, score);
            Best = PlayerPrefs.GetInt(highScore);

        }


        hiScoreText.text = Best.ToString();

    }

}
