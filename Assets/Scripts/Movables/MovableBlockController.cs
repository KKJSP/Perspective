using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlockController : MonoBehaviour {

    bool isMoved = false;
    public bool largeAnim = false;

    float maxDistanceDelta = 0.001f;
    float maxRotationDelta = 1f;
    public float time;

    GameObject mainCamera, objectAbove;

    public InteractionScript[] interactionScripts;

    public Vector3 newPos;
    public int newRotY;
    Vector3 oldPos;
    float rotateBy;

    IEnumerator coroutine1, coroutine2;

    private void Awake()
    {
        objectAbove = null;
    }

    // Use this for initialization
    void Start () {
        oldPos = transform.position;
        rotateBy = newRotY - transform.localEulerAngles.y; 
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }


    private void OnEnable()
    {
        foreach(InteractionScript interactionScript in interactionScripts)
        {
            interactionScript.ObjectTouch += MoveToNew;
        }
    }

    private void OnDisable()
    {
        foreach (InteractionScript interactionScript in interactionScripts)
        {
            interactionScript.ObjectTouch -= MoveToNew;
        }
    
    }

    // Update is called once per frame
    void Update () {
		
	}

    void MoveToNew()
    {
        maxDistanceDelta = Vector3.Distance(newPos, oldPos) / time;
        maxRotationDelta = rotateBy / time;

        if (coroutine1 != null)
        {
            StopCoroutine(coroutine1);
        }
        if (coroutine2 != null)
        {
            StopCoroutine(coroutine2);
        }
        if (!isMoved)
        {
            coroutine1 = MoveBlock(newPos);
            coroutine2 = RotateBlock(rotateBy);
            StartCoroutine(coroutine1);
            StartCoroutine(coroutine2);
            isMoved = true;
        }
        else if (isMoved)
        {
            coroutine1 = MoveBlock(oldPos);
            coroutine2 = RotateBlock(-rotateBy);
            StartCoroutine(coroutine1);
            StartCoroutine(coroutine2);
            isMoved = false;
        }
    }

    IEnumerator MoveBlock(Vector3 target)
    {
        if (objectAbove == null)
        {
            transform.Find("DetectObject").GetComponent<DetectObjects>().CheckFirst();
        }
        bool gotIt = false; 
        gotIt = transform.Find("DetectObject").GetComponent<DetectObjects>().CheckObject(gotIt);

        Vector3 targetAbove = target;

        if (largeAnim)
        {
            mainCamera.transform.Rotate(25, 0, 0, Space.Self);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 2, mainCamera.transform.position.z);
            InputManager.lock3D = true;
            InputManager.canMove = false;
        }
        
        while (transform.position != target)
        {
            gotIt = transform.Find("DetectObject").GetComponent<DetectObjects>().CheckObject(gotIt);

            transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);

            if (objectAbove != null)
            {
                targetAbove = transform.position;
                targetAbove.y += 1;
                objectAbove.transform.position = targetAbove;
            }

            PlayerController.CheckPlayerDeath();
            yield return new WaitForSeconds(0.01f);
        }
        if (objectAbove != null)
        {
            targetAbove = transform.position;
            targetAbove.y += 1;
            objectAbove.transform.position = targetAbove;
            objectAbove = null;
        }
        //PlayerController.PlayerMovedUnit(PlayerController.pos);


        if (largeAnim)
        {
            mainCamera.transform.Rotate(-25, 0, 0, Space.Self);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 2, mainCamera.transform.position.z);
            InputManager.lock3D = false;
            InputManager.canMove = true;
        }


        ConfigAll.ConfigQuads();
        ConfigCurrents();
        


    }


    IEnumerator RotateBlock(float angle)
    {
        int rotDir = (int)Mathf.Sign(angle);
        while (Mathf.Abs(angle) > maxRotationDelta)
        {
            transform.Rotate(0f, maxRotationDelta*rotDir, 0f);
            angle -= maxRotationDelta * rotDir;
            yield return new WaitForSeconds(0.01f);
        }

        transform.Rotate(0f, angle, 0f);

        ConfigAll.ConfigQuads();
        ConfigCurrents();
        
    }


    public void SetObjectAbove(GameObject value)
    {
        objectAbove = value;
    }

    void ConfigCurrents()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "Current")
            {
                child.GetComponent<CurrentTransmitter>().ReConfigure();
            }
        }
    }

}
