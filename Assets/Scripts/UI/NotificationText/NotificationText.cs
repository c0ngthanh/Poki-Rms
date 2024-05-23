using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationText : MonoBehaviour
{
    public static void ShowNotification(string value){
        NotificationText obj = Instantiate(Resources.Load<NotificationText>("Prefab/UI/NotificationText"),MainUI.Instance.UIRoot.transform);
        obj.GetComponent<TMP_Text>().text = value;
        
    }
    public void Destroy(){
        Destroy(gameObject);
    }
}
