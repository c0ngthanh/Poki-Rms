using System;
using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private CharacterBase characterBase;
    private State state;
    private Vector3 slideTargetPosition;
    private Action onSlideComplete;

    private enum State
    {
        Idle,
        Sliding,
        Busy,
    }


    private void Awake()
    {
        characterBase = GetComponent<CharacterBase>();
        state = State.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Sliding:
                float slideSpeed = 10f;
                transform.position += (slideTargetPosition - GetPosition()) * slideSpeed * Time.deltaTime;

                float reachedDistance = 1f;
                if (Vector3.Distance(GetPosition(), slideTargetPosition) < reachedDistance)
                {
                    transform.position = slideTargetPosition;
                    onSlideComplete();
                }
                break;
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void Attack(CharacterBattle characterBattle, Action onAttackComplete)
    {
        Vector3 slideTagretPosition = characterBattle.GetPosition() + (GetPosition() - characterBattle.GetPosition()).normalized * 3f - Vector3.forward;
        Vector3 startingPosition = GetPosition();
        //Slide to target position
        SlideToPosition(slideTagretPosition, () =>
        {
            state = State.Busy;
            //Arrived to target position, attack
            characterBase.PlayAttackAnimation();
            characterBase.AddNewCompleteAction((string type, EventObject eventObject) =>
            {
                if (eventObject.animationState.name == "Attack")
                {
                    characterBattle.GetComponent<CharacterBase>().PlayDamageAnimation();
                    GameManager.instance.battleHandler.PlayVFX(characterBattle.transform);
                    GameManager.instance.battleHandler.BattleDamage(transform.GetComponent<Monster>(),characterBattle.GetComponent<Monster>());
                    // Slide back to start position
                    SlideToPosition(startingPosition, () =>
                    {
                        state = State.Idle;
                        // Attack completed
                        onAttackComplete();
                    });
                }
            });
        });
    }
    public void Skill(CharacterBattle characterBattle, Action onAttackComplete)
    {
        Vector3 slideTagretPosition = characterBattle.GetPosition() + (GetPosition() - characterBattle.GetPosition()).normalized * 3f - Vector3.forward;
        Vector3 startingPosition = GetPosition();
        //Slide to target position
        SlideToPosition(slideTagretPosition, () =>
        {
            state = State.Busy;
            //Arrived to target position, attack
            characterBase.PlaySkillAnimation();
            characterBase.AddNewCompleteAction((string type, EventObject eventObject) =>
            {
                if (eventObject.animationState.name == "Skill")
                {
                    characterBattle.GetComponent<CharacterBase>().PlayDamageAnimation();
                    GameManager.instance.battleHandler.PlayVFX(characterBattle.transform);
                    GameManager.instance.battleHandler.BattleDamage(transform.GetComponent<Monster>(),characterBattle.GetComponent<Monster>());
                    // Slide back to start position
                    SlideToPosition(startingPosition, () =>
                    {
                        state = State.Idle;
                        // Attack completed
                        onAttackComplete();
                    });
                }
            });
        });
    }
    private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete)
    {
        this.slideTargetPosition = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
    }
}
