using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveMonsterPanel : MonoBehaviour
{
    [SerializeField] private Transform monsterList;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button previousPageButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Text MonsterBookText;
    private void Awake(){
        closeButton.onClick.AddListener(DestroyUI);
        nextPageButton.onClick.AddListener(NextPage);
        previousPageButton.onClick.AddListener(PreviousPage);
    }
    private void Start()
    {
        ActiveMonsterItem item = Resources.Load<ActiveMonsterItem>("Prefab/UI/ActiveMonsterItem");
        playerInventory = new Inventory(2, item.gameObject);
        foreach (Monster monster in PlayerController.instance.GetMonstersList())
        {
            ActiveMonsterItem tempItem = Instantiate(item, monsterList);
            tempItem.LoadSprite(monster);
            playerInventory.Add(tempItem);
        }
        playerInventory.ShowBookFirstTime();
    }
    public void DestroyUI(){
        Destroy(gameObject);
    }

    public void NextPage()
    {
        playerInventory.NextPage();
    }
    public void PreviousPage()
    {
        playerInventory.PreviousPage();
    }
}
