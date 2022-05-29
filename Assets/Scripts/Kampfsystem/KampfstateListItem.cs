using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KampfstateListItem
{
    public string ChoiceName;
    public string ResultText;
    public int HP;
    public int Stress;
    public ScriptableKampfstate NewState;
}
