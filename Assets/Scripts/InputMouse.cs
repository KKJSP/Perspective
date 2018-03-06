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
            InputManager.SetCamInitialPos(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }

        if (Input.GetMouseButton(0))
        {
            InputManager.SetCamCurrentPos(Input.mousePosition);         
        }

        if(Input.GetMouseButtonUp(0))
        {
            InputManager.SetPan(2);
        }
    }

   
}

