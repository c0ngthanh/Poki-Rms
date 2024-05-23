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
    public Image monsterImage;
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
            GameManager.Instance.battleReward = new BattleReward(1,200);
            lose.SetActive(false);
            win.SetActive(true);
            resultText.text = "Win";
            monsterImage.sprite = Resources.Load<Sprite>("MonsterUI/"+GameManager.Instance.monster1.GetMonsterSO().name);
            monsterImage.SetNativeSize();
            // Debug.Log(image.GetComponent<RectTransform>().rect.width);
            float scale = monsterImage.GetComponent<RectTransform>().rect.width/monsterImage.GetComponent<RectTransform>().rect.height;
            monsterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(120*scale,120);
        }
    }
    public void SetUp()
    {
        Debug.Log("Hehe");
    }
}
