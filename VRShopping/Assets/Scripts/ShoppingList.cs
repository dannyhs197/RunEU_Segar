using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public TextMeshProUGUI listDisplay;

    private GameObject[] taggedItems;
    private bool hintsShown = false;

    void Start()
    {
        taggedItems = GameObject.FindGameObjectsWithTag("Item");
        DisplayItems(showHints: false);
    }

    public void ShowHints()
    {
        if (!hintsShown)
        {
            hintsShown = true;
            DisplayItems(showHints: true);
        }
    }

    private void DisplayItems(bool showHints)
    {
        listDisplay.text = "";

        foreach (GameObject obj in taggedItems)
        {
            ItemData data = obj.GetComponent<ItemData>();
            if (data != null)
            {
                listDisplay.text += $"<b>{data.itemName}</b>\n";

                if (!data.isInBasket)
                {
                    listDisplay.text += $"{data.description}\n";
                }

                if (showHints)
                {
                    foreach (string hint in data.hints)
                    {
                        listDisplay.text += $"- {hint}\n";
                    }
                }

                listDisplay.text += "\n";
            }
        }
    }

    public void RefreshList()
    {
        DisplayItems(showHints: hintsShown);
    }
}
