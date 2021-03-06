﻿using UnityEngine;
using System.Collections;
using System;

public class PatrolState : State
{
    public override void Enter()
    {

        SetInitialTarget();

        
    }
    /// <summary>
    /// set the initial target for for each ship in the fleet, based on which station is closet to it
    /// </summary>
    private void SetInitialTarget()
    {
        foreach (GameObject obj in GameManager.GM.Fleet)
        {
            TargetManager TM = obj.GetComponent<TargetManager>();
            Vector3 objPos = obj.transform.position;
            GameObject curTarget = null;
            foreach (GameObject wayP in GameManager.GM.Stations)
            {
                float dist = Vector3.Distance(objPos, wayP.transform.position);

                if (curTarget == null)
                {
                    curTarget = wayP;
                    continue;
                }

                if (dist < Vector3.Distance(curTarget.transform.position, objPos))
                {
                    curTarget = wayP;
                }



            }
            TM.SetTarget(curTarget);
        }
    }

    public override void Exit()
    {
        //throw new NotImplementedException();
    }

    public override void Update()
    {
        UpdateTarget();

        
    }

    /// <summary>
    /// updtates the current target if a ship has reached the current taget
    /// </summary>
    private void UpdateTarget()
    {
        foreach (GameObject obj in GameManager.GM.Fleet)
        {
            TargetManager TM = obj.GetComponent<TargetManager>();
            Vector3 objPos = obj.transform.position;
            GameObject curTarget = null;
            if (Vector3.Distance(TM.Target.transform.position, objPos) >= TM.CloseRange)
                continue;


            if (GameManager.GM.Stations.Count <= 2)
            {
                if (TM.previousTarget != null)
                {
                    curTarget = TM.previousTarget;
                    TM.SetTarget(curTarget);
                    continue;
                }
            }

            foreach (GameObject wayP in GameManager.GM.Stations)
            {
                if (wayP == TM.Target)
                    continue;

                if (wayP == TM.previousTarget)
                    continue;

                float dist = Vector3.Distance(objPos, wayP.transform.position);

                if (curTarget == null)
                {
                    curTarget = wayP;
                    continue;
                }

                if (dist < Vector3.Distance(curTarget.transform.position, objPos))
                {
                    curTarget = wayP;
                }



            }
            TM.SetTarget(curTarget);
        }
    }
}
