using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyBaseClass : MonoBehaviour
{
    public bool BattleHasEnded => BattleEnded;
    public ScriptableKampfstate GetInitState => InitState;
    public int GetHP => HP;

    public string AttackDisplay => AttackText;

    [SerializeField]
    private ScriptableKampfstate InitState;

    [SerializeField]
    protected int HP;
    [SerializeField]
    protected int Stress;

    [SerializeField]
    protected int ATK;
    [SerializeField]
    protected string AttackText;

    [SerializeField]
    protected int HPEndCondition;
    [SerializeField]
    protected int StressEndCondition;

    protected bool BattleEnded;

    public void SetDamage(int dmg, int str)
    {
        HP += dmg;
        Stress += str;
        BattleEnded = EndCheck();
    }

    public virtual int Attack()
    {
        return ATK;
    }

    public virtual bool EndCheck()
    {
        return HP <= HPEndCondition  || Stress >= StressEndCondition;
    }

}
