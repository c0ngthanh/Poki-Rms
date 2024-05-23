using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private GameObject VFXEffect;
    [SerializeField] private Transform p1Stand;
    [SerializeField] private Transform p2Stand;
    private GameObject enemyPrefab;
    private GameObject playerPrefab;
    //Private variables
    private CharacterBattle player1Character;
    private CharacterBattle player2Character;
    private CharacterBattle activeCharacter;
    private State state;
    private bool isElement = false;
    public event EventHandler onBattleComplete;
    private enum State
    {
        WaitingForPlayer,
        Busy,
    }
    public void SetUpBattle(Monster monster1,Monster monster2)
    {
        playerPrefab = Resources.Load<GameObject>("Prefab/Monster/" + monster1.GetMonsterSO().name);
        enemyPrefab = Resources.Load<GameObject>("Prefab/Monster/" + monster2.GetMonsterSO().name);
        
        
        player1Character = SpawnCharacter(true,monster1);
        player2Character = SpawnCharacter(false,monster2);
        SetActiveCharacter(player1Character);
        player1Character.GetComponent<Monster>().OnMonsterStatsChange += BattleManager.instance.UpdateUI;
        player2Character.GetComponent<Monster>().OnMonsterStatsChange += BattleManager.instance.UpdateUI;
        SetUpMonster();
        player1Character.GetComponent<Monster>().SetMonsterStatFromData(monster1);
        player2Character.GetComponent<Monster>().SetMonsterStatFromData(monster2);
        player1Character.GetComponent<Monster>().UpdateMonsterNotification();
        player2Character.GetComponent<Monster>().UpdateMonsterNotification();
    }
    public void BattleHandlerUpdate()
    {
        if (state == State.WaitingForPlayer)
        {
            state = State.Busy;
            if (activeCharacter.GetComponent<Monster>().GetIsSkill())
            {
                if (activeCharacter == player1Character)
                {
                    player1Character.GetComponent<Monster>().GetSkillBase().Activate(player2Character.gameObject, () =>
                {
                    BattleManager.instance.ShowUIAfterAttack();
                    ChooseNextActiveCharacter();
                    player1Character.GetComponent<Monster>().SetMonsterStats(Monster.MonsterStats.ENERGY,0);
                });
                }
                else
                {
                    player2Character.GetComponent<Monster>().GetSkillBase().Activate(player1Character.gameObject, () =>
                {
                    BattleManager.instance.ShowUIAfterAttack();
                    ChooseNextActiveCharacter();
                    player2Character.GetComponent<Monster>().SetMonsterStats(Monster.MonsterStats.ENERGY,0);
                });
                }
            }
            else
            {
                if (activeCharacter == player1Character)
                {
                    player1Character.Attack(player2Character, () =>
                    {
                        BattleManager.instance.ShowUIAfterAttack();
                        ChooseNextActiveCharacter();
                    });
                }
                else
                {
                    player2Character.Attack(player1Character, () =>
                    {
                        BattleManager.instance.ShowUIAfterAttack();
                        ChooseNextActiveCharacter();
                    });
                }
            }
        }
    }
    private void ChooseNextActiveCharacter()
    {
        if (activeCharacter == player1Character)
        {
            SetActiveCharacter(player2Character);
            state = State.WaitingForPlayer;
        }
        else
        {
            SetActiveCharacter(player1Character);
            state = State.WaitingForPlayer;
        }
    }
    private CharacterBattle SpawnCharacter(bool isPlayerTeam, Monster monster)
    {
        Vector2 position;
        GameObject characterGO;
        if (isPlayerTeam)
        {
            position = p1Stand.position;
            characterGO = Instantiate(playerPrefab, position, Quaternion.identity);
            characterGO.gameObject.transform.localScale = new Vector3(characterGO.gameObject.transform.localScale.x * -1, characterGO.gameObject.transform.localScale.y, characterGO.gameObject.transform.localScale.y);
        }
        else
        {
            position = p2Stand.position;
            characterGO = Instantiate(enemyPrefab, position, Quaternion.identity);
        }
        // characterGO.GetComponent<Monster>().SetMonsterStatFromData(monster);
        CharacterBattle characterBattle = characterGO.GetComponent<CharacterBattle>();
        return characterBattle;
    }
    private void SetActiveCharacter(CharacterBattle characterBattle)
    {
        activeCharacter = characterBattle;
    }
    public void UpdateStats(Dictionary<GemSO, int> gemSOList)
    {
        activeCharacter.GetComponent<Monster>().UpdateStats(gemSOList);
    }
    public void SetUpMonster()
    {
        player1Character.GetComponent<Monster>().SetupMonster();
        player2Character.GetComponent<Monster>().SetupMonster();
    }
    public void BattleDamage(Monster AttackMonster, Monster AttackedMonster)
    {
        bool isCrit = false;
        float effectiveBonus = GameConfig.baseCounterBonus;
        if (isElement)
        {
            effectiveBonus = GetEffectiveBonus(AttackMonster.GetMonsterType(), AttackedMonster.GetMonsterType());
        }
        int monsterDEF = AttackedMonster.GetMonsterDef();
        float attackDame = AttackMonster.GetMonsterATK() * effectiveBonus * (1 - (monsterDEF / (monsterDEF + 100f)));
        if (AttackMonster.GetMonsterCritRate() > UnityEngine.Random.Range(0, 100))
        {
            isCrit = true;
            attackDame = attackDame * AttackedMonster.GetMonsterCritDame() / 100;
        }
        AttackedMonster.SetMonsterStats(Monster.MonsterStats.HP, AttackedMonster.GetMonsterHP() - attackDame);
        BattleManager.instance.ShowDamage(AttackedMonster.transform.position, ((int)attackDame).ToString(), AttackMonster.GetMonsterType(), isCrit, isElement);
        if(AttackedMonster.GetMonsterHP() <= 0){
            AttackedMonster.GetComponent<CharacterBattle>().isDead = true;
        }
        BattleManager.instance.TrySetBattleState();
    }
    public void PlayVFX(Transform attackedMonster)
    {
        Instantiate(VFXEffect, attackedMonster.position + Vector3.up * 2 - 5 * Vector3.forward, Quaternion.identity);
    }
    private float GetEffectiveBonus(Monster.MonsterType AttackMonsterType, Monster.MonsterType AttackedMonsterType)
    {
        switch (AttackMonsterType)
        {
            case Monster.MonsterType.Fire:
                if (AttackedMonsterType == Monster.MonsterType.Grass)
                {
                    return GameConfig.effectiveBonus;
                }
                if (AttackedMonsterType == Monster.MonsterType.Water)
                {
                    return GameConfig.uneffectiveBonus;
                }
                break;
            case Monster.MonsterType.Water:
                if (AttackedMonsterType == Monster.MonsterType.Fire)
                {
                    return GameConfig.effectiveBonus;
                }
                if (AttackedMonsterType == Monster.MonsterType.Grass || AttackedMonsterType == Monster.MonsterType.Electric)
                {
                    return GameConfig.uneffectiveBonus;
                }
                break;
            case Monster.MonsterType.Grass:
                if (AttackedMonsterType == Monster.MonsterType.Water || AttackedMonsterType == Monster.MonsterType.Electric)
                {
                    return GameConfig.effectiveBonus;
                }
                if (AttackedMonsterType == Monster.MonsterType.Fire)
                {
                    return GameConfig.uneffectiveBonus;
                }
                break;
            case Monster.MonsterType.Electric:
                if (AttackedMonsterType == Monster.MonsterType.Water)
                {
                    return GameConfig.effectiveBonus;
                }
                if (AttackedMonsterType == Monster.MonsterType.Grass)
                {
                    return GameConfig.uneffectiveBonus;
                }
                break;
            case Monster.MonsterType.Dark:
                if (AttackedMonsterType == Monster.MonsterType.Light)
                {
                    return GameConfig.effectiveBonus;
                }
                break;
            case Monster.MonsterType.Light:
                if (AttackedMonsterType == Monster.MonsterType.Dark)
                {
                    return GameConfig.effectiveBonus;
                }
                break;
        }
        return GameConfig.baseCounterBonus;
    }
    public Monster GetMonster1() { return player1Character.GetComponent<Monster>(); }
    public Monster GetMonster2() { return player2Character.GetComponent<Monster>(); }
    public CharacterBattle GetCharacterBattle1() { return player1Character; }
    public CharacterBattle GetCharacterBattle2() { return player2Character; }
    public void SetElement(bool element)
    {
        this.isElement = element;
    }
    public BattleState GetBattleState(){
        if(player1Character.isDead){
            return BattleState.Lose;
        }else if(player2Character.isDead){
            return BattleState.Win;
        }
        return BattleState.Ingame;
    }
}
