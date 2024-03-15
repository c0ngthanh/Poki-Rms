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
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform p1Stand;
    [SerializeField] private Transform p2Stand;
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
    public void SetUpBattle()
    {
        player1Character = SpawnCharacter(true);
        player2Character = SpawnCharacter(false);
        SetActiveCharacter(player1Character);
        player1Character.GetComponent<Monster>().OnMonsterStatsChange += GameManager.instance.UpdateUI;
        player2Character.GetComponent<Monster>().OnMonsterStatsChange += GameManager.instance.UpdateUI;
        SetUpMonster();
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
                    GameManager.instance.ShowUIAfterAttack();
                    ChooseNextActiveCharacter();
                    player1Character.GetComponent<Monster>().SetMonsterStats(Monster.MonsterStats.ENERGY,0);
                });
                }
                else
                {
                    player2Character.GetComponent<Monster>().GetSkillBase().Activate(player1Character.gameObject, () =>
                {
                    GameManager.instance.ShowUIAfterAttack();
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
                        GameManager.instance.ShowUIAfterAttack();
                        ChooseNextActiveCharacter();
                    });
                }
                else
                {
                    player2Character.Attack(player1Character, () =>
                    {
                        GameManager.instance.ShowUIAfterAttack();
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
    private CharacterBattle SpawnCharacter(bool isPlayerTeam)
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
        float effectiveBonus = GameConfig.baseCounterBonus;
        if (isElement)
        {
            effectiveBonus = GetEffectiveBonus(AttackMonster.GetMonsterType(), AttackedMonster.GetMonsterType());
        }
        int monsterDEF = AttackedMonster.GetMonsterDef();
        float attackDame = AttackMonster.GetMonsterATK() * effectiveBonus * (1 - (monsterDEF / (monsterDEF + 100f)));
        if (AttackMonster.GetMonsterCritRate() > UnityEngine.Random.Range(0, 100))
        {
            attackDame = attackDame * AttackedMonster.GetMonsterCritDame() / 100;
        }
        AttackedMonster.SetMonsterStats(Monster.MonsterStats.HP, AttackedMonster.GetMonsterHP() - attackDame);
        // GameManager.instance.UpdateBattleUI();
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
}
