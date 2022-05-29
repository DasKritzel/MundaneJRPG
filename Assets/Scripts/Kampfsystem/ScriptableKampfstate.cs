using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "0X_Kampfstate", menuName ="Kampfstate", order = 0)]
public class ScriptableKampfstate : ScriptableObject
{
    [SerializeField]
    private List<KampfstateListItem> Choices;
}
