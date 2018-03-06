using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static int pos;
    RaycastHit hitInfo = new RaycastHit();
    int lookAngle;

	// Use this for initialization
	void Start () {
        lookAngle = 0;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void MovePlayer(int toAngle)
    {
        lookAngle += toAngle;
        pos = (lookAngle/90)%4;
        while(pos < 0)
        {
            pos += 4;
        }
        Vector3 position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        //print(position);
        Vector3 camPos = Vector3.zero;
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
        //print(camPos);
        Vector3 dir = position - camPos;

        if (Physics.Raycast(camPos, dir, out hitInfo))
        {
            //Debug.Log(hitInfo.collider.name + ", " + hitInfo.collider.tag);
            Vector3 newpos = hitInfo.collider.gameObject.transform.parent.position;
            //print(newpos);
            newpos.y = newpos.y + 1;
            transform.position = newpos;
        }
    }

}
