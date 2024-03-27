using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBook : MonoBehaviour
{   
    [SerializeField] private Transform monsterList;
    [SerializeField] private Inventory monsterBook;
    private void Start(){
        MonsterBookItem item = Resources.Load<MonsterBookItem>("Prefab/UI/MonsterBook/MonsterBookItem");
        monsterBook = new Inventory(8,item.gameObject);
        foreach (Monster monster in MonsterData.allMonsters)
        {
            MonsterBookItem tempItem = Instantiate(item,monsterList);
            tempItem.LoadSprite(monster.gameObject);
            monsterBook.itemObjectList.Add(tempItem.gameObject);
        }
        monsterBook.ShowBookFirstTime();
    }
}
