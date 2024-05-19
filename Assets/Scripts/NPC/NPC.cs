using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool playerisClosed;
    public DialoguePanel dialoguePanel = null;
    public Sprite npcAvatar;
    public string[] dialogue;
    
    private void Update(){
        if(Input.GetKeyDown(KeyCode.E) && playerisClosed){
            if(dialoguePanel == null){
                dialoguePanel = MainUI.Instance.OpenUI("DialogPanel").GetComponent<DialoguePanel>();  
                dialoguePanel.SetUp(dialogue, npcAvatar,gameObject.name,this);
            }else{
                dialoguePanel.NextLine();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.CompareTag("Player")) 
        { 
            playerisClosed = true; 
        } 
    } 
    private void OnTriggerExit2D(Collider2D collision) 
    { 
        if (collision.CompareTag("Player")) 
        { 
            if(dialoguePanel!=null){
                dialoguePanel.Hide();
            }
            playerisClosed = false; 
        } 
    } 
}
