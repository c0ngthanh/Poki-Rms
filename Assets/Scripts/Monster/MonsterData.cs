using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterData : MonoBehaviour
{
    public static Monster[] allMonsters;
    // Start is called before the first frame update
    void Awake()
    {
        allMonsters = Resources.LoadAll<Monster>("Prefab/Monster");
        foreach (Monster item in allMonsters)
        {
            item.SetupMonsterWithOutRegisterEvent();
        }
    }
}
