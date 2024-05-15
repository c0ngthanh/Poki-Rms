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
    [SerializeField] private Sprite[] icons;
    [SerializeField] private Image[] starIcon;
    private Monster monster;
    // Start is called before the first frame update
    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }
    public void SetMonster(Monster value)
    {
        this.monster = value;
        SetReference();
        SetValue();
    }
    public static string fileName()
    {
        return "Prefab/UI/MonsterInfo";
    }

    private void SetReference()
    {
        Transform monsterStats = transform.Find("MonsterStats");
        statDictionary.Add(monsterStats.Find("HP"), Monster.MonsterStats.HP);
        statDictionary.Add(monsterStats.Find("ATK"), Monster.MonsterStats.ATK);
        statDictionary.Add(monsterStats.Find("DEF"), Monster.MonsterStats.DEF);
        statDictionary.Add(monsterStats.Find("ENERGY"), Monster.MonsterStats.MAXENERGY);
        statDictionary.Add(monsterStats.Find("SPEED"), Monster.MonsterStats.SPEED);
        statDictionary.Add(monsterStats.Find("ER"), Monster.MonsterStats.ER);
        statDictionary.Add(monsterStats.Find("HR"), Monster.MonsterStats.HR);
        statDictionary.Add(monsterStats.Find("CRITRATE"), Monster.MonsterStats.CRITRATE);
        statDictionary.Add(monsterStats.Find("CRITDAME"), Monster.MonsterStats.CRITDAME);
    }

    private void SetValue()
    {
        //SetReference
        Image monsterImage = transform.Find("MonsterImage").GetComponent<Image>();
        Text monsterName = transform.transform.Find("MonsterName").GetComponent<Text>();
        Image monsterType = monsterName.transform.Find("Type").GetComponent<Image>();
        Text monsterLevel = transform.transform.Find("Level/Value").GetComponent<Text>();


        MonsterSO monsterSO = this.monster.GetMonsterSO();
        float ITEM_HEIGHT = 250;
        monsterImage.sprite = Resources.Load<Sprite>($"MonsterUI/" + monsterSO.monsterName);
        monsterImage.sprite = Resources.Load<Sprite>($"MonsterUI/" + monsterSO.monsterName);
        monsterImage.SetNativeSize();
        float scale = monsterImage.GetComponent<RectTransform>().rect.width / monsterImage.GetComponent<RectTransform>().rect.height;
        monsterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ITEM_HEIGHT * scale, ITEM_HEIGHT);
        //
        monsterName.text = monsterSO.monsterName;
        monsterType.sprite = icons[monster.GetMonsterStat(Monster.MonsterStats.TYPE)];
        monsterLevel.text = monster.GetMonsterStat(Monster.MonsterStats.LEVEL).ToString();
        int star = monster.GetMonsterStat(Monster.MonsterStats.STAR);
        for (int i = 0; i < starIcon.Length; i++)
        {
            if(i+1>star){
                starIcon[i].color = Color.black;
            }
        }
        
        //Set Value
        foreach (KeyValuePair<Transform, Monster.MonsterStats> item in statDictionary)
        {
            Text valueText = item.Key.Find("Value").GetComponent<Text>();
            valueText.text = monster.GetMonsterStat(item.Value).ToString();
        }
    }
}
