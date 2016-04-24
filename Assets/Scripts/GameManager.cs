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


    StateMachine SM
    {
        get { return GetComponent<StateMachine>(); }
    }

    public delegate void Setup();
    public static event Setup OnSetup;

    public delegate void EnemyArrive();
    public static event EnemyArrive OnEnemyArrive;


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
        SM.SwitchState(new PatrolState());

    }

    // Update is called once per frame
    void Update()
    {

    }


    public List<GameObject> FindValid(GameObject o)
    {

        if (Fleet.Contains(o))
        {
            return Fleet;
        }


        return null;

        
    }
}
