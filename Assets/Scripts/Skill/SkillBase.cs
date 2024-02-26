using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public new string name;
    public Sprite icon;
    public virtual void Activate(GameObject obj, Action onSkillComplete){}
}
