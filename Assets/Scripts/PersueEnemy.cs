using UnityEngine;
using System.Collections;

public class PersueEnemy : State {



    public override void Enter()
    {

        SetEnemyAsTarget();

    }

    /// <summary>
    /// sets the enemy that triggered the state change as the current target for the fleet
    /// </summary>
    public void SetEnemyAsTarget()
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
