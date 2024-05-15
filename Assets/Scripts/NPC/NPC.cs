using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool playerisClosed;
    private DialoguePanel dialoguePanel;
    
    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            if(dialoguePanel.gameObject.activeInHierarchy){
                
            }else{

            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.GetComponent<PlayerController>()) 
        { 
            playerisClosed = true; 
        } 
    } 
    void OnTriggerExit2D(Collider2D collision) 
    { 
        if (collision.gameObject.GetComponent<PlayerController>()) 
        { 
            playerisClosed = false; 
        } 
    } 
}
