using UnityEngine;
using System.Collections;


/// <summary>
/// contrls non-movment aspects of the enemy
/// </summary>
public class Enemy : MonoBehaviour
{

    public float TimeToArrive;

    public delegate void EnemyArrive();

    public static event EnemyArrive OnEnemyArrive;

    public bool Arrived;

    public void Update()
    {

        if (Arrived)
            return;

        // timer
        if (TimeToArrive > 0)
        {
            TimeToArrive -= Time.deltaTime;
            return;
        }

        // trigger OnEnemyArrive to change state on State Machine
        if (OnEnemyArrive != null)
        {
            gameObject.GetComponent<SB_SteeringBehaviours>().enabled = true;
            OnEnemyArrive();
            Arrived = true;
        }
    }
}
