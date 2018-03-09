using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<InteractionScript>().ButtonPressed();
        }
    }

    void OnTriggerExit(Collider other)
    {
        this.GetComponent<InteractionScript>().ButtonLeft();
    }
}
