using UnityEngine;
using System.Collections;
using System;

public class PatrolState : State
{
    public override void Enter()
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
        //throw new NotImplementedException();
    }
}
