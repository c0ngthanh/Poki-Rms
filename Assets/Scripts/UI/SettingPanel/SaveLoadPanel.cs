using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadPanel : MonoBehaviour
{
    public Button loadButton;
    public Button saveButton;
    public Button closeButton;
    // public Transform loadPrefab;
    // public Transform loadGrid;
    public TMP_InputField inputText;
    // Start is called before the first frame update
    private void Awake(){
        loadButton.onClick.AddListener(Load);
        saveButton.onClick.AddListener(Save);
        closeButton.onClick.AddListener(Hide);
    }

    private void Hide()
    {
        Destroy(gameObject);
    }

    private void Save()
    {
        fileSave[] fileSave = FileDataService.GetFileSaves();
        if(fileSave.Length > 5){
            NotificationText.ShowNotification("Only can save max 4 file.");
            return;
        }
        SaveLoadSystem.Instance.Save(inputText.text);
        NotificationText.ShowNotification("Save file successfully");
        Hide();
    }

    private void Load()
    {
        fileSave[] fileSave = FileDataService.GetFileSaves();
        foreach (fileSave item in fileSave)
        {
            if(item.name == SaveLoadSystem.Instance.tempSaveFile){
                continue;
            }
            if(item.name == inputText.text){
                SaveLoadSystem.Instance.LoadFile(item.name);
                SceneManager.LoadScene("2DTopDown");
                return;
            }
        }
        NotificationText.ShowNotification("Cant load file");
    }

    void Start()
    {
        LoadGameFrame();
    }
    private void LoadGameFrame()
    {
        fileSave[] fileSave = FileDataService.GetFileSaves();
        Transform loadPrefab = transform.Find("SaveFileList/Btn");
        Transform loadGrid = transform.Find("SaveFileList");
        foreach (fileSave item in fileSave)
        {
            if(item.name == SaveLoadSystem.Instance.tempSaveFile){
                continue;
            }
            GameObject tpl = Instantiate(loadPrefab.gameObject,loadGrid.transform);
            TMP_Text name = tpl.transform.Find("FileName").GetComponent<TMP_Text>();
            name.text = item.name;
            tpl.GetComponent<Button>().onClick.AddListener(() => {
                inputText.text = name.text;
            });
        }
        loadPrefab.gameObject.SetActive(false);
    }
}
