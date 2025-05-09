using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public TextMeshProUGUI listDisplay;
    public GameObject[] taggedItems;
    private bool hintsShown = false;
    private int currentItemIndex = 0;
    private int currentHintIndex = 0;

    void Start()
    {
        ShowCurrentItem();
    }

    private void ShowCurrentItem()
    {
        listDisplay.text = "";

        if (currentItemIndex >= taggedItems.Length)
        {
            listDisplay.text = "All items are in the basket!";
            return;
        }

        GameObject currentItem = taggedItems[currentItemIndex];
        ItemData data = currentItem.GetComponent<ItemData>();

        if (data == null) return;

        listDisplay.text += $"<b>{data.itemName}</b>\n";

        if (!data.isInBasket)
        {
            listDisplay.text += $"{data.description}\n";

            for (int i = 0; i <= currentHintIndex && i < data.hints.Length; i++)
            {
                listDisplay.text += $"- {data.hints[i]}\n";
            }
        }
        else
        {
            MoveToNextItem();
        }
    }

    public void ShowNextHint()
    {
        GameObject currentItem = taggedItems[currentItemIndex];
        ItemData data = currentItem.GetComponent<ItemData>();

        if (data != null && currentHintIndex < data.hints.Length - 1)
        {
            currentHintIndex++;
            ShowCurrentItem();
        }
    }

    public void MoveToNextItem()
    {
        currentItemIndex++;
        currentHintIndex = 0;
        ShowCurrentItem();
    }

    public void RefreshList()
    {
        ShowCurrentItem();
    }

    public bool IsCurrentItem(GameObject obj)
    {
        if (currentItemIndex >= taggedItems.Length) return false;

        return taggedItems[currentItemIndex] == obj;
    }
}