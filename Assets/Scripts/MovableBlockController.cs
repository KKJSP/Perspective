using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlockController : MonoBehaviour {

    int i, max = 5;

    public GameObject[] buttons;
    InteractionScript[] interactionScripts;

    public Vector3 newPos;
    Vector3 oldPos;


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
	}


    private void OnEnable()
    {
        for (int k = 0; k < i; k++)
        {
            interactionScripts[k].ButtonHeld += MoveToNew;
            interactionScripts[k].ButtonIdle += MoveToOld;
        }
    }

    private void OnDisable()
    {
        for (int k = 0; k < i; k++)
        {
            interactionScripts[k].ButtonHeld -= MoveToNew;
            interactionScripts[k].ButtonIdle -= MoveToOld;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void MoveToNew()
    {
        transform.position = newPos;
    }

    void MoveToOld()
    {
        transform.position = oldPos;
    }


}
