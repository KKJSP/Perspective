using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static int pos, prev_pos, layer;
    public static float maxRayDist = 100f;
    public static GameObject player;
    int lookAngle;

    RaycastHit hitInfo = new RaycastHit();

    GameObject nextBlock;

    public delegate void VoidIntDelegate(int i);
    public static VoidIntDelegate PlayerMovedUnit;

    IEnumerator coroutine;

    private void OnEnable()
    {
        InputManager.Snapped += MovePlayer;
        InputManager.OnClickFunctions += CheckPath;
    }

    private void OnDisable()
    {
        InputManager.Snapped -= MovePlayer;
        InputManager.OnClickFunctions -= CheckPath;
    }

    private void Awake()
    {
        layer = LayerMask.GetMask("PathBlock");
        player = gameObject;
    }


    // Use this for initialization
    void Start () {
        nextBlock = null;
        lookAngle = 0;
        pos = 0;
        prev_pos = -1;
    }

    // Update is called once per frame
    void Update () {

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

        if(pos == prev_pos)
        {
            return;
        }
        prev_pos = pos;

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

        if (Physics.Raycast(camPos, dir, out hitInfo, maxRayDist, layer))
        {
            Vector3 newpos = hitInfo.collider.gameObject.transform.parent.position;
            newpos.y = newpos.y + 1;
            transform.position = newpos;
            if(PlayerMovedUnit!=null)
            {
                PlayerMovedUnit(pos);
            }
        }
    }

    //Checking If path exists
    public void CheckPath(Ray ray)
    {
        if (Physics.Raycast(ray, out hitInfo, maxRayDist, layer))
        {
            GameObject finalQuadHit = hitInfo.transform.gameObject;
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            if (CheckRight(finalQuadHit) != null)
            {
                coroutine = TranslatePlayer(-1, CheckRight(finalQuadHit), finalQuadHit);
                StartCoroutine(coroutine);
            }
            if (CheckLeft(finalQuadHit) != null)
            {
                coroutine = TranslatePlayer(1, CheckLeft(finalQuadHit), finalQuadHit);
                StartCoroutine(coroutine);
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
        InputManager.lock3D = true;

        if(nextBlock == null)
        {
            nextBlock = initialBlock;
        }

        while (nextBlock != finalBlock)
        {
            if (direction == 1)
            {
                if (nextBlock.GetComponent<Configurer>().right == null)
                    break;

                nextBlock = nextBlock.GetComponent<Configurer>().right;
            }
            else if (direction == -1)
            {
                if (nextBlock.GetComponent<Configurer>().left == null)
                    break;
                nextBlock = nextBlock.GetComponent<Configurer>().left;
            }

            Vector3 newpos = new Vector3(nextBlock.transform.parent.position.x, nextBlock.transform.parent.position.y + 1, nextBlock.transform.parent.position.z);
            transform.position = newpos;
            if(PlayerMovedUnit != null)
            {
                PlayerMovedUnit(pos);
            }

            if (nextBlock == finalBlock)
                break;

            yield return new WaitForSeconds(0.25f);

        }
        nextBlock = null;
        InputManager.lock3D = false;

    }

    public static void ChangePlayerLayer(int value)
    {
        /*switch (value)
        {
            case 8:
                {
                    player.GetComponent<Renderer>().material = Settings.GetMaterialPlayerDefault();
                    break;
                }
            case 12:
                {
                    player.GetComponent<Renderer>().material = Settings.GetMaterialYellow();
                    break;
                }
            case 13:
                {
                    player.GetComponent<Renderer>().material = Settings.GetMaterialViolet();
                    break;
                }

        }
        */

        if (value == 8)
        {
            layer = LayerMask.GetMask("PathBlock");
        }
        else
        {
            layer = LayerMask.GetMask("PathBlock", LayerMask.LayerToName(value));
        }
    }

}
