using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public static int correctItemsCollected;
    public static int wrongItemsCollected;
    public static int totalItems;
    public static int score = 0;


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
}
