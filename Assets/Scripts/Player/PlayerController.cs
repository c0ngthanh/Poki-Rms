using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private List<Monster> monstersList;
    public Monster activeMonster = null;
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool isMoving;
    [SerializeField] private Vector2 input;
    private Animator animationController;
    public int coin;
    public int ticket;
    // Start is called before the first frame update
    void Awake(){
        instance = this;
        monstersList = new List<Monster>();
    }

    void Start()
    {
        animationController = GetComponent<Animator>();
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal") * playerSpeed;
        input.y = Input.GetAxisRaw("Vertical") * playerSpeed;
        if (input.x != 0)
        {
            input.y = 0;
        }
        if (input.y != 0)
        {
            input.x = 0;
        }
        if (input == Vector2.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
            animationController.SetFloat("inputX", input.x);
            animationController.SetFloat("inputY", input.y);
        }

        animationController.SetBool("isMoving", isMoving);
        GetComponent<Rigidbody2D>().velocity = input;
        if(Input.GetKeyDown(KeyCode.Z)){
            SaveLoadSystem.Instance.Save("Game");
        }
        if(Input.GetKeyDown(KeyCode.X)){
            SceneManager.LoadScene("2DTopDown");
            SaveLoadSystem.Instance.LoadFile("Game");
        }
        // if(Input.GetMouseButtonDown(0)){
        //     LoadPanel loadPanel = Resources.Load<LoadPanel>("Prefab/UI/LoadPanel");
        //     loadPanel.ShowGachaPanel("Red");
        // }
    }
    public List<Monster> GetMonstersList(){
        return monstersList;
    }
    public int GetCoin(){
        return coin;
    }
    public int GetTicket(){
        return ticket;
    }
    public void SetCoin(int value){
        coin = value;
        MainUI.Instance.coin.SetCoinUI();
    }
    public void AddCoin(int value){
        SetCoin(coin+value);
    }
    public void SetTicket(int value){
        ticket = value;
    }
    public void SetActiveMonster(Monster monster){
        activeMonster = monster;
        MainUI.Instance.activeMonster.Show();
    }
}
