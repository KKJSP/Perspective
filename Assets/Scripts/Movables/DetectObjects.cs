using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour {

    int layer;
    RaycastHit hitInfo = new RaycastHit();

	// Use this for initialization
	void Start () {
        layer = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool CheckObject(bool gotIt)
    {
        int pos = PlayerController.pos;
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

        if (Physics.Raycast(position, dir, out hitInfo, PlayerController.maxRayDist, layer) && hitInfo.transform == transform && !gotIt)
        {
            Vector3 newpos = hitInfo.collider.gameObject.transform.parent.position;
            transform.position = newpos;
            if (PlayerController.PlayerMovedUnit != null)
            {
                PlayerController.PlayerMovedUnit(pos);
            }
            return !gotIt;
        }

        return gotIt;
    }

    public void CheckFirst()
    {
        if (transform.parent.tag == "MovableBlock" && PlayerController.player.transform.position == transform.position)
        {
            transform.parent.GetComponent<MovableBlockController>().SetObjectAbove(PlayerController.player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.tag == "MovableBlock" && other.transform.tag == "Player")
        {
            transform.parent.GetComponent<MovableBlockController>().SetObjectAbove(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (transform.parent.tag == "MovableBlock" && other.transform.tag == "Player")
        {
            transform.parent.GetComponent<MovableBlockController>().SetObjectAbove(null);
        }
    }
}
