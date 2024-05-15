using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool playerisClosed;
    private DialoguePanel dialoguePanel = null;
    public Sprite npcAvatar;
    public string[] dialogue;
    
    private void Update(){
        if(Input.GetKeyDown(KeyCode.E) && playerisClosed){
            if(dialoguePanel == null){
                dialoguePanel = MainUI.Instance.OpenUI("DialogPanel").GetComponent<DialoguePanel>();  
                dialoguePanel.SetUp(dialogue, npcAvatar,gameObject.name);
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
                Destroy(dialoguePanel.gameObject);
                dialoguePanel=null;
                print(dialoguePanel);
            }
            playerisClosed = false; 
        } 
    } 
}
