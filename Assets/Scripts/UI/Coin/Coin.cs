using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public TMP_Text coinValue;
    private void Start(){
        SetCoinUI();
    }
    public void SetCoinUI(){
        coinValue.text = PlayerController.instance.coin.ToString();
    }
}
