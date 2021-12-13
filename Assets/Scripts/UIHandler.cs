using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text text;
    public Text gameOver;

    public void UpdateScore(int score)
    {
        text.text = ""+score;
    }

    public void ShowGameOver(bool show)
    {
        gameOver.enabled = show;
    }
}
