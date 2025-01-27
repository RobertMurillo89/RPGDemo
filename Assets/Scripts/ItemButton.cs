using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public TMP_Text amountText;
    public int buttonValue;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if (GameMenu.instance.theMenu.activeInHierarchy)
        {
            if (GameManager.instance.itemsHeld[buttonValue] != "")
            {
                GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }
        else if(Shop.instance.shopMenu.activeInHierarchy)
        {
            if(Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.instance.itemsForSale[buttonValue]));
            }

            if(Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }
        else if (BattleManager.instance.battleScene.activeInHierarchy)
        {
            if (GameManager.instance.itemsHeld[buttonValue] != "")
            {
                BattleManager.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
            }
        }
    }
}
