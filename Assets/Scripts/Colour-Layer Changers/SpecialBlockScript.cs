using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBlockScript : MonoBehaviour {

    GameObject front, back, left, right;

    private void OnEnable()
    {
        PlayerController.PlayerChangedLayer += ChangeLayers;
    }

    private void OnDisable()
    {
        PlayerController.PlayerChangedLayer -= ChangeLayers;
    }

    // Use this for initialization
    void Start () {
        front = transform.Find("Front").gameObject;
        back = transform.Find("Back").gameObject;
        right = transform.Find("Right").gameObject;
        left = transform.Find("Left").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeLayers(int value)
    {
        front.layer = value;
        back.layer = value;
        right.layer = value;
        left.layer = value;
    }
}
