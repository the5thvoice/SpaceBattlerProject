﻿using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// mmanages the current target of the ship it's attached to.
/// </summary>
public class TargetManager : MonoBehaviour
{

    public float CloseRange;

    private GameObject _Target;
    public GameObject Target
    {
        set
        {
            GetComponent<SB_SteeringBehaviours>().TargetObject = value;
            _Target = value;
        }
        get { return _Target; }
    }

    public GameObject previousTarget;

    public void OnEnable()
    {
        GameManager.OnSetup += setUp;

    }

    private void setUp()
    {
        GameManager.GM.Fleet.Add(gameObject);
        GameManager.OnSetup -= setUp;
    }


    
    /// <summary>
    /// updates the current target
    /// </summary>
    /// <param name="curTarget"></param>
    internal void SetTarget(GameObject curTarget)
    {
        if (Target != null)
            previousTarget = Target;

        Target = curTarget;


    }


    public void Mode(AgentState SteeringMode)
    {
        GetComponent<SB_SteeringBehaviours>().aiState = SteeringMode;
    }
}
