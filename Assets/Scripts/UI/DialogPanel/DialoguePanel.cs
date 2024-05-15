using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public Text dialogueText;
    public string[] dialogue;
    public int index = 0;
    public float wordSpeed = 0.1f;
    public void ZeroText(){
        dialogueText.text = "";
        index = 0;
    }
    public void NextLine(){
        if(index < dialogue.Length-1){
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }else{
            ZeroText();
        }
    }
    IEnumerator Typing(){
        foreach (char letter in dialogue[0].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
}
