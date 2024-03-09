using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AcientAutomatonSkill : SkillBase
{
    public override void Activate(GameObject obj,Action onSkillComplete)
    {
        GetComponent<CharacterBattle>().Skill(obj.GetComponent<CharacterBattle>(),onSkillComplete);
        Debug.Log("AcientAutomatonSkill");
    }
}
