using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{
    public TMP_Text value;
    public Image critDame;
    public Vector3 offset = new Vector3(0,1,0);
    
    
    private void Start(){
        Destroy(gameObject, 2);
    }
    public void ShowDamage(string damageValue, Monster.MonsterType type,bool isCrit, bool isElement){
        transform.position += offset;
        Color color = Color.white;
        value.text = damageValue;
        critDame.gameObject.SetActive(isCrit);
        if(!isCrit){
            value.fontSize *=0.6f;
        }
        if(isElement){
            switch (type)
            {
                case Monster.MonsterType.Fire: color = Color.red; break;
                case Monster.MonsterType.Water: color = Color.blue; break;
                case Monster.MonsterType.Grass: color = Color.green; break;
                case Monster.MonsterType.Electric: color = new Color(1,0.52f,0,1); break;
                case Monster.MonsterType.Dark: color = Color.black; break;
                case Monster.MonsterType.Light: color = Color.yellow; break;
            }
            value.color = color;
            critDame.color = color;
        }
        gameObject.SetActive(true);
    }
}
