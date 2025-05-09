using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData : MonoBehaviour
{
    public string itemName;
    [TextArea]
    public string description;
    public string[] hints = new string[2];
    public bool isInBasket = false; 
}
