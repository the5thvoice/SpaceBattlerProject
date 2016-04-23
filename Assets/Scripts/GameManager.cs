using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager GM;
    private List<GameObject> _Fleet;
    public List<GameObject> Fleet
    {
        get { return _Fleet ?? (_Fleet = new List<GameObject>()); }
    }

    StateMachine SM
    {
        get { return GetComponent<StateMachine>(); }
    }


    public void Awake()
    {
        if (GM == null)
            GM = this;

    }


	// Use this for initialization
	void Start ()
	{

	    SM.SwitchState(new PatrolState());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
