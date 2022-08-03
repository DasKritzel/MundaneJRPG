using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "00_Dialog", menuName = "Dialog", order = 0)]

public class ScriptableDialoguebox : ScriptableObject
{
    public string[] GetLines => new string[]{ line1, line2, line3, line4 };

    [SerializeField]
    private string line1;
    [SerializeField]
    private string line2;
    [SerializeField]
    private string line3;
    [SerializeField]
    private string line4;


    [ContextMenu("Go!")]
    public void UpdateText()
    {
        Dialogmanager.Instance.OnNewText(this);
    }

}