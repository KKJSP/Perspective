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


        if (tag == "ToggleButton" || tag == "PushButton")
        {
            this.GetComponent<InteractionScript>().ButtonSwitchTouch();
        }

        Vector3 scale = transform.localScale;
        Vector3 pos = transform.position;
        scale.y /= 2;
        pos.y -= 0.25f;
        transform.localScale = scale;
        transform.position = pos;



    }

    void OnTriggerExit(Collider other)
    {
        if (tag == "PushButton")
        {
            this.GetComponent<InteractionScript>().ButtonSwitchTouch();
        }

        Vector3 scale = transform.localScale;
        Vector3 pos = transform.position;
        scale.y *= 2;
        pos.y += 0.25f;
        transform.localScale = scale;
        transform.position = pos;
    }
}
