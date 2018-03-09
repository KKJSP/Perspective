using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurer : MonoBehaviour {

    public GameObject left, right;

    int layer;
    float maxRayDist;

    RaycastHit hitInfo, checkAbove = new RaycastHit();

    // Use this for initialization
    void Start () {
        left = right = null;
        layer = PlayerController.layer;
        maxRayDist = PlayerController.maxRayDist;


        //Finding Neighbours
        Vector3 camPosright = Vector3.zero;
        Vector3 camPosleft = Vector3.zero;
        Vector3 posleft = transform.position;
        Vector3 posright = transform.position;
        if (transform.localEulerAngles.x == 0 && transform.localEulerAngles.y == 0)
        {
            posright.x += 1;
            posleft.x -= 1;
            camPosright = posright + new Vector3(0, 0, -20);
            camPosleft = posleft + new Vector3(0, 0, -20);
        }
        else if (transform.localEulerAngles.x == 0 && transform.localEulerAngles.y == 270)
        {
            posright.z += 1;
            posleft.z -= 1;
            camPosright = posright + new Vector3(20, 0, 0);
            camPosleft = posleft + new Vector3(20, 0, 0);
        }
        else if (transform.localEulerAngles.x == 0 && transform.localEulerAngles.y == 90)
        {
            posright.z += -1;
            posleft.z -= -1;
            camPosright = posright + new Vector3(-20, 0, 0);
            camPosleft = posleft + new Vector3(-20, 0, 0);
        }
        else if (transform.localEulerAngles.x == 0 && transform.localEulerAngles.y == 180)
        {
            posright.x += -1;
            posleft.x -= -1;
            camPosright = posright + new Vector3(0, 0, 20);
            camPosleft = posleft + new Vector3(0, 0, 20);
        }

        Vector3 dir_right = posright - camPosright;
        Vector3 dir_left = posleft - camPosleft;

        if (camPosleft != Vector3.zero && posleft != transform.position)
        {
            if (Physics.Raycast(camPosright, dir_right, out hitInfo, maxRayDist, layer))
            {
                camPosright.y += 1;
                if(!Physics.Raycast(camPosright, dir_right, out checkAbove, maxRayDist, layer))
                    right = hitInfo.transform.gameObject;
            }
            if (Physics.Raycast(camPosleft, dir_left, out hitInfo, maxRayDist, layer))
            {
                camPosleft.y += 1;
                if(!Physics.Raycast(camPosleft, dir_left, out checkAbove, maxRayDist, layer))
                    left = hitInfo.transform.gameObject;
            }

            Vector3 posup = transform.position;
            posup.y += 1;
            Vector3 camPosup = posup - dir_right;

            if(Physics.Raycast(camPosup, dir_right, out hitInfo, maxRayDist, layer))
            {
                left = null;
                right = null;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
