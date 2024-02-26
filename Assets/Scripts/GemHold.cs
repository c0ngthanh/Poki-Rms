using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemHold : MonoBehaviour
{
    [SerializeField] private Text gemCount;
    [SerializeField] private GemSO gemSO;
    [SerializeField] private Image gemSprite;
    public void SetUp(GemSO gem)
    {
        gemSO = gem;
        gemSprite.sprite = gem.sprite;
        gemCount.text = "0";
    }
    public void SetText(int count)
    {
        gemCount.text = count.ToString();
    }
    public GemSO GetGemSO(){
        return gemSO;
    }
}
