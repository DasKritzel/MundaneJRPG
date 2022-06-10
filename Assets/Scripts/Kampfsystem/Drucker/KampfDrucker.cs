using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KampfDrucker : AEnemyBaseClass
{
    private void Start()
    {
        StartBattle();
    }

    [ContextMenu("BATTLE!")]
    public void StartBattle()
    {
        Kampfsystem.Instance.InitBattle(this);
    }
}
