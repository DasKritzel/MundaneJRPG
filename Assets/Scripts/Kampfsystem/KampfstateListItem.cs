using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//TODO: ExhaustedSprite den Character Sprite ändern lassen
public class KampfstateListItem
{
    public string ChoiceName;
    public string ResultText;
    public int HP;
    public int Stress;
    public int Cost;
    public bool ExhaustedSprite;
    public ScriptableKampfstate NewState;
}
