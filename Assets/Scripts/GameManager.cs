using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Monster monster1;
    public Monster monster2;
    public static Monster[] allMonsters;
    public static GameManager Instance;
    [SerializeField] private BattleManager battleManager;
    private void Awake(){
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        allMonsters = Resources.LoadAll<Monster>("Prefab/Monster");
        foreach (Monster item in allMonsters)
        {
            item.SetupMonsterWithOutRegisterEvent();
        }
    }
    public void SetUpBattle(Monster monster1, Monster monster2){
        this.monster1 = monster1;
        this.monster2 = monster2;
    }
    public void SetBattleManager(BattleManager battleManager){
        this.battleManager = battleManager;
    }
}
