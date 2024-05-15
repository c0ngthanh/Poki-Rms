using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public class MonsterStatsChangeEventArgs : EventArgs
    {
        public int level;
        public int star;
        public int HP;
        public int ATK;
        public int DEF;
        public int energy;
        public int speed;
        public int ER;
        public int HR;
        public int critRate;
        public int critDame;
        public int maxEnergy;
        public int maxHP;
    }
    public event EventHandler<MonsterStatsChangeEventArgs> OnMonsterStatsChange;
    public enum MonsterStats
    {
        HP,
        ATK,
        DEF,
        ENERGY,
        MAXENERGY,
        SPEED,
        ER,
        HR,
        CRITRATE,
        CRITDAME,
        LEVEL,
        JOB,
        STAR,
        TYPE
    }
    public enum MonsterType
    {
        Fire =1,
        Water=2,
        Grass=3,
        Electric=4,
        Light=5,
        Dark=6
    }
    //Prototype
    [SerializeField] private MonsterSO monsterSO;
    private int baseHP;
    private int baseLevel;
    private MonsterJob job;
    private int baseStar;
    private string ID;
    private MonsterType type;
    private int baseATK;
    private int baseDEF;
    private int baseSpeed;
    private int baseER;
    private int baseEnergy;
    private int baseHR;
    private int baseCritRate;
    private int baseCritDame;
    [SerializeField] private bool isSkill;
    private int HP;
    private int ATK;
    private int level;
    private int star;
    private int DEF;
    private int energy;
    private int speed;
    private int ER;
    private int HR;
    private int critRate;
    private int critDame;
    private int maxEnergy;
    private int maxHP;
    //stats increase Rate
    private float ATK_INCREASE_RATE = 0.1f;
    private float DEF_INCREASE_RATE = 0.1f;
    private float HEAL_INCREASE_RATE = 0.05f;
    private float ENERGY_INCREASE_RATE = 5;
    public Monster(string ID,int atk,MonsterSO monsterSO){
        this.ATK = atk;
        this.ID = ID;
        this.monsterSO =monsterSO;
    }
    private void SetUpBaseStats()
    {
        baseLevel = monsterSO.level;
        baseStar = monsterSO.star;
        job = monsterSO.job;
        baseHP= monsterSO.baseHP;
        ID= monsterSO.ID;
        type= monsterSO.type;
        baseATK= monsterSO.baseATK;
        baseDEF= monsterSO.baseDEF;
        baseSpeed= monsterSO.baseSpeed;
        baseER= monsterSO.baseER;
        baseEnergy= monsterSO.baseEnergy;
        baseHR= monsterSO.baseHR;
        baseCritRate= monsterSO.baseCritRate;
        baseCritDame= monsterSO.baseCritDame;
    }
    public void SetupMonster()
    {
        SetUpBaseStats();
        star = baseStar;
        level = baseLevel;
        HP = baseHP;
        DEF = baseDEF;
        ATK = baseATK;
        energy = 0;
        maxEnergy = baseEnergy;
        maxHP = baseHP;
        speed = baseSpeed;
        ER = baseER;
        HR = baseHR;
        critRate = baseCritRate;
        critDame = baseCritDame;
        UpdateMonsterNotification();
    }
    public void SetupMonsterWithOutRegisterEvent()
    {
        SetUpBaseStats();
        star = baseStar;
        level = baseLevel;
        HP = baseHP;
        DEF = baseDEF;
        ATK = baseATK;
        energy = 0;
        maxEnergy = baseEnergy;
        maxHP = baseHP;
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
        UpdateMonsterNotification();
    }
    public void SetMonsterStats(MonsterStats stats, float value)
    {
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
            case MonsterStats.LEVEL: level = (int)value; break;
            case MonsterStats.STAR: star = (int)value; break;
            case MonsterStats.TYPE: type = (MonsterType)(int)value; break;
            case MonsterStats.JOB: job = (MonsterJob)(int)value; break;
            default: Debug.Log("Unknown Stats"); break;
        }
        UpdateMonsterNotification();
    }
    private void UpdateMonsterNotification()
    {
        OnMonsterStatsChange?.Invoke(this, new MonsterStatsChangeEventArgs
        {
            level = this.level,
            star = this.star,
            HP = this.HP,
            ATK = this.ATK,
            DEF = this.DEF,
            energy = this.energy,
            speed = this.speed,
            ER = this.ER,
            HR = this.HR,
            critRate = this.critRate,
            critDame = this.critDame,
            maxEnergy = this.maxEnergy,
            maxHP = this.maxHP
        });
    }
    public int GetMonsterHP()
    {
        return HP;
    }
    public int GetMonsterDef()
    {
        return DEF;
    }
    public int GetMonsterCritRate()
    {
        return critRate;
    }
    public int GetMonsterCritDame()
    {
        return critDame;
    }
    public int GetMonsterATK()
    {
        return ATK;
    }
    public int GetMonsterEnergy()
    {
        return energy;
    }
    public int GetMonsterMaxEnergy()
    {
        return baseEnergy;
    }
    public int GetMonsterMaxHP()
    {
        return baseHP;
    }
    public SkillBase GetSkillBase()
    {
        return GetComponent<SkillBase>();
    }
    public bool GetIsSkill()
    {
        return isSkill;
    }
    public void SetIsSkill(bool value)
    {
        isSkill = value;
    }
    public MonsterType GetMonsterType()
    {
        return type;
    }
    public MonsterSO GetMonsterSO(){
        return monsterSO;
    }
    public void SetMonsterSO(MonsterSO value){
        monsterSO = value;
    }
    public int GetMonsterStat(MonsterStats statsType){
        switch (statsType)
        {
            case MonsterStats.HP: return HP;
            case MonsterStats.SPEED: return speed;
            case MonsterStats.DEF: return DEF;
            case MonsterStats.HR: return HR;
            case MonsterStats.ENERGY: return energy;
            case MonsterStats.MAXENERGY: return maxEnergy;
            case MonsterStats.ATK: return ATK;
            case MonsterStats.CRITRATE: return critRate;
            case MonsterStats.CRITDAME: return critDame;
            case MonsterStats.ER: return ER;
            case MonsterStats.LEVEL: return level;
            case MonsterStats.JOB: return (int)job;
            case MonsterStats.STAR: return star;
            case MonsterStats.TYPE: return (int)this.type;
        }
        return -1;
    }
}
