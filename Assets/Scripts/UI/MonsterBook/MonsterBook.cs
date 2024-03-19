using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBook : MonoBehaviour
{   
    [SerializeField] private Transform monsterList;
    private void Start(){
        MonsterBookItem item = Resources.Load<MonsterBookItem>("Prefab/UI/MonsterBook/MonsterBookItem");
        foreach (Monster monster in MonsterData.allMonsters)
        {
            MonsterBookItem tempItem = Instantiate(item,monsterList);
            Debug.Log(monster.GetMonsterSO().monsterName);
            tempItem.LoadSprite(monster.gameObject);
        }
    }
}
