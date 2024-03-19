using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBookItem : ItemBase
{
    private const float ITEM_HEIGHT = 50f;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image monsterImage;
    [SerializeField] private TMP_Text monsterName;
    public override void LoadSprite(GameObject obj)
    {
        MonsterSO monsterSO = obj.GetComponent<Monster>().GetMonsterSO();
        monsterImage.sprite = Resources.Load<Sprite>($"MonsterUI/"+monsterSO.monsterName);
        monsterImage.SetNativeSize();
        // Debug.Log(image.GetComponent<RectTransform>().rect.width);
        float scale = monsterImage.GetComponent<RectTransform>().rect.width/monsterImage.GetComponent<RectTransform>().rect.height;
        monsterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ITEM_HEIGHT*scale,ITEM_HEIGHT);
        monsterName.text = monsterSO.monsterName;
    }
}
