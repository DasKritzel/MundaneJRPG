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
    public int SolveValueChange;
    public int PlayerStressChange;
    public int EnergyCost;
    public bool ExhaustedSprite;
    public ScriptableKampfstate NewState;
}
