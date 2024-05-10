using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    public Animator animator;
    public LoadPanel go;
    public LoadPanelAnimation loadPanelAnimation;
    // Update is called once per frame
    private void Start(){
        loadPanelAnimation.go = gameObject;
    }
    public void ShowGachaPanel(string color){
        LoadPanel loadPanel = Instantiate(go);
        loadPanel.animator.SetTrigger(color);
    }
    public void Hide(){
        Destroy(gameObject);
    }
}
