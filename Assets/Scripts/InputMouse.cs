using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour
{

    int mouseButtonReleaseBlurRange = 2;
    bool drag = false;
    Vector3 Button0DownPoint;
    Vector3 Button0UpPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //  CLICKS FOR MOUSE BUTTON

        if (Input.GetButtonDown("Fire1"))
        {
            Button0DownPoint = Input.mousePosition;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Button0UpPoint = Input.mousePosition;

            if (IsInRange(Button0DownPoint, Button0UpPoint))
            {
                Mouse0Click();  //Function where all actions associated with LMB clicks are performed
            }

            if(drag)
            {
                InputManager.SetPan(2);
                drag = false;
            }
        }


        //  DRAGS FOR MOUSE

        if (Input.GetButton("Fire1"))
        {
            if (Input.mousePosition != Button0DownPoint)
            {
                Mouse0Drag();//Function where all actions associated with LMB drag are performed
            }
        }


    }

    bool IsInRange(Vector2 v1, Vector2 v2)
    {
        if (Vector2.Distance(v1, v2) < mouseButtonReleaseBlurRange)
        {
            return true;
        }
        return false;
    }

    void Mouse0Click()
    {
        Vector3 mousePos = Input.mousePosition;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        player.GetComponent<PlayerController>().CheckPath(ray);
    }

    void Mouse0Drag()
    {
        
        InputManager.SetPan(3);
        InputManager.SetDeltaPos(Input.GetAxis("Mouse X"));
        drag = true;
    }
}

