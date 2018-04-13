using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTransmitter : MonoBehaviour {

    public Vector2 transmitDir;
    public GameObject left, right, up, down;
    public Material onMat, offMat;

    bool on = false, deConfigured;
    int layer;
    float maxRayDist;

    Renderer rend;
    RaycastHit hitInfo = new RaycastHit();

    IEnumerator coroutine;

    public delegate void VoidVoidDelegate();
    public VoidVoidDelegate CurrentEnd;

    private void Awake()
    {
        if (tag == "Current")
        {
            GameObject child = transform.Find("Mesh").gameObject;
            rend = child.GetComponent<Renderer>();
            offMat = rend.material;
        }
        else if (tag == "Switch")
        {
            rend = GetComponent<Renderer>();
            offMat = rend.material;
        }
    }

    // Use this for initialization
    void Start()
    {
        maxRayDist = PlayerController.maxRayDist;
        layer = LayerMask.GetMask("Current", "Switch");
        Configure();
        if(tag == "Switch")
        {
            transmitDir.Set(2, 2);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Configure()
    {
        up = down = left = right = null;

        //Finding Neighbours
        Vector3 camPosright = Vector3.zero;
        Vector3 camPosleft = Vector3.zero;
        Vector3 posleft = transform.position;
        Vector3 posright = transform.position;
        if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 0)
        {
            if (transmitDir.x == 1)
                posright.x += 1;
            else if (transmitDir.x == -1)
                posleft.x -= 1;
            else if (transmitDir.x != 0)
            {
                posright.x += 1;
                posleft.x -= 1;
            }
            camPosright = posright + new Vector3(0, 0, -20);
            camPosleft = posleft + new Vector3(0, 0, -20);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 270)
        {
            if (transmitDir.x == 1)
                posright.z += 1;
            else if (transmitDir.x == -1)
                posleft.z -= 1;
            else if (transmitDir.x != 0)
            {
                posright.z += 1;
                posleft.z -= 1;
            }
            camPosright = posright + new Vector3(20, 0, 0);
            camPosleft = posleft + new Vector3(20, 0, 0);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 90)
        {
            if (transmitDir.x == -1)
                posleft.z += 1;
            else if (transmitDir.x == 1)
                posright.z -= 1;
            else if (transmitDir.x != 0)
            {
                posright.z -= 1;
                posleft.z += 1;
            }
            camPosright = posright + new Vector3(-20, 0, 0);
            camPosleft = posleft + new Vector3(-20, 0, 0);
        }
        else if ((transform.parent.localEulerAngles.y + transform.localEulerAngles.y) % 360 == 180)
        {
            if (transmitDir.x == -1)
                posleft.x += 1;
            else if (transmitDir.x == 1)
                posright.x -= 1;
            else if (transmitDir.x != 0)
            {
                posright.x -= 1;
                posleft.x += 1;
            }
            camPosright = posright + new Vector3(0, 0, 20);
            camPosleft = posleft + new Vector3(0, 0, 20);
        }


        Vector3 dir_right = posright - camPosright;
        Vector3 dir_left = posleft - camPosleft;

        if (camPosleft != Vector3.zero)
        {
            if (Physics.Raycast(camPosright, dir_right, out hitInfo, maxRayDist, layer) && posright != transform.position)
            {
                right = hitInfo.transform.gameObject;
            }
            if (Physics.Raycast(camPosleft, dir_left, out hitInfo, maxRayDist, layer) && posleft != transform.position)
            {
                left = hitInfo.transform.gameObject;
            }

            Vector3 posup = transform.position;
            posup.y += 1;
            Vector3 camPosup = posup - dir_right;

            if (Physics.Raycast(camPosup, dir_right, out hitInfo, maxRayDist, layer) && (transmitDir.y == 1 || transmitDir.y != 0))
            {
                up = hitInfo.transform.gameObject;
            }

            posup.y -= 2;
            camPosup = posup - dir_right;

            if (Physics.Raycast(camPosup, dir_right, out hitInfo, maxRayDist, layer) && (transmitDir.y == -1 || transmitDir.y != 0))
            {
                down = hitInfo.transform.gameObject;
            }
        }

        deConfigured = false;
    }

    public void ReConfigure()
    {
        if (right != null)
        {
            right.GetComponent<CurrentTransmitter>().Configure();
        }
        if (left != null)
        {
            left.GetComponent<CurrentTransmitter>().Configure();
        }
        if (up != null)
        {
            up.GetComponent<CurrentTransmitter>().Configure();
        }
        if (down != null)
        {
            left.GetComponent<CurrentTransmitter>().Configure();
        }
        Configure();
    }

    public void DeConfigure()
    {
        up = down = left = right = null;
        deConfigured = true;
    }


    public void ChangeState(GameObject incoming)
    {
        if ((incoming != left && incoming != right && incoming != up && incoming != down) && incoming.tag != "Switch" && incoming != gameObject)
        {
            return;
        }

        if(deConfigured)
        {
            return;
        }

        if(coroutine!=null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = OnOff(incoming);
        StartCoroutine(coroutine);     

    }

    IEnumerator OnOff(GameObject incoming)
    {
        bool prev_state = on;

        while (prev_state == on)
        {
            on = !on;
            //Changing Material
            if (!on)
            {
                rend.material = offMat;
            }
            else
            {
                rend.material = onMat;
            }

            if(CurrentEnd != null)
            {
                CurrentEnd();
            }

            yield return new WaitForSeconds(0.05f);
        }

        //Transmitting Current
        if (right != null && right != incoming)
        {
            right.GetComponent<CurrentTransmitter>().ChangeState(gameObject);
        }
        if (left != null && left != incoming)
        {
            left.GetComponent<CurrentTransmitter>().ChangeState(gameObject);
        }
        if (up != null && up != incoming)
        {
            up.GetComponent<CurrentTransmitter>().ChangeState(gameObject);
        }
        if (down != null && down != incoming)
        {
            down.GetComponent<CurrentTransmitter>().ChangeState(gameObject);
        }
    }
}
