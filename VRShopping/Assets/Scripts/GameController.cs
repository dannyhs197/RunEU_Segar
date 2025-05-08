using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System;
using UnityEditor;

public class GameController : MonoBehaviour
{
    public static int score = 0;

    public static bool trackScore = false;


    public TextMeshProUGUI scoreField;
    public GameObject[] collectableItems;

    void Start()
    {
        UpdateScore(score.ToString());
    }

    public void UpdateScore(String score)
    {
        scoreField.text = "You're Score : " + score;
    }

    public void toggleScoreTracking()
    {
        if (trackScore.Equals(false))
        {
            trackScore = true;
            Debug.Log("Score Tracking : " + trackScore);
        }
        else if (trackScore.Equals(true))
        {
            trackScore = false;
            Debug.Log("Score Tracking : " + trackScore);
        }else
        {
            Debug.LogError("Score Toggle not set");
        }
    }
}
