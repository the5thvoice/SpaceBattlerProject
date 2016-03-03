using UnityEngine;
using System.Collections;

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


    protected Vector3 desiredVelocity, _target;
    public float MaxSpeed, rotationSpeed;
    public GameObject targetObject;   

    public Vector3 Seek(Vector3 seekTarget)
    {

        desiredVelocity = seekTarget - transform.position;
        desiredVelocity.Normalize();
        desiredVelocity *= MaxSpeed;

        if (rb.velocity.magnitude > MaxSpeed)
            rb.velocity = rb.velocity.normalized * MaxSpeed;

        //transform.forward = desiredVelocity;
        return desiredVelocity - (Vector3)rb.velocity;

    }

    public void Face(Vector3 faceThis)
    {
        Vector3 dir = (faceThis - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

    }
    

    

    public void FixedUpdate()
    {
        _target = targetObject.transform.position;
        rb.AddForce(Seek(_target));
    }




    // Update is called once per frame
    public void Update()
    {

        Face(_target);

    }
}
