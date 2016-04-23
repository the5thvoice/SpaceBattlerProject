using UnityEngine;
using System.Collections;

public abstract class State { 
    protected GameObject _Entity;

    public GameObject Entity
    {
        set { _Entity = value; }
        get { return _Entity; }
    }


    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
