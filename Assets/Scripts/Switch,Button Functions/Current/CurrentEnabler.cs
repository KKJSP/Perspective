using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEnabler : MonoBehaviour {

    public static int pos, prev_pos, lookAngle;

    public static GameObject[] currents, switches;

    /*

    private void OnEnable()
    {
        InputManager.Snapped += EnableCurrents;
    }

    private void OnDisable()
    {
        InputManager.Snapped -= EnableCurrents;
    }

    */

    // Use this for initialization
    void Start () {
        lookAngle = pos = 0;
        prev_pos = -1;
        currents = GameObject.FindGameObjectsWithTag("Current");
        switches = GameObject.FindGameObjectsWithTag("Switch");
        EnableCurrents(lookAngle);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void EnableCurrents(int toAngle)
    {
        lookAngle += toAngle;
        pos = (lookAngle / 90) % 4;
        while (pos < 0)
        {
            pos += 4;
        }

        if (pos == prev_pos)
        {
            return;
        }
        prev_pos = pos;

        foreach (GameObject current in currents)
        {
            if(Mathf.RoundToInt(current.transform.localEulerAngles.y/90) == pos)
            {
                current.GetComponent<CurrentTransmitter>().Configure();
            }
            else
            {
                current.GetComponent<CurrentTransmitter>().DeConfigure();
            }
        }


        foreach(GameObject _switch in switches)
        {
            if (Mathf.RoundToInt(_switch.transform.localEulerAngles.y / 90) == pos)
            {
                _switch.GetComponent<CurrentTransmitter>().Configure();
            }
            else
            {
                _switch.GetComponent<CurrentTransmitter>().DeConfigure();
            }
        }
    }

}
