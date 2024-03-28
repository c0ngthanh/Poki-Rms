using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBook : MonoBehaviour
{   
    [SerializeField] private Transform monsterList;
    [SerializeField] private Inventory monsterInventory;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button previousPageButton;
    private void Start(){
        nextPageButton.onClick.AddListener(NextPage);
        previousPageButton.onClick.AddListener(PreviousPage);
        MonsterBookItem item = Resources.Load<MonsterBookItem>("Prefab/UI/MonsterBook/MonsterBookItem");
        monsterInventory = new Inventory(8,item.gameObject);
        foreach (Monster monster in MonsterData.allMonsters)
        {
            MonsterBookItem tempItem = Instantiate(item,monsterList);
            tempItem.LoadSprite(monster.gameObject);
            monsterInventory.Add(tempItem);
        }
        monsterInventory.ShowBookFirstTime();
    }
    public void NextPage(){
        monsterInventory.NextPage();
    }
    public void PreviousPage(){
        monsterInventory.PreviousPage();
    }
}
