using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Flocking : SB_SteeringBehaviours
{
    private List<GameObject> Flock = new List<GameObject>();
    public float Radius, SeperationWeight, CohesionWeight, AlignWeight, SeekWight, AvoidWeight;
    public float MaxForceMag;


    public override void FixedUpdate()
    {
        Target = TargetObject.transform.position;
        Vector3 steeringForce = CalculateWeightedPrioritized();
        Rb.AddForce(steeringForce);
        Face(Target, false);
    }

    


    /// <summary>
    /// calculates the priority to each movemnt available
    /// </summary>
    /// <returns>a direction</returns>
    private Vector3 CalculateWeightedPrioritized()
    {

        TagNeighbour(Radius);
        Vector3 force = Vector3.zero;
        Vector3 SteeringForce = Vector3.zero;

        if (Flock.Count > 0)
        {
            force = Seperation()*SeperationWeight;
            if (!AccumulateForce(ref SteeringForce, force))
            {
                return SteeringForce;
            }
            force = Alignment()*AlignWeight;
            if (!AccumulateForce(ref SteeringForce, force))
            {
                return SteeringForce;
            }
            force = Cohesion()*CohesionWeight;
            if (!AccumulateForce(ref SteeringForce, force))
            {
                return SteeringForce;
            }
            
        }
        force = Avoid()*AvoidWeight;
        if (!AccumulateForce(ref SteeringForce, force))
        {
            return SteeringForce;
        }
        force = Seek(Target) * SeekWight;
        if (!AccumulateForce(ref SteeringForce, force))
        {
            return SteeringForce;
        }


        return SteeringForce;
    }


    private Vector3 Cohesion()
    {
        Vector3 steeringForce = Vector3.zero;
        Vector3 centreOfMass = Vector3.zero;
        int flockCount = 0;

        foreach (GameObject entity in Flock)
        {
            if (entity == gameObject)
                continue;

            centreOfMass += entity.transform.position;
            flockCount++;
        }

        if (flockCount > 0)
        {
            centreOfMass /= flockCount;
            if (centreOfMass.sqrMagnitude == 0)
                return Vector3.zero;
            else
                steeringForce = Vector3.Normalize(Seek(centreOfMass));

        }

        return steeringForce;
    }


    private Vector3 Alignment()
    {
        Vector3 steeringForce = Vector3.zero;
        int flockCount = 0;

        foreach (GameObject entity in Flock)
        {
            if (entity == gameObject)
                continue;

            steeringForce += entity.transform.forward;
            flockCount++;


        }
        if (flockCount > 0)
        {
            steeringForce /= flockCount;
            steeringForce -= transform.forward;
        }

        return steeringForce;


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="steeringForce"></param>
    /// <param name="force"></param>
    /// <returns></returns>
    private bool AccumulateForce(ref Vector3 steeringForce, Vector3 force)
    {
        float soFar = steeringForce.magnitude;

        float remaining = MaxForceMag - soFar;

        if (remaining <= 0)
            return false;

        float toAdd = force.magnitude;

        if (toAdd <= remaining)
        {
            steeringForce += force;
        }
        else
        {
            steeringForce += force.normalized*remaining;
        }
        return true;
    }

    private Vector3 Seperation()
    {
        Vector3 steeringForce = Vector3.zero;

        for (int i = 0; i < Flock.Count; i++)
        {
            GameObject entity = Flock[i];

            if (enabled != null)
            {
                Vector3 toEntity = transform.position - entity.transform.position;
                steeringForce += toEntity.normalized/toEntity.magnitude;
            }
        }


        return steeringForce;
    }


    /// <summary>
    /// gets each similar object in range and adds them to the fleet
    /// </summary>
    /// <param name="radius"></param>
    /// <returns> count[int] of in range fleet objects </returns>
    private int TagNeighbour(float radius)
    {
        Flock.Clear();
        List<GameObject> steerables = GameManager.GM.FindValid(gameObject);

        foreach (GameObject steerable in steerables)
        {
            if (steerable == gameObject)
                continue;

            if ((transform.position - steerable.transform.position).magnitude < radius)
                Flock.Add(steerable);

        }


        return Flock.Count;

    }
}
