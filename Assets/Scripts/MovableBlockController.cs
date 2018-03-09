using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlockController : MonoBehaviour {

    public GameObject button;
    InteractionScript interactionScript;

    public Vector3 newPos;
    Vector3 oldPos;

	// Use this for initialization
	void Start () {
        oldPos = transform.position;
	}

    private void Awake()
    {
        interactionScript = button.GetComponent<InteractionScript>();
    }

    private void OnEnable()
    {
        interactionScript.ButtonHeld += MoveToNew;
        interactionScript.ButtonIdle += MoveToOld;
    }

    private void OnDisable()
    {
        interactionScript.ButtonHeld -= MoveToNew;
        interactionScript.ButtonIdle -= MoveToOld;
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
