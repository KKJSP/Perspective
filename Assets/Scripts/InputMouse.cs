using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour {



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            InputManager.SetPan(1);
        }

        if (Input.GetMouseButton(0))
        {
            InputManager.SetDeltaPos(Input.GetAxis("Mouse X"));         
        }

        if(Input.GetMouseButtonUp(0))
        {
            InputManager.SetPan(2);
        }
    }

   
}

