using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveMonster : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    public Image monsterImage;
    public TMP_Text monsterName;
    public TMP_Text monsterLevel;
    private Monster monster;
    private ActiveMonsterPanel panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = Resources.Load<ActiveMonsterPanel>("Prefab/UI/ActiveMonsterPanel");
        GetComponent<Button>().onClick.AddListener(()=>{
            Instantiate(panel,MainUI.Instance.UIRoot.transform);
        });
        if(PlayerController.instance.activeMonster == null){
            Hide();
        }else{
            Show();
        }
    }
    public void Hide(){
        monsterImage.gameObject.SetActive(false);
        monsterName.gameObject.SetActive(false);
        monsterLevel.gameObject.SetActive(false);
    }
    public void Show(){
        monsterImage.gameObject.SetActive(true);
        monsterName.gameObject.SetActive(true);
        monsterLevel.gameObject.SetActive(true);
        monster = PlayerController.instance.activeMonster;
        MonsterSO monsterSO = monster.GetMonsterSO();
        monsterImage.sprite = Resources.Load<Sprite>($"MonsterUI/"+monsterSO.monsterName);
        monsterImage.SetNativeSize();
        float scale = monsterImage.GetComponent<RectTransform>().rect.width/monsterImage.GetComponent<RectTransform>().rect.height;
        monsterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(150*scale,150);
        monsterName.text = monsterSO.monsterName;
        monsterLevel.text = "Lv " + monster.GetMonsterStat(Monster.MonsterStats.LEVEL).ToString();
    }
}
