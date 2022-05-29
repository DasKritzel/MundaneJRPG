using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Player
{
    int HP = 20;
}

public class Kampfsystem : MonoBehaviour
{
    public static Kampfsystem Instance;

    [SerializeField]
    private GameObject Button1;
    [SerializeField]
    private GameObject Button2;
    [SerializeField]
    private GameObject Button3;

    [SerializeField]
    private TextMeshProUGUI Dialogbox;

    private TextMeshProUGUI B1Text, B2Text, B3Text;
    private Button B1, B2, B3;

    [SerializeField]
    private Player Player;
    private AEnemyBaseClass Enemy;

    private ScriptableKampfstate currentState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        B1Text = Button1.GetComponentInChildren<TextMeshProUGUI>();
        B2Text = Button2.GetComponentInChildren<TextMeshProUGUI>();
        B3Text = Button3.GetComponentInChildren<TextMeshProUGUI>();
        B1 = Button1.GetComponent<Button>();
        B2 = Button2.GetComponent<Button>();
        B3 = Button3.GetComponent<Button>();
    }

    public void InitBattle(AEnemyBaseClass newenemy)
    {
        Enemy = newenemy;
        SetStep(Enemy.GetInitState);
    }

    public void SetStep(ScriptableKampfstate newState)
    {
        currentState = newState;
        SetButtonText(currentState);
    }

    private void SetButtonText(ScriptableKampfstate State)
    {
        B1Text.text = State.GetChoices[0].ChoiceName;
        B2Text.text = State.GetChoices[1].ChoiceName;
        B3Text.text = State.GetChoices[2].ChoiceName;
    }

    public void OnButton1()
    {
        ResolveState(currentState.GetChoices[0]);
    }

    public void OnButton2()
    {
        ResolveState(currentState.GetChoices[1]);
    }
    public void OnButton3()
    {
        ResolveState(currentState.GetChoices[2]);
    }
    private void ResolveState(KampfstateListItem choice)
    {
        Enemy.SetDamage(choice.HP, choice.Stress);
        Dialogbox.text = choice.ResultText;
        if (Enemy.EndCheck())
        {
            EndBattle();
            return;
        }

        SetStep(choice.NewState);
    }
    private void EndBattle()
    {
        Dialogbox.text = "ENDE GELÄNDE";
        Button1.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);

    }
}
