using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BestScoreHandler : MonoBehaviour
{
    private Text[] scoreTab = new Text[10];
    public Text textPrefab;
    public Canvas renderCanvas;
    
    public void AddNewScore(int score)
    {
        for (int i = 0; i < 10; i++)
        {
            int savedScore = PlayerPrefs.GetInt("score" + i.ToString(), 0);
            if (score > savedScore)
            {
                PlayerPrefs.SetInt("score"+i.ToString(), score);
                for (int j = i+1; j < 10; j++)
                {
                    int previousScore = PlayerPrefs.GetInt("score" + j.ToString(), 0);
                    PlayerPrefs.SetInt("score"+j.ToString(), savedScore);
                    savedScore = previousScore;
                }
                return;
            }
        }
    }

    public void UpdateScore()
    {
        for (int i = 0; i < 10; i++)
        {
            int score = PlayerPrefs.GetInt("score" + i.ToString(), 0);
            if (score != 0)
            {
                scoreTab[i].text = score.ToString();
            }
        }
    }

    public void Awake()
    {
        InitScoreText();
        UpdateScore();
    }
    
    private void InitScoreText()
    {
        Text title = Instantiate(textPrefab, new Vector3(10,-40,0), quaternion.identity);
        title.text = "High Score:";
        title.transform.SetParent(renderCanvas.transform, false);
        for (int i = 0; i < 10; i++)
        {
            scoreTab[i] = Instantiate(textPrefab , new Vector3(10,-(i+3)*20,0), quaternion.identity);
            scoreTab[i].transform.SetParent(renderCanvas.transform, false);
        }
    }
}