using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hinzufügen von Modifiern und Texten zu Gegnern
public abstract class AEnemyBaseClass : MonoBehaviour
{
    public bool BattleHasEnded => BattleEnded;
    public ScriptableKampfstate GetInitState => InitState;
    public int GetHP => HP;
    public int MaxHP => maxHP;

    public string AttackDisplay => AttackText;

    [SerializeField]
    private ScriptableKampfstate InitState;

    [SerializeField]
    protected int HP;
    [SerializeField]
    protected int SolveValue; //Versteckter Wert der Fortschritt zur Problemlösung trackt

    [SerializeField]
    protected int ATK;
    [SerializeField]
    protected string AttackText;

    [SerializeField]
    protected int HPEndCondition;
    [SerializeField]
    protected int SolveEndCondition;

    protected bool BattleEnded;

    protected int maxHP;

    private void Awake()
    {
        maxHP = HP;
    }

    //TODO: EndCheck überarbeiten
    public void SetDamage(int dmg, int str)
    {
        HP += dmg;
        SolveValue += str;
        BattleEnded = EndCheck();
    }

    public virtual int Attack()
    {
        return ATK;
    }

    //TODO: HP, Solve und Stress EndConditions führen zu verschiedenen Win/Lose Screens

    public virtual bool EndCheck()
    {
        return HP <= HPEndCondition  || SolveValue >= SolveEndCondition;
    }

}
