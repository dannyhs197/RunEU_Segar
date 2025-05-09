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
            ItemData data = other.GetComponent<ItemData>();

            if (data != null && !data.isInBasket)
            {
                data.isInBasket = true;
                other.gameObject.SetActive(false);
                GameController.score += pointsPerItem;

                if (shoppingList.IsCurrentItem(other.gameObject))
                {
                    shoppingList.MoveToNextItem();
                }
                else
                {
                    shoppingList.RefreshList();
                }
            }
        }
    }
}
