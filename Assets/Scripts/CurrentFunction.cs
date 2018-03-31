using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentFunction : MonoBehaviour {

    private void OnEnable()
    {
        GetComponent<CurrentTransmitter>().CurrentEnd += CurrentEndFunctions;
    }

    private void OnDisable()
    {
        GetComponent<CurrentTransmitter>().CurrentEnd -= CurrentEndFunctions;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CurrentEndFunctions()
    {
        GetComponent<InteractionScript>().ButtonSwitchTouch();
    }
}
