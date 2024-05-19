using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanelAnimation : MonoBehaviour
{
    public GameObject go;
    public void Hide(){
        Destroy(go);
    }
}
