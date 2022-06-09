using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

enum BattleStates
{
    PLAYER,
    PLAYERRESOLVE,
    ENEMY,
    ENEMYRESOLVE,
    END
}
public class Player
{
    public int HP = 20;
}

public class Button
{
    public GameObject ButtonObject;
    public TextMeshProUGUI Text;
    public UnityEngine.UI.Button ButtonField;
}

public class Kampfsystem : MonoBehaviour
{
    public static Kampfsystem Instance;

    [SerializeField]
    private GameObject[] ButtonGameObjects = new GameObject[3];

    private Button[] Buttons = new Button[3];

    [SerializeField]
    private TextMeshProUGUI Dialogbox;

    [SerializeField]
    private Player Player = new Player();
    private AEnemyBaseClass Enemy;

    private ScriptableKampfstate currentState;

    [SerializeField]
    private TextMeshProUGUI UI;

    private bool isActiveBattle;

    private BattleStates CurrentBattleState;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isActiveBattle)
            Turn();
    }

    public void InitBattle(AEnemyBaseClass newenemy)
    {
        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            Buttons[i] = new Button();
            Buttons[i].ButtonObject = ButtonGameObjects[i];
            Buttons[i].ButtonField = ButtonGameObjects[i].GetComponent<UnityEngine.UI.Button>();
            Buttons[i].Text = ButtonGameObjects[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        Enemy = newenemy;
        SetStep(Enemy.GetInitState);
        isActiveBattle = true;
    }
    public void SetStep(ScriptableKampfstate newState)
    {
        currentState = newState;
        SetButtonText(currentState);
    }

    private void UpdateUI()
    {
        UI.text = "Player HP: " + Player.HP + " " + "Enemy HP: " + Enemy.GetHP;
    }

    public void Turn()
    {

        UpdateUI();

        switch (CurrentBattleState)
        {
            case BattleStates.PLAYER:
                ResolvePlayer();
                break;
            case BattleStates.PLAYERRESOLVE:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CurrentBattleState = BattleStates.ENEMY;
                }
                break;
            case BattleStates.ENEMY:
                ResolveEnemy();
                break;
            case BattleStates.ENEMYRESOLVE:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    CurrentBattleState = BattleStates.PLAYER;
                break;
            case BattleStates.END:
                EndBattle();
                break;
        }
    }
    private void SetButtonText(ScriptableKampfstate State)
    {
        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            Buttons[i].Text.text = State.GetChoices[i].ChoiceName;
        }
    }

    public void OnButton1()
    {
        ResolvePlayerChoice(currentState.GetChoices[0]);
    }

    public void OnButton2()
    {
        ResolvePlayerChoice(currentState.GetChoices[1]);
    }
    public void OnButton3()
    {
        ResolvePlayerChoice(currentState.GetChoices[2]);
    }

    public void ResolvePlayer()
    {
        Dialogbox.text = "Bitte wähle eine Aktion";
        if (!Buttons[0].ButtonObject.activeInHierarchy)
            for (int i = 0; i < ButtonGameObjects.Length; i++)
            {
                Buttons[i].ButtonObject.SetActive(true);
            }
    }
    public void ResolveEnemy()
    {
        Dialogbox.text = Enemy.AttackDisplay;
        Player.HP += Enemy.Attack();

        if (Player.HP <= 0)
        {
            CurrentBattleState = BattleStates.END;
            return;
        }

        CurrentBattleState = BattleStates.ENEMYRESOLVE;
    }
    private void ResolvePlayerChoice(KampfstateListItem choice)
    {
        Enemy.SetDamage(choice.HP, choice.Stress);
        if (Enemy.EndCheck())
        {
            CurrentBattleState = BattleStates.END;
            return;
        }

        Dialogbox.text = choice.ResultText;
        SetStep(choice.NewState);
        if (Buttons[0].ButtonObject.activeInHierarchy)
            for (int i = 0; i < ButtonGameObjects.Length; i++)
            {
                Buttons[i].ButtonObject.SetActive(false);
            }
        CurrentBattleState = BattleStates.PLAYERRESOLVE;
    }
    private void EndBattle()
    {
        Dialogbox.text = "ENDE GELÄNDE";
        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            Buttons[i].ButtonObject.SetActive(false);
        }

        isActiveBattle = false;

    }
}
