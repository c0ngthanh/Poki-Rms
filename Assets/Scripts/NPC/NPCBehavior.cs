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
    // Start is called before the first frame update
    public void Action(){
        if(npcType == NPCType.Shopkeeper){
            Buy();
        }
    }
    private void Buy(){
        if(npcItem == Item.Ticket){
            if(PlayerController.instance.coin >= price){
                PlayerController.instance.coin -= price;
                PlayerController.instance.ticket += 1;
            }
        }
    }
}
