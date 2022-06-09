using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KampfDrucker : AEnemyBaseClass
{

    [ContextMenu("BATTLE!")]
    public void StartBattle()
    {
        Kampfsystem.Instance.InitBattle(this);
    }
}
