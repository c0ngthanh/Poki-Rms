using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public string Name;
    public int numOfMonsters;
}
// Start is called before the first frame update
public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] public GameData gameData;
    IDataService dataService;
    private void Awake(){
        dataService = new FileDataService(new JsonSerializer());
    }
    public void Save(){
        dataService.Save(gameData);
    }
    public void Load(string gameName){
        Debug.Log("Loading " + gameName);
    }
}
