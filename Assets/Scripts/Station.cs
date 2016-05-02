using UnityEngine;
using System.Collections;

public class Station : MonoBehaviour {


    public void OnEnable()
    {
        GameManager.OnSetup += setUp;

    }


    private void setUp()
    {
        GameManager.GM.Stations.Add(gameObject);// adds this station to the list of active stations
        GameManager.OnSetup -= setUp;
    }
    
}
