using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum MonsterStats
    {
        HP,
        ATK,
        DEF,
        ENERGY,
        SPEED,
        ER,
        HR,
        CRITRATE,
        CRITDAME
    }
    public enum MonsterType
    {
        Fire,
        Water,
        Grass,
        Electric,
        Light,
        Dark
    }
    //Prototype
    [SerializeField] private int baseHP;
    [SerializeField] private MonsterType type;
    [SerializeField] private int baseATK;
    [SerializeField] private int baseDEF;
    [SerializeField] private int baseSpeed;
    [SerializeField] private int baseER;
    [SerializeField] private int baseEnergy;
    [SerializeField] private int baseHR;
    [SerializeField] private int baseCritRate;
    [SerializeField] private int baseCritDame;
    [SerializeField] private bool isSkill;
    private int HP;
    private int ATK;
    private int DEF;
    private int energy;
    private int speed;
    private int ER;
    private int HR;
    private int critRate;
    private int critDame;
    //stats increase Rate
    private float ATK_INCREASE_RATE = 0.1f;
    private float DEF_INCREASE_RATE = 0.1f;
    private float HEAL_INCREASE_RATE = 0.05f;
    private float ENERGY_INCREASE_RATE = 5;

    public void SetupMonster(){
        HP = baseHP;
        DEF = baseDEF;
        ATK = baseATK;
        energy = 0;
        speed = baseSpeed;
        ER = baseER;
        HR = baseHR;
        critRate = baseCritRate;
        critDame = baseCritDame;
    }
    public void UpdateStats(Dictionary<GemSO, int> gemSOList)
    {
        foreach (KeyValuePair<GemSO, int> item in gemSOList)
        {
            if (item.Value == 0)
            {
                continue;
            }
            switch (item.Key.gemType)
            {
                case GemSO.Type.ATK:
                    ATK += (int)(baseATK * ATK_INCREASE_RATE * item.Value);
                    break;
                case GemSO.Type.DEF:
                    DEF += (int)(baseDEF * DEF_INCREASE_RATE * item.Value);
                    break;
                case GemSO.Type.HEAL:
                    HP += (int)(baseHP * HEAL_INCREASE_RATE * item.Value);
                    if (HP > baseHP)
                    {
                        HP = baseHP;
                    }
                    break;
                case GemSO.Type.ENERGY:
                    energy += (int)(ENERGY_INCREASE_RATE * item.Value * ER / 100);
                    if (energy > baseEnergy)
                    {
                        energy = baseEnergy;
                    }
                    break;
                case GemSO.Type.ELEMENT:
                    GameManager.instance.battleHandler.SetElement(true);
                break;
            }
        }
    }
    public void SetMonsterStats(MonsterStats stats,float value){
        switch (stats)
        {
            case MonsterStats.HP: HP = (int)value; break;
            case MonsterStats.ATK: ATK = (int)value; break;
            case MonsterStats.DEF: DEF = (int)value; break;
            case MonsterStats.SPEED: speed = (int)value; break;
            case MonsterStats.ENERGY: energy = (int)value; break;
            case MonsterStats.HR: HR = (int)value; break;
            case MonsterStats.ER: ER = (int)value; break;
            case MonsterStats.CRITDAME: critDame = (int)value; break;
            case MonsterStats.CRITRATE: critRate = (int)value; break;
            default: Debug.Log("Unknown Stats"); break;
        }
    }
    public int GetMonsterHP(){
        return HP;
    }
    public int GetMonsterDef(){
        return DEF;
    }
    public int GetMonsterCritRate(){
        return critRate;
    }
    public int GetMonsterCritDame(){
        return critDame;
    }
    public int GetMonsterATK(){
        return ATK;
    }
    public int GetMonsterEnergy(){
        return energy;
    }
    public int GetMonsterMaxEnergy(){
        return baseEnergy;
    }
    public int GetMonsterMaxHP(){
        return baseHP;
    }
    public SkillBase GetSkillBase(){
        return GetComponent<SkillBase>();
    }
    public bool GetIsSkill(){
        return isSkill;
    }
    public void SetIsSkill(bool value){
        isSkill=value;
    }
    public MonsterType GetMonsterType(){
        return type;
    }
}
