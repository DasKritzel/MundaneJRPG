using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Kampfaufrufe sollen verschiedene Gegner nach Namen aufrufen
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
