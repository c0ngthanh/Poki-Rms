using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private List<Monster> monstersList;
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool isMoving;
    [SerializeField] private Vector2 input;
    private Animator animationController;
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
            SceneManager.LoadScene("BattleScene");
        }
        // if(Input.GetMouseButtonDown(0)){
        //     LoadPanel loadPanel = Resources.Load<LoadPanel>("Prefab/UI/LoadPanel");
        //     loadPanel.ShowGachaPanel("Red");
        // }
    }
    public List<Monster> GetMonstersList(){
        return monstersList;
    }
}
