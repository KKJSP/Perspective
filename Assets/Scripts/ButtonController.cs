using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    bool pressed = false;

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
            if (tag == "ToggleButton" && !pressed)
            {
                pressed = true;
                this.GetComponent<InteractionScript>().ButtonPressed();
            }
            else if (tag == "ToggleButton" && pressed)
            {
                pressed = false;
                this.GetComponent<InteractionScript>().ButtonLeft();
            }
            else if (tag == "PushButton")
            {
                this.GetComponent<InteractionScript>().ButtonPressed();
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (tag == "PushButton")
        {
            this.GetComponent<InteractionScript>().ButtonLeft();
        }
    }
}
