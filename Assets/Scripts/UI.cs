using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public ResultPanel resultPanel;
    public BattleUI battleUI;
    public SkillUI skillUI;
    public void SetUp(){
        skillUI.SetUp();
    }
}
