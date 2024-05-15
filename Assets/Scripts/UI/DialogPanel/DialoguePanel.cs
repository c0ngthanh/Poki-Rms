using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public Text dialogueText;
    public Text npcName;
    public Image npcAvatar;
    private string[] dialogue;
    private int index = 0;
    private float wordSpeed = 0.01f;
    public void SetUp(string[] dialogue, Sprite avatar, string name)
    {
        this.dialogue = dialogue;
        npcAvatar.sprite = avatar;
        npcName.text = name;
        ZeroText();
        StartCoroutine(Typing());
    }
    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
    }
    public void NextLine()
    {
        if (dialogueText.text.Length < dialogue[index].Length)
        {
            wordSpeed = 0.005f;
        }
        else if (index < dialogue.Length - 1)
        {
            wordSpeed = 0.01f;
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
    }
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
}
