using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public ShoppingList shoppingList;
    private string itemTag = "Item";
    private int pointsPerItem = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(itemTag))
        {
            other.gameObject.SetActive(false);
            GameController.score += pointsPerItem;

            other.GetComponent<ItemData>().isInBasket = true;
            shoppingList.RefreshList();
        }
    }
}
