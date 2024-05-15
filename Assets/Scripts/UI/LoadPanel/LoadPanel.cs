using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    public Animator animator;
    public LoadPanel go;
    public LoadPanelAnimation loadPanelAnimation;
    // public EventHandler OnAnimationEnd;
    // Update is called once per frame
    private void Start(){
        loadPanelAnimation.go = gameObject;
    }
    public LoadPanel ShowGachaPanel(string color){
        LoadPanel loadPanel = Instantiate(go);
        loadPanel.animator.SetTrigger(color);
        return loadPanel;
    }
    public void Hide(){
        // OnAnimationEnd?.Invoke(this,null);
        Destroy(gameObject);
    }
}
