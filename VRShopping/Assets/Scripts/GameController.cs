using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    public static int correctItemsCollected;
    public static int wrongItemsCollected;
    public static int totalItems;
    public static int score;


    public GameObject[] collectableItems;
}
