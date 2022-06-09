using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "00_Kampfstate", menuName ="Kampfstate", order = 0)]
public class ScriptableKampfstate : ScriptableObject
{
    public List<KampfstateListItem> GetChoices => Choices;

    [SerializeField]
    private List<KampfstateListItem> Choices;
}
