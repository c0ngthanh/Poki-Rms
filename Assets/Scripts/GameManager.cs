using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Toggle toggle;
    public static GameManager instance;
    public Match3Visual match3Visual;
    public Match3 match3;
    public BattleHandler battleHandler;
    public UI ui;
    private void Awake()
    {
        instance = this;
        match3.OnLevelSet += SetUpUI;
    }
    private void Start() {
        // battleHandler.GetMonster1().OnMonsterStatsChange += UpdateUI;
        // battleHandler.GetMonster2().OnMonsterStatsChange += UpdateUI;
        battleHandler.SetUpBattle();
        ui.SetUp();
    }

    public void UpdateUI(object sender, Monster.MonsterStatsChangeEventArgs e)
    {
        Monster temp = sender as Monster;
        if(temp == battleHandler.GetMonster1()){
            ui.battleUI.UpdateUI(1,e.HP,e.energy,e.maxHP,e.maxEnergy);
        }else if(temp == battleHandler.GetMonster2()){
            ui.battleUI.UpdateUI(2,e.HP,e.energy,e.maxHP,e.maxEnergy);
        }else{
            Debug.LogError("Unknown Monster");
        }
    }

    private void SetUpUI(object sender, Match3.OnLevelSetEventArgs e)
    {
        ui.resultPanel.SetUp(e.levelSO);
    }

    private void Update()
    {
        match3Visual.Match3Update();
    }
    public void HideUIWhenAttack(){
        match3Visual.HideGemGrid();
        ui.resultPanel.Show();
    }
    public void ShowUIAfterAttack(){
        match3Visual.ShowGemGrid();
        ui.resultPanel.Hide();
    }
    public void Battle()
    {
        HideUIWhenAttack();
        ui.resultPanel.UpdateResult(match3.totalGemClear);
        battleHandler.UpdateStats(match3.totalGemClear);
        battleHandler.BattleHandlerUpdate();
        // UpdateBattleUI();
        match3.ClearDictionary();
    }
    // public void UpdateBattleUI(){
    //     ui.battleUI.UpdateUI(battleHandler.GetMonster1().GetMonsterHP(),
    //                          battleHandler.GetMonster1().GetMonsterEnergy(),
    //                          battleHandler.GetMonster2().GetMonsterHP(),
    //                          battleHandler.GetMonster2().GetMonsterEnergy());
    // }
}
