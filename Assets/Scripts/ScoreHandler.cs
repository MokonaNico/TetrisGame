using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public Text text;

    public void UpdateScore(int score)
    {
        score = score * 1000;
        text.text = ""+score;
    }
}
