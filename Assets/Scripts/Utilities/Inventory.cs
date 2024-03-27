using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    public double currentPage=1;
    public double itemPerPage;
    // Hold all item
    public GameObject itemObject; // Item for Instantiate in Inventory
    public List<GameObject> itemObjectList; // Hold all ItemObject for showing or hiding page
    public double numberOfPages;
    public Inventory(double itemPerPage, GameObject itemObject){
        this.itemPerPage = itemPerPage;
        this.itemObject = itemObject;
        this.itemObjectList = new List<GameObject>();
        this.numberOfPages = Math.Ceiling(itemObjectList.Count/this.itemPerPage);
    }
    public void ShowBookFirstTime(){
        if(currentPage ==1){
            for(int i=0;i<itemObjectList.Count;i++){
                if(i>=0 && i<itemPerPage){
                    itemObjectList[i].SetActive(true);
                }else{
                    itemObjectList[i].SetActive(false);
                }
            }
        }else{
            Debug.LogError("Current page is not 1");
        }
    }
    public void NextPage(){
        if(currentPage <= this.numberOfPages){
            for(int i=0;i<itemObjectList.Count;i++){

            }
        }
    }
}
