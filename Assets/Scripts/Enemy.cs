using UnityEngine;
using System.Collections;

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

        if (TimeToArrive > 0)
        {
            TimeToArrive -= Time.deltaTime;
            return;
        }

        
        if (OnEnemyArrive != null)
        {
            gameObject.GetComponent<SB_SteeringBehaviours>().enabled = true;
            OnEnemyArrive();
            Arrived = true;
        }
    }
}
