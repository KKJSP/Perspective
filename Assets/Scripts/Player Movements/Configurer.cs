using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurer : MonoBehaviour {

    public GameObject left, right;

    bool mover = false;
    int layer;
    float maxRayDist;

    RaycastHit hitInfo = new RaycastHit();

    private void Awake()
    {
        left = right = null;
        layer = LayerMask.GetMask("PathBlock", "ColourOnePath", "ColourTwoPath");

    }


    // Use this for initialization
    void Start () {
        maxRayDist = PlayerController.maxRayDist;
        Configure();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Configure()
    {

        if (tag == "Current")
        {
            layer = LayerMask.GetMask("Current", "Switch");
        }


        //Finding Neighbours
        Vector3 camPosright = Vector3.zero;
        Vector3 camPosleft = Vector3.zero;
        Vector3 posleft = transform.position;
        Vector3 posright = transform.position;
        if (transform.localEulerAngles.y == 0)
        { 
            posright.x += 1;
            posleft.x -= 1;
            camPosright = posright + new Vector3(0, 0, -20);
            camPosleft = posleft + new Vector3(0, 0, -20);
        }
        else if (transform.localEulerAngles.y == 270)
        {
            posright.z += 1;
            posleft.z -= 1;
            camPosright = posright + new Vector3(20, 0, 0);
            camPosleft = posleft + new Vector3(20, 0, 0);
        }
        else if (transform.localEulerAngles.y == 90)
        {
            posright.z += -1;
            posleft.z -= -1;
            camPosright = posright + new Vector3(-20, 0, 0);
            camPosleft = posleft + new Vector3(-20, 0, 0);
        }
        else if (transform.localEulerAngles.y == 180)
        {
            posright.x += -1;
            posleft.x -= -1;
            camPosright = posright + new Vector3(0, 0, 20);
            camPosleft = posleft + new Vector3(0, 0, 20);
        }

        Vector3 dir_right = posright - camPosright;
        Vector3 dir_left = posleft - camPosleft;

        //if (camPosleft != Vector3.zero && camPosright != Vector3.zero) // Take this out if not required.
        //{
            if (Physics.Raycast(camPosright, dir_right, out hitInfo, maxRayDist, layer) && posright != transform.position)
            {
                var hit = hitInfo;
                camPosright.y += 1;
                if (!Physics.Raycast(camPosright, dir_right, out hitInfo, maxRayDist, layer))
                    right = hit.transform.gameObject;
                else if(mover)
                    right = null;
            }
            else if (mover)
                right = null;
            if (Physics.Raycast(camPosleft, dir_left, out hitInfo, maxRayDist, layer) && posleft != transform.position )
            {

                var hit = hitInfo;
                camPosleft.y += 1;
                if (!Physics.Raycast(camPosleft, dir_left, out hitInfo, maxRayDist, layer))
                    left = hit.transform.gameObject;
                else if (mover)
                    left = null;
            }
            else if (mover)
                left = null;

        Vector3 posup = transform.position;
            posup.y += 1;
            Vector3 camPosup = posup - dir_right;

            if (Physics.Raycast(camPosup, dir_right, out hitInfo, maxRayDist, layer))
            {
                if (hitInfo.transform.tag == "StairBlock")
                {
                    if (left != null && hitInfo.transform.name == "L2R")
                    {
                        left.GetComponent<Configurer>().right = hitInfo.transform.gameObject;
                        hitInfo.transform.GetComponent<Configurer>().left = left;
                    }
                    if (right != null && hitInfo.transform.name == "R2L")
                    {
                        right.GetComponent<Configurer>().left = hitInfo.transform.gameObject;
                        hitInfo.transform.GetComponent<Configurer>().right = right;
                    }
                }
                                
                left = null;
                right = null;
                
            }
        //}
    }

    public void ReConfigure(int level)
    {
        level--;
        if (level > 0)
        {
            if (right != null)
            {
                right.GetComponent<Configurer>().SetMover(true);
                right.GetComponent<Configurer>().ReConfigure(level);
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().SetMover(true);
                left.GetComponent<Configurer>().ReConfigure(level);
            }
            Configure();
            if (right != null)
            {
                right.GetComponent<Configurer>().SetMover(true);
                right.GetComponent<Configurer>().ReConfigure(level);
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().SetMover(true);
                left.GetComponent<Configurer>().ReConfigure(level);
            }
        }
        else
        {
            if (right != null)
            {
                right.GetComponent<Configurer>().SetMover(true);
                right.GetComponent<Configurer>().Configure();
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().SetMover(true);
                left.GetComponent<Configurer>().Configure();
            }
            Configure();
            if (right != null)
            {
                right.GetComponent<Configurer>().SetMover(true);
                right.GetComponent<Configurer>().Configure();
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().SetMover(true);
                left.GetComponent<Configurer>().Configure();
            }
        }
    }

    public void SetMover(bool value)
    {
        mover = value;
    }

}
