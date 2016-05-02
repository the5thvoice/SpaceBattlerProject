using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    private List<GameObject> _Fleet;
    public List<GameObject> Fleet
    {
        get { return _Fleet ?? (_Fleet = new List<GameObject>()); }
    }

    private List<GameObject> _Stations;

    public List<GameObject> Stations
    {
        get { return _Stations ?? (_Stations = new List<GameObject>()); }
    }

    public GameObject EnemyShip;

    StateMachine SM
    {
        get { return GetComponent<StateMachine>(); }
    }


    // Setup event to allow more specfic control of the order in which scripts set up in what order
    public delegate void Setup();
    public static event Setup OnSetup;

    


    public void Awake()
    {
        if (GM == null)
            GM = this;



    }


    // Use this for initialization
    void Start()
    {
        if (OnSetup != null)
            OnSetup();
        SM.SwitchState(new PatrolState()); // sets the initial states of the state machine
        Enemy.OnEnemyArrive += AlertToEnemy; // suscribes the state chage defination for when the enmey arrives
    }

    /// <summary>
    /// chages state to the persue enemy state
    /// </summary>
    public void AlertToEnemy()
    {
        SM.SwitchState(new PersueEnemy()); 
        GetComponent<AudioSource>().Play();
        Enemy.OnEnemyArrive -= AlertToEnemy;
    }
    

    /// <summary>
    /// gets the list containting submited GameObjct
    /// </summary>
    /// <param name="o"></param>
    /// <returns>List</returns>
    public List<GameObject> FindValid(GameObject o)
    {

        if (Fleet.Contains(o))
        {
            return Fleet;
        }
        else if (Stations.Contains(o))
        {
            return Stations;
        }



        return null;

        
    }
}
