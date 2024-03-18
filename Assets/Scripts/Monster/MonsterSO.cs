using UnityEngine;

[CreateAssetMenu(menuName = "SO/MonsterSO")]
public class MonsterSO : ScriptableObject
{
    public string monsterName;
    public string ID;
    public Monster.MonsterType type;
    public int baseHP;
    public int baseATK;
    public int baseDEF;
    public int baseSpeed;
    public int baseER;
    public int baseEnergy;
    public int baseHR;
    public int baseCritRate;
    public int baseCritDame;
}
