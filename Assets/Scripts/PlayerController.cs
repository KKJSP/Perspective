using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static int pos;
    RaycastHit hitInfo = new RaycastHit();
    int lookAngle;
    GameObject nextBlock;

    // Use this for initialization
    void Start () {
        nextBlock = null;
        lookAngle = 0;
        pos = 0;
    }

    // Update is called once per frame
    void Update () {
        MovePlayer(0);
	}

    //Teleporting Player on Each 2D View Switch
    public void MovePlayer(int toAngle)
    {
        lookAngle += toAngle;
        pos = (lookAngle/90)%4;
        while(pos < 0)
        {
            pos += 4;
        }
        Vector3 position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
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

        if (Physics.Raycast(camPos, dir, out hitInfo))
        {
            Vector3 newpos = hitInfo.collider.gameObject.transform.parent.position;
            newpos.y = newpos.y + 1;
            transform.position = newpos;
        }
    }

    //Checking If path exists
    public void CheckPath(Ray ray)
    {
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject finalQuadHit = hitInfo.transform.gameObject;
            if (CheckRight(finalQuadHit) != null)
            {
                StartCoroutine(TranslatePlayer(-1, CheckRight(finalQuadHit), finalQuadHit));
            }
            if (CheckLeft(finalQuadHit) != null)
            {
                StartCoroutine(TranslatePlayer(1, CheckLeft(finalQuadHit), finalQuadHit));
            }
        }
    }

    GameObject CheckRight(GameObject quadHit)
    {
        GameObject pathBlock = quadHit.transform.parent.gameObject;
        if (pathBlock.transform.position.y + 1 == transform.position.y && pathBlock.transform.position.x == transform.position.x && pathBlock.transform.position.z == transform.position.z)
        {
            return quadHit;
        }
        else if (quadHit.GetComponent<Configurer>().right != null)
        {
            return CheckRight(quadHit.GetComponent<Configurer>().right);
        }
        else
            return null;
        
    }
    GameObject CheckLeft(GameObject quadHit)
    {
        GameObject pathBlock = quadHit.transform.parent.gameObject;
        if (pathBlock.transform.position.y + 1 == transform.position.y && pathBlock.transform.position.x == transform.position.x && pathBlock.transform.position.z == transform.position.z)
        {
            return quadHit;
        }
        else if (quadHit.GetComponent<Configurer>().left != null)
        {
            return CheckLeft(quadHit.GetComponent<Configurer>().left);
        }
        else
            return null;
        
    }

    //Moving the player
    IEnumerator TranslatePlayer(int direction, GameObject initialBlock, GameObject finalBlock)
    {
        if(nextBlock == null)
        {
            nextBlock = initialBlock;
        }

        while (nextBlock != finalBlock)
        {
            if (direction == 1)
            {
                nextBlock = nextBlock.GetComponent<Configurer>().right;
            }
            else if (direction == -1)
            {
                nextBlock = nextBlock.GetComponent<Configurer>().left;
            }

            Vector3 newpos = new Vector3(nextBlock.transform.parent.position.x, nextBlock.transform.parent.position.y + 1, nextBlock.transform.parent.position.z);
            transform.position = newpos;
            yield return new WaitForSeconds(0.25f);

        }
        nextBlock = null;

    }

}
