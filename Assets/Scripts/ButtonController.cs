using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    Transform front, back, left, right, mesh;

    
	// Use this for initialization
	void Start () {
        front = transform.Find("Front");
        back = transform.Find("Back");
        right = transform.Find("Right");
        left = transform.Find("Left");
        mesh = transform.Find("Mesh");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {


        if (tag == "ToggleButton" || tag == "PushButton")
        {
            front.GetComponent<CurrentTransmitter>().ChangeState(front.gameObject);
            back.GetComponent<CurrentTransmitter>().ChangeState(back.gameObject);
            left.GetComponent<CurrentTransmitter>().ChangeState(left.gameObject);
            right.GetComponent<CurrentTransmitter>().ChangeState(right.gameObject);
        }

        Vector3 scale = mesh.localScale;
        Vector3 pos = mesh.position;
        scale.y /= 2;
        pos.y -= 0.1f;
        mesh.localScale = scale;
        mesh.position = pos;



    }

    void OnTriggerExit(Collider other)
    {
        if (tag == "PushButton")
        {
            front.GetComponent<CurrentTransmitter>().ChangeState(front.gameObject);
            back.GetComponent<CurrentTransmitter>().ChangeState(back.gameObject);
            left.GetComponent<CurrentTransmitter>().ChangeState(left.gameObject);
            right.GetComponent<CurrentTransmitter>().ChangeState(right.gameObject);
        }

        Vector3 scale = mesh.localScale;
        Vector3 pos = mesh.position;
        scale.y *= 2;
        pos.y += 0.1f;
        mesh.localScale = scale;
        mesh.position = pos;
    }
}
