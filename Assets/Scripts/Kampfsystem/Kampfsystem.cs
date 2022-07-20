using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

enum BattleStates
{
    PLAYER,
    PLAYERRESOLVED,
    ENEMY,
    ENEMYRESOLVED,
    END
}
public class Player
{
    public int HP = 20;
}

public class Button
{
    public Button(GameObject _obj)
    {
        ButtonObject = _obj;
        ButtonField = _obj.GetComponent<UnityEngine.UI.Button>();
        Text = _obj.GetComponentInChildren<TextMeshProUGUI>();
    }

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
        else
        {

            if (Input.GetKey(KeyCode.Mouse0))
            {
                menu_controller.RoomScene.SetActive(true);
                SceneManager.UnloadSceneAsync("Kampf");
            }

        }
    }

    //TODO: Kämpfe sollen abgebrochen werden können und Gegner behalten ihre HP/Stress bei Wiederaufnahme
    //TODO: To-Do Liste für den Spieler erstellen
    public void InitBattle(AEnemyBaseClass newenemy)
    {
        //ruft bei Kampfstart das UI auf
        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            Buttons[i] = new Button(ButtonGameObjects[i]);
        }

        Enemy = newenemy;
        SetStep(Enemy.GetInitState);
        isActiveBattle = true;
    }

    /// <summary>
    /// Bereitet den Text und die State-Update auf einen neuen State vor
    /// </summary>
    /// <param name="newState">Der neue State, in dem sich der Gegner befindet</param>
    public void SetStep(ScriptableKampfstate newState)
    {
        currentState = newState;
        SetButtonText(currentState);
    }
    //TODO: Player HP und Gegner HP unabhängig voneinander im UI anzeigen
    private void UpdateUI()
    {
        //Zeigt die derzeitigen HP des Spielers und Gegners an
        UI.text = "Player HP: " + Player.HP + " " + "Enemy HP: " + Enemy.GetHP;
    }
    //public enum LeosState
    //{
        //despair,
        //cry,
        //happy,
        //sadsoup,
        //iloveyoubaby,
    //}
    //public void LeosSwitch(LeosState currentState)
    //{
        //if (currentState == LeosState.despair)
        //{
            ////lie down
        //}
        //else if (currentState == LeosState.cry)
        //{
            ////cry
        //}
        //else if (currentState == LeosState.happy)
        //{
            ////laugh
        //}
        //else if (currentState == LeosState.sadsoup)
        //{
            ////number E
        //}
        //else if (currentState == LeosState.iloveyoubaby)
        //{
            ////*intense head bopping*
        //}
    //}
    //TODO: Mit Dialog/Monologsystem verbinden, Namenskonvention ändern, (UI Update mit Event)
    /// <summary>
    /// Geht schrittweise die Zugreihenfolge ab: Spielerangriff > Ergebnistext > Gegnerangriff > Ergebnistext, repeat oder Kampfende
    /// </summary>
    public void Turn()
    {
        //
        UpdateUI();

        switch (CurrentBattleState)
        {
            case BattleStates.PLAYER:
                ResolvePlayer();
                break;
            case BattleStates.PLAYERRESOLVED:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CurrentBattleState = BattleStates.ENEMY;
                }
                break;
            case BattleStates.ENEMY:
                ResolveEnemy();
                break;
            case BattleStates.ENEMYRESOLVED:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    CurrentBattleState = BattleStates.PLAYER;
                break;
            case BattleStates.END:
                EndBattle();
                break;
        }
    }

    // Updated den Buttontext zum neuen Kampfstate
    private void SetButtonText(ScriptableKampfstate State)
    {
        for (int i = 0; i < ButtonGameObjects.Length; i++)
        {
            Buttons[i].Text.text = State.GetChoices[i].ChoiceName;
        }
    }
    /// <summary>
    /// Holt sich Ergebnis für Buttonklick
    /// </summary>
    /// <param name="choice"></param>
    public void OnButton(int choice)
    {
        if (choice <= 0)
            choice = 1;

        ResolvePlayerChoice(currentState.GetChoices[choice - 1]);
    }
    //TODO: Von Updateschleife entkoppeln
    /// <summary>
    /// Aktiviert Buttons und wartet auf Auswahl
    /// </summary>
    public void ResolvePlayer()
    {
        Dialogbox.text = "Bitte wähle eine Aktion";
        if (!Buttons[0].ButtonObject.activeInHierarchy)
            for (int i = 0; i < ButtonGameObjects.Length; i++)
            {
                Buttons[i].ButtonObject.SetActive(true);
            }
    }
    //TODO: Resolvemethoden zusammenführen
    //Fügt dem Spieler den Angriffswert des Gegners als Schaden zu, checkt ob Spieler HP auf 0 gefallen sind
    public void ResolveEnemy()
    {
        Dialogbox.text = Enemy.AttackDisplay;
        Player.HP += Enemy.Attack();

        if (Player.HP <= 0)
        {
            CurrentBattleState = BattleStates.END;
            return;
        }

        CurrentBattleState = BattleStates.ENEMYRESOLVED;
    }
    /// <summary>
    /// Ändert Gegner HP und Stress, checkt für Kampfende, updated Dialogfeld und Buttontext, deaktiviert Buttons währenddessen
    /// Markiert Spielerzug als beendet (Nächster State wartet auf Mausklick)
    /// </summary>
    /// <param name="choice"></param>
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
        CurrentBattleState = BattleStates.PLAYERRESOLVED;
    }
    //TODO: Sieg/Losescreen einbauen
    /// <summary>
    /// Entfernt Buttons, setzt ActiveBattle auf falsch und lässt aus der Turnorder springen
    /// </summary>
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
