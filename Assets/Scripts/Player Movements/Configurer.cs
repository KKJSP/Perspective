using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurer : MonoBehaviour {

    public GameObject left, right;

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
        
        Vector3 camPosright = Vector3.zero;
        Vector3 camPosleft = Vector3.zero;
        Vector3 posleft = transform.position;
        Vector3 posright = transform.position;
        if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 0)
        { 
            posright.x += 1;
            posleft.x -= 1;
            camPosright = posright + new Vector3(0, 0, -20);
            camPosleft = posleft + new Vector3(0, 0, -20);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 270)
        {
            posright.z += 1;
            posleft.z -= 1;
            camPosright = posright + new Vector3(20, 0, 0);
            camPosleft = posleft + new Vector3(20, 0, 0);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 90)
        {
            posright.z += -1;
            posleft.z -= -1;
            camPosright = posright + new Vector3(-20, 0, 0);
            camPosleft = posleft + new Vector3(-20, 0, 0);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 180)
        {
            posright.x += -1;
            posleft.x -= -1;
            camPosright = posright + new Vector3(0, 0, 20);
            camPosleft = posleft + new Vector3(0, 0, 20);
        }

        Vector3 dir_right = posright - camPosright;
        Vector3 dir_left = posleft - camPosleft;

        
            if (Physics.Raycast(camPosright, dir_right, out hitInfo, maxRayDist, layer) && posright != transform.position)
            {
                var hit = hitInfo;
                camPosright.y += 1;
                if (!Physics.Raycast(camPosright, dir_right, out hitInfo, maxRayDist, layer))
                {
                    right = hit.transform.gameObject;
                }
            }
            
            if (Physics.Raycast(camPosleft, dir_left, out hitInfo, maxRayDist, layer) && posleft != transform.position )
            {
                var hit = hitInfo;
                camPosleft.y += 1;
                if (!Physics.Raycast(camPosleft, dir_left, out hitInfo, maxRayDist, layer))
                {
                    left = hit.transform.gameObject;
                }
                

            }
            

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
        
    }

    public void ReConfigure(int level)
    {
        level--;
        if (level > 1)
        {
            if (right != null)
            {
                right.GetComponent<Configurer>().ReConfigure(level);
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().ReConfigure(level);
            }
            Configure();
            if (right != null)
            {
                right.GetComponent<Configurer>().ReConfigure(level);
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().ReConfigure(level);
            }
        }
        else
        {
            if (right != null)
            {
                right.GetComponent<Configurer>().Configure();
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().Configure();
            }
            Configure();
            if (right != null)
            {
                right.GetComponent<Configurer>().Configure();
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().Configure();
            }
        }

        if(tag == "StairBlock")
        {
            ConfigBelow();
            if (right != null)
            {
                right.GetComponent<Configurer>().ConfigBelow();
            }
            if (left != null)
            {
                left.GetComponent<Configurer>().ConfigBelow();
            }
        }

    }

    public void ConfigBelow()
    {
        Vector3 camPosDown = Vector3.zero;
        Vector3 posDown = transform.position;
        posDown.y -= 1;

        if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 0)
        {
            camPosDown = posDown + new Vector3(0, 0, -20);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 270)
        {
            camPosDown = posDown + new Vector3(20, 0, 0);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 90)
        {
            camPosDown = posDown + new Vector3(-20, 0, 0);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 180)
        {
            camPosDown = posDown + new Vector3(0, 0, 20);
        }

        Vector3 dir_down = posDown - camPosDown;

        if (Physics.Raycast(camPosDown, dir_down, out hitInfo, maxRayDist, layer) && posDown != transform.position)
        {
            hitInfo.transform.GetComponent<Configurer>().Configure();
        }
    }

    public void SetNull()
    {
        left = right = null;
    }



}
