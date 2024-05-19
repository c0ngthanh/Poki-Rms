using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState{
    Lose,
    Win,
    Ingame
}
public class BattleResultPanel : MonoBehaviour
{
    public Text resultText;
    public GameObject lose;
    public GameObject win;
    public Button backButton;
    // Start is called before the first frame update
    private void Awake(){
        backButton.onClick.AddListener(BackToMainScene);
    }
    private void Start(){
        gameObject.SetActive(false);
    }
    private void BackToMainScene()
    {
        SaveLoadSystem.Instance.LoadFile(SaveLoadSystem.Instance.tempSaveFile);
        SceneManager.LoadScene("2DTopDown");
    }
    public void SetResult(BattleState result)
    {
        gameObject.SetActive(true);
        if(result == BattleState.Lose){
            lose.SetActive(true);
            win.SetActive(false);
            resultText.text = "Lose";
        }else{
            lose.SetActive(false);
            win.SetActive(true);
            resultText.text = "Win";
        }
    }
    public void SetUp()
    {
        Debug.Log("Hehe");
    }
}
