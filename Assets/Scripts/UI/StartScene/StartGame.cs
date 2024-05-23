using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject startFrame;
    public GameObject LoadFrame;
    public GameObject CreditFrame;
    public Button startGame;
    public Button loadGame;
    public Button creditGame;
    public Button backBtn1;
    public Button backBtn2;
    private void Awake(){
        startGame.onClick.AddListener(StartNewGame);
        loadGame.onClick.AddListener(LoadGameFrame);
        creditGame.onClick.AddListener(OpenCredit);
        backBtn1.onClick.AddListener(ShowGameFrame);
        backBtn2.onClick.AddListener(ShowGameFrame);

    }
    void Start()
    {
        ShowGameFrame();
    }
    private void ShowGameFrame()
    {
        startFrame.SetActive(true);
        LoadFrame.SetActive(false);
        CreditFrame.SetActive(false);
    }

    private void OpenCredit()
    {
        startFrame.SetActive(false);
        LoadFrame.SetActive(false);
        CreditFrame.SetActive(true);
    }

    private void LoadGameFrame()
    {
        startFrame.SetActive(false);
        LoadFrame.SetActive(true);
        CreditFrame.SetActive(false);
        Transform loadPrefab = LoadFrame.transform.Find("LoadGameGrid/Load");
        Transform loadGrid = LoadFrame.transform.Find("LoadGameGrid");
        Debug.Log(loadPrefab);
        fileSave[] fileSave = FileDataService.GetFileSaves();
        foreach (fileSave item in fileSave)
        {
            if(item.name == SaveLoadSystem.Instance.tempSaveFile){
                continue;
            }
            GameObject tpl = Instantiate(loadPrefab.gameObject,loadGrid.transform);
            Image character = tpl.transform.Find("Character").GetComponent<Image>();
            Text name = tpl.transform.Find("Name").GetComponent<Text>();
            Text date = tpl.transform.Find("Date").GetComponent<Text>();
            name.text = item.name;
            date.text = item.date.ToString();
            tpl.GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("2DTopDown");
                SaveLoadSystem.Instance.LoadFile(item.name); 
            });
        }
        loadPrefab.gameObject.SetActive(false);
    }

    private void StartNewGame()
    {
        SceneManager.LoadScene("2DTopDown");
        SaveLoadSystem.Instance.LoadNewGame(); 
    }

    
}
