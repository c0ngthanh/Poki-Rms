using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public Button saveLoadButton;
    public Button exitButton;
    public Button closeButton;
    public Slider slider;
    // Start is called before the first frame update
    private void Awake(){
        saveLoadButton.onClick.AddListener(SaveLoad);
        exitButton.onClick.AddListener(Exit);
        closeButton.onClick.AddListener(Destroy);
        slider.onValueChanged.AddListener((value)=>{
            GameManager.Instance.SetVolume(value);
        });
    }
    private void Start(){
        slider.value = GameManager.Instance.source.volume;
    }
    private void Exit()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void SaveLoad()
    {
        MainUI.Instance.OpenUI("SaveLoadPanel");
    }
}
