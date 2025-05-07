using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject UIScore;
    public GameObject UINoScore;

    void Start()
    {
        if (GameController.trackScore.Equals(true))
        {
            UINoScore.SetActive(false);
        }

        if (GameController.trackScore.Equals(false))
        {
            UIScore.SetActive(false);
        }
    } 
}
