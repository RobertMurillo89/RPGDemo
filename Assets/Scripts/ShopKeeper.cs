using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{

    bool canOpen;

    public string[] itemsForSale = new string[40];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpen && Input.GetKeyDown(KeyCode.E) && Player.instance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {

            Shop.instance.itemsForSale = itemsForSale;

            Shop.instance.OpenShop();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            DialogueManager.instance.ActivateUIInstruction();
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            canOpen = false;
    }
}
