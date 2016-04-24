using UnityEngine;
using System.Collections;

public class PersueEnemy : State {



    public override void Enter()
    {

        GameObject enemy = GameManager.GM.EnemyShip;

        foreach (GameObject ship in GameManager.GM.Fleet)
        {
            TargetManager TM = ship.GetComponent<TargetManager>();
            TM.SetTarget(enemy);
            TM.Mode(AgentState.persue);
        }

    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}
