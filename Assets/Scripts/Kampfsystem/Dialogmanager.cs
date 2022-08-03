using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogmanager : MonoBehaviour
{
    public static Dialogmanager Instance { get; private set; }

    [SerializeField]
    private GameObject block;
    [SerializeField]
    private TextMeshProUGUI dialogTextbox;

    private ScriptableDialoguebox dialog;

    private void Awake()
    {
        if(Instance is null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void Update()
    {
        
    }

    public void OnNewText(ScriptableDialoguebox newText)
    {
        if (!block.activeInHierarchy)
            block.SetActive(true);

        //StopCoroutine(Letter());
        dialog = newText;
        StartCoroutine(Letter());
    }

    IEnumerator Letter()
    {
        string[] tmp = dialog.GetLines;
        int line = 0;

        dialogTextbox.text = $"{tmp[0]}\n{tmp[1]}\n{tmp[2]}\n{tmp[3]}";

        return null;
    }
}
