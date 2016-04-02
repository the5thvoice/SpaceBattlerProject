using UnityEngine;
using System.Collections;


public enum AgentState
{
    seek,
    flee
}

public class SB_SteeringBehaviours : MonoBehaviour
{

    protected Rigidbody _rb;
    public Rigidbody rb
    {
        get
        {
            if (_rb == null) ;
            _rb = GetComponent<Rigidbody>();

            return _rb;
        }
    }

    public AgentState aiState;

    public bool isFleeing = false;


    protected Vector3 desiredVelocity, _target;
    public float MaxSpeed, rotationSpeed;
    public GameObject targetObject;   


    /// <summary>
    /// determins seek vector
    /// </summary>
    /// <param name="seekTarget"></param>
    /// <returns>seek direction vector</returns>
    public Vector3 Seek(Vector3 seekTarget)
    {

        desiredVelocity = seekTarget - transform.position;
        desiredVelocity.Normalize();
        desiredVelocity *= MaxSpeed;
        if (rb.velocity.magnitude > MaxSpeed)
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        return desiredVelocity - (Vector3)rb.velocity;

    }


    /// <summary>
    /// determins flee vector
    /// </summary>
    /// <param name="fleeTarget"></param>
    /// <returns>flee direction vector</returns>
    public Vector3 Flee(Vector3 fleeTarget)
    {
        desiredVelocity = transform.position - fleeTarget;
        desiredVelocity.Normalize();
        desiredVelocity *= MaxSpeed;
        if (rb.velocity.magnitude > MaxSpeed)
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        return desiredVelocity - (Vector3)rb.velocity;

    }
    
    /// <summary>
    /// rotates object to face its target
    /// </summary>
    /// <param name="faceThis"></param>
    /// <param name="faceAway"></param>
    public void Face(Vector3 faceThis, bool faceAway)
    {
        Vector3 dir = (faceThis - transform.position).normalized;
        if (faceAway)
            dir = -dir;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

    }
    public void FixedUpdate()
    {
        _target = targetObject.transform.position;

        switch (aiState)
        {
            default:
            case AgentState.seek:
                isFleeing = false;
                rb.AddForce(Seek(_target));
                break;
            case AgentState.flee:
                isFleeing = true;
                rb.AddForce(Flee(_target));
                break;
                


        }
        
    }




    // Update is called once per frame
    public void Update()
    {

        Face(_target, isFleeing);

    }
}
