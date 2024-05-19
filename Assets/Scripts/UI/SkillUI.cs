using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] GameObject P1Skill;
    [SerializeField] Monster P1Monster;
    // [SerializeField] GameObject P2Skill;
    public void SetUp()
    {
        P1Monster = BattleManager.instance.battleHandler.GetCharacterBattle1().GetComponent<Monster>();
        P1Monster.OnMonsterStatsChange += OnMonsterStatsChange;
        P1Skill.GetComponent<Toggle>().onValueChanged.AddListener((bool value) =>
        {
            P1Monster.SetIsSkill(value);
        });
        // P2Skill.GetComponent<Toggle>().onValueChanged.AddListener((bool value)=>{
        //     BattleManager.instance.battleHandler.GetCharacterBattle2().GetComponent<Monster>().SetIsSkill(value);
        // });
        // P1Skill.GetComponent<Toggle>().interactable = false;
        // P2Skill.GetComponent<Toggle>().interactable = false;
        // P2Skill.GetComponent<Toggle>().onValueChanged.AddListener((bool value)=>{
        //     BattleManager.instance.battleHandler.GetCharacterBattle2().GetComponent<Monster>().SetIsSkill(value);
        // });
        P1Skill.GetComponent<Image>().sprite = BattleManager.instance.battleHandler.GetCharacterBattle1().GetComponent<Monster>().GetSkillBase().icon;
        SetSkillIteraction(P1Monster.GetMonsterEnergy(),P1Monster.GetMonsterMaxEnergy());
        // P2Skill.GetComponent<Image>().sprite = BattleManager.instance.battleHandler.GetCharacterBattle2().GetComponent<Monster>().GetSkillBase().icon;
    }

    private void OnMonsterStatsChange(object sender, Monster.MonsterStatsChangeEventArgs e)
    {
        SetSkillIteraction(e.energy,e.maxEnergy);
    }

    public void SetSkillIteraction(int currentMana, int maxMana)
    {
        if (currentMana == maxMana)
        {
            P1Skill.GetComponent<Toggle>().interactable = true;
        }else{
            if(P1Skill.GetComponent<Toggle>().isOn==true){
                P1Skill.GetComponent<Toggle>().isOn = false;
            }
            P1Skill.GetComponent<Toggle>().interactable = false;
        }
    }
}
