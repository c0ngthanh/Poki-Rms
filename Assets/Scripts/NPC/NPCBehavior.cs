using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum NPCType{
    Shopkeeper,
    Quest,
    Duelist,
    Normal
}
public enum Item{
    Ticket
}
public class NPCBehavior : MonoBehaviour
{
    public NPCType npcType;
    public Item npcItem;
    public int price;
    public Monster monster;
    public int level;
    // Start is called before the first frame update
    public void Action(){
        if(npcType == NPCType.Shopkeeper){
            Buy();
        }
        if(npcType == NPCType.Duelist){
            Duel();
        }
    }

    private void Duel()
    {
        if(PlayerController.instance.activeMonster == null){
            NotificationText.ShowNotification("You dont have activeMonster");
            return;
        }
        Monster temp = monster;
        temp.SetMonsterStatWithLevel(level);
        GameManager.Instance.SetUpBattle(PlayerController.instance.activeMonster,temp);
    }

    private void Buy(){
        if(npcItem == Item.Ticket){
            if(PlayerController.instance.coin >= price){
                PlayerController.instance.SetCoin(PlayerController.instance.coin-price);
                PlayerController.instance.ticket += 1;
                NotificationText.ShowNotification("Buy 1 ticket successfully with 100 coins");
            }else{
                NotificationText.ShowNotification("You don't have enough coin to buy");
            }
        }
    }
}
