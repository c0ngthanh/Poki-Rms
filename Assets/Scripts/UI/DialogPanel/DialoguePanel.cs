using System;
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
    public Button acceptBtn;
    public Button cancelBtn;
    public NPC npc;
    private string[] dialogue;
    private int index = 0;
    private float wordSpeed = 0.01f;
    public void SetUp(string[] dialogue, Sprite avatar, string name, NPC npc)
    {
        cancelBtn.onClick.AddListener(Hide);
        acceptBtn.onClick.AddListener(() => {
            npc.GetComponent<NPCBehavior>().Action();
        });
        acceptBtn.gameObject.SetActive(false);
        cancelBtn.gameObject.SetActive(false);
        this.dialogue = dialogue;
        npcAvatar.sprite = avatar;
        npcName.text = name;
        this.npc = npc;
        ZeroText();
        StartCoroutine(Typing());
    }
    public void Hide()
    {
        npc.dialoguePanel = null;
        Destroy(gameObject);
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
        }else{
            if(npc.GetComponent<NPCBehavior>().npcType != NPCType.Normal){
                acceptBtn.gameObject.SetActive(true);
            }
            cancelBtn.gameObject.SetActive(true);
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
