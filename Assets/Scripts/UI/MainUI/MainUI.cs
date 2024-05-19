using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;
    public GameObject UIRoot;
    public Button monsterBookButton;
    public Button gachaButton;
    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
        monsterBookButton.onClick.AddListener(() => OpenUI("MonsterBook"));
        gachaButton.onClick.AddListener(() => OpenUI("GachaPanel"));
    }

    public GameObject OpenUI(string name)
    {
        return Instantiate(Resources.Load("Prefab/UI/" + name) as GameObject,UIRoot.transform);
    }
}
