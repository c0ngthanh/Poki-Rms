using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] int player1HealthMax;
    [SerializeField] int player1EnergyMax;
    [SerializeField] Text player1HealthValueText;
    [SerializeField] Slider player1HealthSlider;
    [SerializeField] Text player1EnergyValueText;
    [SerializeField] Slider player1EnergySlider;
    [SerializeField] int player2HealthMax;
    [SerializeField] int player2EnergyMax;
    [SerializeField] Text player2HealthValueText;
    [SerializeField] Slider player2HealthSlider;
    [SerializeField] Text player2EnergyValueText;
    [SerializeField] Slider player2EnergySlider;
    public void BattleUIUpdate(){
        GameManager.instance.battleHandler.onBattleComplete += UpdateUI;
    }

    private void UpdateUI(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void SetUp(int maxP1HP,int maxP1Mana,int maxP2HP,int maxP2Mana){
        player1HealthValueText.text = maxP1HP + "/" + maxP1HP;
        player2HealthValueText.text = maxP2HP + "/" + maxP2HP;
        player1EnergyValueText.text = 0 + "/" + maxP1Mana;
        player2EnergyValueText.text = 0 + "/" + maxP2Mana;
        player1HealthSlider.value = 1;
        player2HealthSlider.value = 1;
        player1EnergySlider.value = 0;
        player2EnergySlider.value = 0;
        player1HealthMax = maxP1HP;
        player1EnergyMax = maxP1Mana;
        player2HealthMax = maxP2HP;
        player2EnergyMax = maxP2Mana;
    }
    public void UpdateUI(int P1HP,int P1Mana,int P2HP,int P2Mana){
        player1HealthValueText.text = P1HP + "/" + player1HealthMax;
        player2HealthValueText.text = P2HP + "/" + player2HealthMax;
        player1EnergyValueText.text = P1Mana + "/" + player1EnergyMax;
        player2EnergyValueText.text = P2Mana + "/" + player2EnergyMax;
        player1HealthSlider.value = (float)P1HP/player1HealthMax;
        player2HealthSlider.value = (float)P2HP/player2HealthMax;
        player1EnergySlider.value = (float)P1Mana/player1EnergyMax;
        player2EnergySlider.value = (float)P2Mana/player2EnergyMax;
    }
}
