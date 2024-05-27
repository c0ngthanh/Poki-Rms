using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class BattleReward
{
    public int exp;
    public int coin;
    public BattleReward(int exp, int coin) { 
        this.exp = exp;
        this.coin = coin;
    }
}
public class GameManager : MonoBehaviour
{
    public Monster monster1;
    public Monster monster2;
    public static Monster[] allMonsters;
    public static GameManager Instance;
    public AudioClip themeAudio;
    public AudioClip mainAudio;
    public AudioClip battleAudio;
    public AudioClip battlePrepareAudio;
    public AudioSource source;
    public BattleReward battleReward = null;
    [SerializeField] private BattleManager battleManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        allMonsters = Resources.LoadAll<Monster>("Prefab/Monster");
        foreach (Monster item in allMonsters)
        {
            item.SetupMonsterWithOutRegisterEvent();
        }
        PlaySound(themeAudio);
        // SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "StartScene")
        {
            PlaySound(themeAudio);
        }
        if (arg0.name == "2DTopDown")
        {
            PlaySound(mainAudio);
            if (battleReward != null && battleReward.exp != 0 && battleReward.coin != 0 )
            {
                PlayerController.instance.AddCoin(battleReward.coin);
                PlayerController.instance.activeMonster.LevelUp(battleReward.exp);
                MainUI.Instance.activeMonster.Show();
                battleReward = null;
            }
        }
        if (arg0.name == "BattleScene")
        {
            PlaySound(battleAudio);
        }
    }

    public void SetUpBattle(Monster monster1, Monster monster2)
    {
        this.monster1 = monster1;
        this.monster2 = monster2;
        SaveLoadSystem.Instance.Save(SaveLoadSystem.Instance.tempSaveFile);
        SceneManager.LoadScene("BattleScene");
    }
    public void SetBattleManager(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }
    public void PlaySound(AudioClip audio)
    {
        source.Stop();
        source.clip = audio;
        source.Play();
    }
    public void SetVolume(float value)
    {
        source.volume = value;
    }
}
