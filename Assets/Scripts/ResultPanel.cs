using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private List<GemHold> gemHolds = new List<GemHold>();
    [SerializeField] private GemHold gemHoldPrefab;
    public void SetUp(LevelSO levelSO)
    {
        foreach (GemSO gemSO in levelSO.gemList)
        {
            GemHold tempGemHold = Instantiate(gemHoldPrefab, transform);
            gemHolds.Add(tempGemHold);
            tempGemHold.SetUp(gemSO);
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2((levelSO.gemList.Count + 1) * gemHoldPrefab.GetComponent<RectTransform>().sizeDelta.x, 130);
        Hide();
    }
    public void UpdateResult(Dictionary<GemSO, int> totalGemClear)
    {
        foreach (KeyValuePair<GemSO, int> item in totalGemClear)
        {
            foreach (GemHold gemHold in gemHolds)
            {
                if (gemHold.GetGemSO() == item.Key)
                {
                    gemHold.SetText(item.Value);
                }
            }
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
