using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour {

    public GameObject[] Interactives;

    public delegate void VoidVoidDelegate();
    public VoidVoidDelegate ObjectTouch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ButtonSwitchTouch()
    {
        if(ObjectTouch != null)
        {
            ObjectTouch();
        }
    }

    



}
