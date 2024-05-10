using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    [SerializeField] private Dictionary<Transform, Monster.MonsterStats> statDictionary = new Dictionary<Transform, Monster.MonsterStats>();
    [SerializeField] private Button closeButton;
    private Monster monster;
    // Start is called before the first frame update
    private void Start()
    {
        closeButton.onClick.AddListener(()=>{
            Destroy(gameObject);
        });
    }
    public void SetMonster(Monster value){
        this.monster = value;
        SetReference();
        SetValue();
    }
    public static string fileName(){
        return "Prefab/UI/MonsterInfo";
    }

    private void SetReference()
    {
        Transform monsterStats = transform.Find("MonsterStats");
        statDictionary.Add(monsterStats.Find("HP"),Monster.MonsterStats.HP);
        statDictionary.Add(monsterStats.Find("ATK"),Monster.MonsterStats.ATK);
        statDictionary.Add(monsterStats.Find("DEF"),Monster.MonsterStats.DEF);
        statDictionary.Add(monsterStats.Find("ENERGY"),Monster.MonsterStats.ENERGY);
        statDictionary.Add(monsterStats.Find("SPEED"),Monster.MonsterStats.SPEED);
        statDictionary.Add(monsterStats.Find("ER"),Monster.MonsterStats.ER);
        statDictionary.Add(monsterStats.Find("HR"),Monster.MonsterStats.HR);
        statDictionary.Add(monsterStats.Find("CRITRATE"),Monster.MonsterStats.CRITRATE);
        statDictionary.Add(monsterStats.Find("CRITDAME"),Monster.MonsterStats.CRITDAME);
    }

    private void SetValue()
    {
        foreach (KeyValuePair<Transform,Monster.MonsterStats> item in statDictionary)
        {
            Text valueText = item.Key.Find("Value").GetComponent<Text>();
            valueText.text = monster.GetMonsterStat(item.Value).ToString();
        }
    }
}
