using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameData
{
    [Serializable]
    public class MonsterDataSave
    {
        public string id;
        public int atk;
        public int def;
        public int maxEnergy;
        public int speed;
        public int ER;
        public int HR;
        public int critRate;
        public int critDame;
        public int level;
        public MonsterDataSave(Monster monster)
        {
            id = monster.GetMonsterSO().ID;
            atk = monster.GetMonsterStat(Monster.MonsterStats.ATK);
            def = monster.GetMonsterStat(Monster.MonsterStats.DEF);
            maxEnergy = monster.GetMonsterStat(Monster.MonsterStats.MAXENERGY);
            speed = monster.GetMonsterStat(Monster.MonsterStats.SPEED);
            ER = monster.GetMonsterStat(Monster.MonsterStats.ER);
            HR = monster.GetMonsterStat(Monster.MonsterStats.HR);
            critRate = monster.GetMonsterStat(Monster.MonsterStats.CRITRATE);
            critDame = monster.GetMonsterStat(Monster.MonsterStats.CRITDAME);
            level = monster.GetMonsterStat(Monster.MonsterStats.LEVEL);
        }
    }
    public string Name;
    public List<MonsterDataSave> monsterLists;
    public Monster activeMonster;
    public Vector3 playerPosition;
    public int ticket;
    public int coin;
    public GameData()
    {
        Name = "";
        monsterLists = new List<MonsterDataSave>();
        coin = 500;
        ticket = 0;
        playerPosition = Vector3.zero;
        activeMonster = null;
    }
    public GameData(PlayerController playerController, string name)
    {
        Name = name;
        coin = playerController.GetCoin();
        ticket = playerController.GetTicket();
        playerPosition = playerController.transform.position;
        activeMonster = playerController.activeMonster;
        monsterLists = new List<MonsterDataSave>();
        foreach (Monster monster in playerController.GetMonstersList())
        {
            monsterLists.Add(new MonsterDataSave(monster));
        }
    }
}
// Start is called before the first frame update
// [ExecuteInEditMode]
public class SaveLoadSystem : MonoBehaviour
{
    public string tempSaveFile = "abcdefghtempczhxkjch";
    public static SaveLoadSystem Instance;
    [SerializeField] public GameData gameData;
    [SerializeField] public PlayerController player;
    [SerializeField] public MonsterSO[] monsterSOList;
    IDataService dataService;
    private bool isFirstLoad = true;
    private void Awake()
    {
        dataService = new FileDataService(new JsonSerializer());
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (GameObject.FindGameObjectWithTag("Player") && arg0.name == "2DTopDown")
        {
            player = PlayerController.instance;
            LoadMonster();
        }
    }

    public void LoadFile(string filename = null)
    {
        if (filename == null)
        {
            gameData = new GameData();
        }
        else
        {
            Load(filename);
        }
    }
    public void Save()
    {
        if (player != null)
        {
            gameData = new GameData(player,gameData.Name);
        }
        dataService.Save(gameData);
    }
    public void Save(string fileName)
    {
        if (player != null)
        {
            dataService.Save(new GameData(player,fileName));
        }
    }
    public void Load(string gameName)
    {
        gameData = dataService.Load(gameName);
        Debug.Log("Load game successfully");
    }
    public void LoadMonster()
    {
        if(gameData.activeMonster != null){
            PlayerController.instance.SetActiveMonster(gameData.activeMonster);
        }
        PlayerController.instance.transform.position = gameData.playerPosition;
        PlayerController.instance.SetCoin(gameData.coin);
        PlayerController.instance.SetTicket(gameData.ticket);
        foreach (GameData.MonsterDataSave data in gameData.monsterLists)
        {
            foreach (Monster monster in GameManager.allMonsters)
            {
                if (data.id == monster.GetMonsterSO().ID)
                {
                    // Monster temp = Resources.Load<Monster>("Prefab/Monster/" + monster.GetMonsterSO().name);
                    // temp.SetMonsterStats(Monster.MonsterStats.ATK,data.atk);
                    Monster temp1 = new Monster(data.id, data.atk, monster.GetMonsterSO(), data.def,
                        data.maxEnergy,
                        data.speed,
                        data.ER,
                        data.HR,
                        data.critRate,
                        data.critDame,
                        data.level);
                    Monster temp = monster;
                    temp.SetMonsterStatFromData(temp1);
                    player.GetMonstersList().Add(temp);
                }
            }
        }
    }
}
