using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [Serializable]
    public class MonsterDataSave
    {
        public string id;
        public int atk;
    }
    public string Name;
    public int numOfMonsters;
    public List<MonsterDataSave> monsterLists;
}
// Start is called before the first frame update
public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] public GameData gameData;
    [SerializeField] public PlayerController player;
    [SerializeField] public MonsterSO[] monsterSOList;
    IDataService dataService;
    private void Awake()
    {
        dataService = new FileDataService(new JsonSerializer());
    }
    private void Start(){
        LoadMonster();
    }
    public void Save()
    {
        dataService.Save(gameData);
    }
    public void Load(string gameName)
    {
        gameData = dataService.Load(gameName);
    }
    private void LoadMonster(){
        foreach(GameData.MonsterDataSave data in gameData.monsterLists)
        {
            foreach(Monster monster in MonsterData.allMonsters){
                if(data.id == monster.GetMonsterSO().ID){
                    Monster temp = Resources.Load<Monster>("Prefab/Monster/" + monster.GetMonsterSO().name);
                    player.monstersList.Add(temp);
                    Debug.Log(temp);
                    Debug.Log(temp.GetMonsterATK());
                    temp.SetMonsterStats(Monster.MonsterStats.ATK,data.atk);
                    Debug.Log(temp.GetMonsterATK());
                }
            }
        }
    }
}
