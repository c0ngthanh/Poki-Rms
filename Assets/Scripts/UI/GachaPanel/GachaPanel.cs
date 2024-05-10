using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaPanel : MonoBehaviour
{
    public Button draw1Button;
    private LoadPanel loadPanel;
    // Start is called before the first frame update
    private void Awake(){
        loadPanel = Resources.Load<LoadPanel>("Prefab/UI/LoadPanel");
    }
    void Start()
    {
        draw1Button.onClick.AddListener(Draw1Ticket);
    }

    private void Draw1Ticket()
    {
        loadPanel.ShowGachaPanel("Red");
    }

}
