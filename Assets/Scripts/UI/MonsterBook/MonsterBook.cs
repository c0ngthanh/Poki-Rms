using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBook : MonoBehaviour
{
    [SerializeField] private Transform monsterList;
    [SerializeField] private Inventory monsterInventory;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Inventory currentInventory;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button previousPageButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button playerTabButton;
    [SerializeField] private Button MonsterTabButton;
    [SerializeField] private TMP_Text MonsterBookText;
    private void Awake(){
        closeButton.onClick.AddListener(DestroyUI);
        playerTabButton.onClick.AddListener(ShowPlayerPage);
        MonsterTabButton.onClick.AddListener(ShowMonsterPage);
        nextPageButton.onClick.AddListener(NextPage);
        previousPageButton.onClick.AddListener(PreviousPage);
    }
    private void Start()
    {
        MonsterBookItem item = Resources.Load<MonsterBookItem>("Prefab/UI/MonsterBookItem");
        monsterInventory = new Inventory(8, item.gameObject);
        playerInventory = new Inventory(8, item.gameObject);
        foreach (Monster monster in MonsterData.allMonsters)
        {
            MonsterBookItem tempItem = Instantiate(item, monsterList);
            tempItem.LoadSprite(monster);
            monsterInventory.Add(tempItem);
        }
        foreach (Monster monster in PlayerController.instance.GetMonstersList())
        {
            MonsterBookItem tempItem = Instantiate(item, monsterList);
            // print(monster.GetMonsterATK());
            tempItem.LoadSprite(monster);
            playerInventory.Add(tempItem);
        }
        ShowPlayerPage();
    }
    public void DestroyUI(){
        Destroy(gameObject);
    }
    public void ShowPlayerPage()
    {
        playerInventory.ShowBookFirstTime();
        currentInventory = playerInventory;
        monsterInventory.Hide();
        MonsterBookText.text = "Player Monsters";
    }
    public void ShowMonsterPage()
    {
        monsterInventory.ShowBookFirstTime();
        currentInventory = monsterInventory;
        playerInventory.Hide();
        MonsterBookText.text = "All Monsters";
    }
    public void NextPage()
    {
        currentInventory.NextPage();
    }
    public void PreviousPage()
    {
        currentInventory.PreviousPage();
    }
}
