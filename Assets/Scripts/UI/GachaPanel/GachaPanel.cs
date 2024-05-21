using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaPanel : MonoBehaviour
{
    public Button draw1Button;
    public Button infoButton;
    public Button closeButton;
    private LoadPanel loadPanel;
    private List<Monster> fourStarMonster = new List<Monster>();
    private List<Monster> fiveStarMonster = new List<Monster>();
    private Monster newMonster;
    public TMP_Text currentTicketText;
    private string rateUpID = "1016";
    private float rate5Star = 10;
    // Start is called before the first frame update
    private void Awake()
    {
        loadPanel = Resources.Load<LoadPanel>("Prefab/UI/LoadPanel");
        draw1Button.onClick.AddListener(DrawTicket);
        infoButton.onClick.AddListener(ShowRateUpMonster);
        closeButton.onClick.AddListener(DestroyUI);
    }

    private void ShowRateUpMonster()
    {
        foreach (Monster monster in fiveStarMonster)
        {
            if (monster.GetMonsterSO().ID == rateUpID)
            {
                MonsterInfo info = Instantiate(Resources.Load<MonsterInfo>(MonsterInfo.fileName()), MainUI.Instance.UIRoot.transform);
                info.SetMonster(monster);
                return;
            }
        }

    }
    public void DestroyUI()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        foreach (Monster monster in GameManager.allMonsters)
        {
            if (monster.GetMonsterSO().star == 4)
            {
                fourStarMonster.Add(monster);
            }
            else
            {
                fiveStarMonster.Add(monster);
            }
        }
        currentTicketText.text = "x" + PlayerController.instance.ticket.ToString();
    }
    private int RandomMonsterList(List<Monster> monsters)
    {
        return Random.Range(0, monsters.Count);
    }
    public void DrawTicket()
    {
        if(PlayerController.instance.ticket <= 0){
            return;
        }
        PlayerController.instance.ticket -= 1;
        currentTicketText.text = "x" + PlayerController.instance.ticket.ToString();
        if (Random.Range(0, 100) > rate5Star)
        {
            newMonster = fourStarMonster[RandomMonsterList(fourStarMonster)];
            LoadPanel panel = loadPanel.ShowGachaPanel("Blue");
            // panel.OnAnimationEnd += ShowMonsterInfo;
        }
        else
        {
            if (Random.Range(0, 100) > 50)
            {
                foreach (Monster monster in fiveStarMonster)
                {
                    if (monster.GetMonsterSO().ID == rateUpID)
                    {
                        newMonster = monster;
                    }
                }

            }else{
                newMonster = fiveStarMonster[RandomMonsterList(fiveStarMonster)];
            }
            LoadPanel panel = loadPanel.ShowGachaPanel("Red");
            // panel.OnAnimationEnd += ShowMonsterInfo;
        }
        // info.SetMonster(obj);
        MonsterInfo info = Instantiate(Resources.Load<MonsterInfo>(MonsterInfo.fileName()), MainUI.Instance.UIRoot.transform);
        info.SetMonster(newMonster);
        if(PlayerController.instance.GetMonstersList().Count ==0){
            PlayerController.instance.activeMonster = newMonster;
        }
        PlayerController.instance.GetMonstersList().Add(newMonster);
    }
}
