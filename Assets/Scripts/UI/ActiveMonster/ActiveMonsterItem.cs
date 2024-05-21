using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActiveMonsterItem : ItemBase
{
    private const float ITEM_HEIGHT = 100;
    [SerializeField] private Image monsterImage;
    [SerializeField] private TMP_Text monsterName;
    [SerializeField] private TMP_Text monsterLevel;
    public override void LoadSprite(Monster obj)
    {
        GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("hehe");
        });
        MonsterSO monsterSO = obj.GetMonsterSO();
        monsterImage.sprite = Resources.Load<Sprite>($"MonsterUI/"+monsterSO.monsterName);
        monsterImage.SetNativeSize();
        // Debug.Log(image.GetComponent<RectTransform>().rect.width);
        float scale = monsterImage.GetComponent<RectTransform>().rect.width/monsterImage.GetComponent<RectTransform>().rect.height;
        monsterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ITEM_HEIGHT*scale,ITEM_HEIGHT);
        monsterName.text = monsterSO.monsterName;
        monsterLevel.text = "Lv " + obj.GetMonsterStat(Monster.MonsterStats.LEVEL).ToString();
    }
}
