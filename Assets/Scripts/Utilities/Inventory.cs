using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    public double currentPage = 1;
    public double itemPerPage;
    // Hold all item
    public GameObject itemObject; // Item for Instantiate in Inventory
    public List<GameObject> itemObjectList; // Hold all ItemObject for showing or hiding page
    public double numberOfPages;
    public Inventory(double itemPerPage, GameObject itemObject)
    {
        this.itemPerPage = itemPerPage;
        this.itemObject = itemObject;
        this.itemObjectList = new List<GameObject>();
        this.numberOfPages = Math.Ceiling(itemObjectList.Count / this.itemPerPage);

    }
    public void ShowBookFirstTime()
    {
        if (currentPage == 1)
        {
            for (int i = 0; i < itemObjectList.Count; i++)
            {
                if (i >= 0 && i < itemPerPage)
                {
                    itemObjectList[i].SetActive(true);
                }
                else
                {
                    itemObjectList[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Current page is not 1");
        }
    }
    public void NextPage()
    {
        if (currentPage < numberOfPages)
        {
            for (double i = itemPerPage * (currentPage - 1); i < itemObjectList.Count && i < itemPerPage * currentPage; i++)
            {
                itemObjectList[(int)i].SetActive(false);
            }
            for (double i = itemPerPage * currentPage; i < itemObjectList.Count && i < itemPerPage * (currentPage + 1); i++)
            {
                itemObjectList[(int)i].SetActive(true);
            }
            currentPage++;
        }
        else
        {
            return;
        }
    }
    public void Hide()
    {
        for (int i = 0; i < itemObjectList.Count; i++)
        {
            itemObjectList[i].SetActive(false);
        }
    }
    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            for (double i = Math.Min(itemPerPage * currentPage - 1, itemObjectList.Count - 1); i >= 0 && i >= itemPerPage * (currentPage - 1); i--)
            {
                itemObjectList[(int)i].SetActive(false);
            }
            for (double i = itemPerPage * (currentPage - 1) - 1; i >= 0 && i >= itemPerPage * (currentPage - 2); i--)
            {
                itemObjectList[(int)i].SetActive(true);
            }
            currentPage--;
        }
        else
        {
            return;
        }
    }
    public void Add(MonsterBookItem item)
    {
        itemObjectList.Add(item.gameObject);
        numberOfPages = Math.Ceiling(itemObjectList.Count / this.itemPerPage);
        item.gameObject.SetActive(false);
    }
}
