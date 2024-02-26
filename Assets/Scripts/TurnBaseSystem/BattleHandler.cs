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
                });
                }
                else
                {
                    player2Character.GetComponent<Monster>().GetSkillBase().Activate(player1Character.gameObject, () =>
                {
                    GameManager.instance.ShowUIAfterAttack();
                    ChooseNextActiveCharacter();
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
        AttackedMonster.SetMonsterStats(Monster.MonsterStats.HP, AttackedMonster.GetMonsterHP() - AttackMonster.GetMonsterATK());
        GameManager.instance.UpdateBattleUI();
    }
    public void PlayVFX(Transform attackedMonster)
    {
        Instantiate(VFXEffect, attackedMonster.position + Vector3.up * 2 - 5 * Vector3.forward, Quaternion.identity);
    }
    public Monster GetMonster1() { return player1Character.GetComponent<Monster>(); }
    public Monster GetMonster2() { return player2Character.GetComponent<Monster>(); }
    public CharacterBattle GetCharacterBattle1() { return player1Character; }
    public CharacterBattle GetCharacterBattle2() { return player2Character; }
}
