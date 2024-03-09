using UnityEngine;
using System.Collections;
using DragonBones;
using System;

public class CharacterBase : MonoBehaviour
{
    public string monsterName = "AncientAutomaton";
    private UnityArmatureComponent armatureComponent;
    void Start()
    {
        UnityFactory.factory.LoadDragonBonesData("DragonBoneData/"+monsterName+"_ske"); // DragonBones file path (without suffix)
        UnityFactory.factory.LoadTextureAtlasData("DragonBoneData/"+monsterName+"_tex"); //Texture atlas file path (without suffix) 

        // Create armature.
        armatureComponent = UnityFactory.factory.BuildArmatureComponent(monsterName);
        // Input armature name

        // Play animation.
        armatureComponent.animation.Play("Idle");

        // Change armatureposition.
        armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        armatureComponent.transform.SetParent(gameObject.transform,false);
        armatureComponent.transform.localPosition = Vector2.zero;
        this.armatureComponent.AddDBEventListener(EventObject.COMPLETE, this.OnAnimationEventHandler);
    }
    public void AddNewCompleteAction(ListenerDelegate<EventObject> listener){
        this.armatureComponent.AddDBEventListener(EventObject.COMPLETE, listener);
    }
    public void PlayIdleAnimation(){
        armatureComponent.animation.Play("Idle");
    }
    public void PlayAttackAnimation(){
        armatureComponent.animation.Play("Attack");
    }
    public void PlayDamageAnimation(){
        armatureComponent.animation.Play("Damage");
    }
    public void PlaySkillAnimation(){
        armatureComponent.animation.Play("Skill");
    }

    private void OnAnimationEventHandler(string type, EventObject eventObject)
    {
        this.armatureComponent.animation.FadeIn("Idle", 0.2f);
    }

//     private void Update()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             //
//             this.ChangeAnimtion();
//         }
//     }

//     private void ChangeAnimtion()
//     {
//         var animationName = this.armatureComponent.animation.lastAnimationName;
//         if (!string.IsNullOrEmpty(animationName))
//         {
//             var animationNames = this.armatureComponent.animation.animationNames;
//             var animationIndex = (animationNames.IndexOf(animationName) + 1) % animationNames.Count;
//             this.armatureComponent.animation.Play(animationNames[animationIndex]);
//             // Debug.Log(animationNames[animationIndex]);
//             foreach (var item in animationNames)
//             {
//                 Debug.Log(item);
//             }
//         }
//         else
//         {
//             this.armatureComponent.animation.Play();
//         }

//         animationName = this.armatureComponent.animation.lastAnimationName;
//     }
}