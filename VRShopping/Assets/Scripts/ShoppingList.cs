using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public GameObject[] listItems;

    private String[][] itemHints;

    public void hint()
    {
        Debug.Log("Give item hint");
    }
}
