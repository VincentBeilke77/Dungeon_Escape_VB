using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    protected GameObject shopPanel;
    protected int selectedItem, selectedCost, remainingDiamonds;
    protected Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            if (player != null)
            {
                UIManager.Instance.OpenShop(player.Diamonds);
                shopPanel.SetActive(true);
            } else
            {
                Debug.Log("Player is NULL.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        selectedItem = item;
        //0 - flame sword
        //1 - boots of flight
        //2 - key to castle
        Debug.Log($"SelectItem({item})");
        switch(item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(103);
                selectedCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(3);
                selectedCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-107);
                selectedCost = 100;
                break;
            default:
                break;
        }
    }

    public void BuyItem()
    {
        // check if player has enough gems
        if (player.Diamonds >= selectedCost)
        {
            if (selectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }

            Debug.Log($"Player Purchased {selectedItem}!");
            player.Diamonds = player.Diamonds - selectedCost;
            shopPanel.SetActive(false);
        } else
        {
            Debug.Log("You do not have enough gems. Closing Shop");
            shopPanel.SetActive(false);
        }
    }
}
