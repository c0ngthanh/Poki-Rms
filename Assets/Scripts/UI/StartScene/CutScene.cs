using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("2DTopDown");
        SaveLoadSystem.Instance.LoadNewGame(); 
    }
}
