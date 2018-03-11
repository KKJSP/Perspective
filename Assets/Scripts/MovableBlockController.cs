using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlockController : MonoBehaviour {

    bool isMoved = false;
    public bool largeAnim = false;

    int i, max = 5;
    public float maxDistanceDelta = 0.001f;

    GameObject mainCamera, objectAbove;

    public GameObject[] buttons;
    InteractionScript[] interactionScripts;

    public Vector3 newPos;
    Vector3 oldPos;

    IEnumerator coroutine;

    private void Awake()
    {
        interactionScripts = new InteractionScript[max];
        i = 0;
        foreach (GameObject button in buttons)
        {
            interactionScripts[i] = button.GetComponent<InteractionScript>();
            i++;
        }
        
    }

    // Use this for initialization
    void Start () {
        oldPos = transform.position;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }


    private void OnEnable()
    {
        for (int k = 0; k < i; k++)
        {
            interactionScripts[k].ObjectTouch += MoveToNew;
        }
    }

    private void OnDisable()
    {
        for (int k = 0; k < i; k++)
        {
            interactionScripts[k].ObjectTouch -= MoveToNew;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void MoveToNew()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        if (!isMoved)
        {
            coroutine = MoveBlock(newPos);
            StartCoroutine(coroutine);
            isMoved = true;
        }
        else if (isMoved)
        {
            coroutine = MoveBlock(oldPos);
            StartCoroutine(coroutine);
            isMoved = false;
        }
    }

    IEnumerator MoveBlock(Vector3 target)
    {
        Vector3 targetAbove = target;

        if (largeAnim)
        {
            mainCamera.transform.Rotate(25, 0, 0, Space.Self);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 2, mainCamera.transform.position.z);
        }

        if (objectAbove != null)
        {
            targetAbove.y += objectAbove.transform.position.y - transform.position.y;
        }

        while (transform.position != target)
        {
            if(objectAbove != null)
            {
                objectAbove.transform.position = Vector3.MoveTowards(objectAbove.transform.position, targetAbove, maxDistanceDelta);
            }
            transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);

            transform.Find("Front").GetComponent<Configurer>().ReConfigure();
            transform.Find("Back").GetComponent<Configurer>().ReConfigure();
            transform.Find("Left").GetComponent<Configurer>().ReConfigure();
            transform.Find("Right").GetComponent<Configurer>().ReConfigure();


            yield return new WaitForSeconds(0.01f);
        }

        if (largeAnim)
        {
            mainCamera.transform.Rotate(-25, 0, 0, Space.Self);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 2, mainCamera.transform.position.z);
        }

        transform.Find("Front").GetComponent<Configurer>().ReConfigure();
        transform.Find("Back").GetComponent<Configurer>().ReConfigure();
        transform.Find("Left").GetComponent<Configurer>().ReConfigure();
        transform.Find("Right").GetComponent<Configurer>().ReConfigure();

    }

    public void SetObjectAbove(GameObject value)
    {
        objectAbove = value;
    }


}
