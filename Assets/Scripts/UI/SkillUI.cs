using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] GameObject P1Skill;
    // [SerializeField] GameObject P2Skill;
    public void SetUp(){
        P1Skill.GetComponent<Toggle>().onValueChanged.AddListener((bool value)=>{
            GameManager.instance.battleHandler.GetCharacterBattle1().GetComponent<Monster>().SetIsSkill(value);
        });
        // P2Skill.GetComponent<Toggle>().onValueChanged.AddListener((bool value)=>{
        //     GameManager.instance.battleHandler.GetCharacterBattle2().GetComponent<Monster>().SetIsSkill(value);
        // });
        // P1Skill.GetComponent<Toggle>().interactable = false;
        // P2Skill.GetComponent<Toggle>().interactable = false;
        // P2Skill.GetComponent<Toggle>().onValueChanged.AddListener((bool value)=>{
        //     GameManager.instance.battleHandler.GetCharacterBattle2().GetComponent<Monster>().SetIsSkill(value);
        // });
        P1Skill.GetComponent<Image>().sprite = GameManager.instance.battleHandler.GetCharacterBattle1().GetComponent<Monster>().GetSkillBase().icon;
        // P2Skill.GetComponent<Image>().sprite = GameManager.instance.battleHandler.GetCharacterBattle2().GetComponent<Monster>().GetSkillBase().icon;
    }
}
