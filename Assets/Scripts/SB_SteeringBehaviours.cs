﻿using UnityEngine;
using System.Collections;


public enum AgentState
{
    seek,
    flee,
    persue
    
}

public class SB_SteeringBehaviours : MonoBehaviour
{

    protected Rigidbody _rb;
    public Rigidbody Rb
    {
        get
        {
            if (_rb == null) ;
            _rb = GetComponent<Rigidbody>();

            return _rb;
        }
    }

    public AgentState aiState;

    public bool IsFleeing = false;


    protected Vector3 DesiredVelocity, Target;
    public float MaxSpeed,  RotationSpeed, MaxPrediction, WallAvoidDistance, DistanceToAvoid;

    public GameObject TargetObject;

    public LayerMask WhatToAvoid;
      


    /// <summary>
    /// determins seek vector
    /// </summary>
    /// <param name="seekTarget"></param>
    /// <returns>seek direction vector</returns>
    public virtual Vector3 Seek(Vector3 seekTarget)
    {

        DesiredVelocity = seekTarget - transform.position;
        DesiredVelocity.Normalize();
        DesiredVelocity *= MaxSpeed;
        if (Rb.velocity.magnitude > MaxSpeed)
            Rb.velocity = Rb.velocity.normalized * MaxSpeed;
        return DesiredVelocity - (Vector3)Rb.velocity;

    }


    /// <summary>
    /// determins flee vector
    /// </summary>
    /// <param name="fleeTarget"></param>
    /// <returns>flee direction vector</returns>
    public virtual Vector3 Flee(Vector3 fleeTarget)
    {
        DesiredVelocity = transform.position - fleeTarget;
        DesiredVelocity.Normalize();
        DesiredVelocity *= MaxSpeed;
        if (Rb.velocity.magnitude > MaxSpeed)
            Rb.velocity = Rb.velocity.normalized * MaxSpeed;
        return DesiredVelocity - (Vector3)Rb.velocity;

    }
    
    /// <summary>
    /// rotates object to face its target
    /// </summary>
    /// <param name="faceThis"></param>
    /// <param name="faceAway"></param>
    public virtual void Face(Vector3 faceThis, bool faceAway)
    {
        Vector3 dir = (faceThis - transform.position).normalized;
        if (faceAway)
            dir = -dir;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * RotationSpeed);
        

    }


    /// <summary>
    /// generates a chase vector for target object
    /// </summary>
    /// <param name="targetObject"></param>
    /// <returns>Vector3</returns>
    public virtual Vector3 Persue(GameObject targetObject)
    {

        float prediction;
        Vector3 direction = targetObject.transform.position - transform.position;
        float distance = direction.magnitude;
        float speed = Rb.velocity.magnitude;
        if (speed <= distance/MaxPrediction)
        {
            prediction = MaxPrediction;
        }
        else
        {
            prediction = distance/speed;
        }
        Vector3 newTarget = targetObject.transform.position;
        Vector3 targetVelosity = new Vector3();
        Rigidbody targetRB = targetObject.GetComponent<Rigidbody>();
        if (targetRB != null)
        {
            targetVelosity = targetRB.velocity;
        }
        newTarget += targetVelosity*prediction;
        return Seek(newTarget);

    }

    /// <summary>
    /// Avoids objects in specified layer
    /// </summary>
    /// <returns>Direction as Vector3</returns>
    public virtual Vector3 Avoid()
    {
        RaycastHit detector;


        if (!Physics.Raycast(transform.position, Rb.velocity.normalized*WallAvoidDistance,out detector,
            WallAvoidDistance, WhatToAvoid))
            return Vector3.zero;

        Vector3 avoidTarget = detector.point + detector.normal * DistanceToAvoid;
        Face(avoidTarget, false);
        return Seek(avoidTarget);

    }

    
    public virtual void FixedUpdate()
    {
        Target = TargetObject.transform.position;

        switch (aiState)
        {
            default:
            case AgentState.seek:
                IsFleeing = false;
                
                Rb.AddForce(Seek(Target));
                Face(Target, IsFleeing);
                
                
                break;
            case AgentState.flee:
                IsFleeing = true;
                
                Rb.AddForce(Flee(Target));
                Face(Target, IsFleeing);
                break;
            case AgentState.persue:
                IsFleeing = false;
                
                Rb.AddForce(Persue(TargetObject));
                Face(Target, IsFleeing);
                break;
            


        }
        Rb.AddForce(Avoid());

    }

    

    

    
}
