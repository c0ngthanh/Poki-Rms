using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterData : MonoBehaviour
{
    public Monster[] allMonsters;
    // Start is called before the first frame update
    void Start()
    {
        allMonsters = Resources.LoadAll<Monster>("Prefab/Monster");
    }
}
