using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    bool pressed = false;

    int layer;

    Transform front, back, left, right, mesh;

    RaycastHit hitInfo = new RaycastHit();
    

    private void OnEnable()
    {
        PlayerController.PlayerMovedUnit += CheckPressed;
    }

    private void OnDisable()
    {
        PlayerController.PlayerMovedUnit -= CheckPressed;
    }


    // Use this for initialization
    void Start () {
        front = transform.Find("Front");
        back = transform.Find("Back");
        right = transform.Find("Right");
        left = transform.Find("Left");
        mesh = transform.Find("Mesh");
        layer = LayerMask.GetMask("Button");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckPressed(int pos)
    {
        Vector3 position = PlayerController.player.transform.position;
        Vector3 camPos = Vector3.zero;

        //Setting Raycast Origin and Directions
        if (pos == 0)
        {
            camPos = position + new Vector3(0, 0, -20);
        }
        else if (pos == 1)
        {
            camPos = position + new Vector3(-20, 0, 0);
        }
        else if (pos == 3)
        {
            camPos = position + new Vector3(20, 0, 0);
        }
        else if (pos == 2)
        {
            camPos = position + new Vector3(0, 0, 20);
        }
        Vector3 dir = position - camPos;

        if (Physics.Raycast(position, dir, out hitInfo, PlayerController.maxRayDist, layer) && hitInfo.transform == transform && !pressed)
        {
            OnEnter(PlayerController.player.GetComponent<Collider>());
            pressed = true;
        }
        else if (Physics.Raycast(position, dir, out hitInfo, PlayerController.maxRayDist, layer) && hitInfo.transform == transform && pressed)
        {
            
        }
        else if (pressed)
        {
            OnExit(PlayerController.player.GetComponent<Collider>());
        }
    }

    void OnEnter(Collider other)
    {


        if (tag == "ToggleButton")
        {
            GetComponent<InteractionScript>().ButtonSwitchTouch();
            front.GetComponent<CurrentTransmitter>().ChangeState(front.gameObject);
            back.GetComponent<CurrentTransmitter>().ChangeState(back.gameObject);
            left.GetComponent<CurrentTransmitter>().ChangeState(left.gameObject);
            right.GetComponent<CurrentTransmitter>().ChangeState(right.gameObject);

        }

        else if (tag == "PushButton")
        {
            GetComponent<InteractionScript>().ButtonSwitchTouch();
        }

        Vector3 scale = mesh.localScale;
        Vector3 pos = mesh.position;
        scale.y /= 2;
        pos.y -= 0.1f;
        mesh.localScale = scale;
        mesh.position = pos;
    }


    void OnExit(Collider other)
    {
        if (tag == "PushButton")
        {
            GetComponent<InteractionScript>().ButtonSwitchTouch();
        }

        Vector3 scale = mesh.localScale;
        Vector3 pos = mesh.position;
        scale.y *= 2;
        pos.y += 0.1f;
        mesh.localScale = scale;
        mesh.position = pos;
        pressed = false;
    }


    void OnTriggerEnter(Collider other)
    {


        if (tag == "ToggleButton")
        {
            front.GetComponent<CurrentTransmitter>().ChangeState(front.gameObject);
            back.GetComponent<CurrentTransmitter>().ChangeState(back.gameObject);
            left.GetComponent<CurrentTransmitter>().ChangeState(left.gameObject);
            right.GetComponent<CurrentTransmitter>().ChangeState(right.gameObject);
        }
        else if(tag == "PushButton")
        {
            GetComponent<InteractionScript>().ButtonSwitchTouch();
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
            GetComponent<InteractionScript>().ButtonSwitchTouch();
        }

        Vector3 scale = mesh.localScale;
        Vector3 pos = mesh.position;
        scale.y *= 2;
        pos.y += 0.1f;
        mesh.localScale = scale;
        mesh.position = pos;
    }
}
