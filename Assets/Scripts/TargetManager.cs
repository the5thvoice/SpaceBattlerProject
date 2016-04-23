using UnityEngine;
using System.Collections;
using System;

public class TargetManager : MonoBehaviour
{

    public float DetectionRange;

    public GameObject Target
    {
        set { GetComponent<SB_SteeringBehaviours>().TargetObject = value; }
    }

    public void OnEnable()
    {
        GameManager.OnSetup += setUp;

    }

    private void setUp()
    {
        GameManager.GM.Fleet.Add(gameObject);
        GameManager.OnSetup -= setUp;
    }


    // Use this for initialization
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void SetTarget(GameObject curTarget)
    {
        Target = curTarget;
    }
}
