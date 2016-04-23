using UnityEngine;
using System.Collections;

public class Station : MonoBehaviour {


    public void OnEnable()
    {
        GameManager.OnSetup += setUp;

    }

    private void setUp()
    {
        GameManager.GM.Stations.Add(gameObject);
        GameManager.OnSetup -= setUp;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
