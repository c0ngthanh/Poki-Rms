using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBookItem : ItemBase
{
    private const float ITEM_HEIGHT = 150f;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image monsterImage;
    [SerializeField] private TMP_Text monsterName;
    public override void LoadSprite(Monster obj)
    {
        GetComponent<Button>().onClick.AddListener(() => {
            MonsterInfo info = Instantiate(Resources.Load<MonsterInfo>(MonsterInfo.fileName()),MainUI.Instance.UIRoot.transform);
            Debug.Log(obj.GetMonsterATK());
            foreach (Monster monster in PlayerController.instance.GetMonstersList())
            {
                print(monster.GetMonsterATK());
            }
            info.SetMonster(obj);
        });
        MonsterSO monsterSO = obj.GetMonsterSO();
        monsterImage.sprite = Resources.Load<Sprite>($"MonsterUI/"+monsterSO.monsterName);
        monsterImage.SetNativeSize();
        // Debug.Log(image.GetComponent<RectTransform>().rect.width);
        float scale = monsterImage.GetComponent<RectTransform>().rect.width/monsterImage.GetComponent<RectTransform>().rect.height;
        monsterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(ITEM_HEIGHT*scale,ITEM_HEIGHT);
        monsterName.text = monsterSO.monsterName;
    }
}
