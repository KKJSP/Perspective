using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {


    float maxRayDist = 100f;

    int layer;

    RaycastHit hitInfo = new RaycastHit();


    private void OnEnable()
    {
        InputManager.OnClickFunctions += SwitchTouch;
    }
    private void OnDisable()
    {
        InputManager.OnClickFunctions -= SwitchTouch;
    }

    private void Awake()
    {
        layer = LayerMask.GetMask("Switch");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwitchTouch(Ray ray)
    {
        if(Physics.Raycast(ray, out hitInfo, maxRayDist, layer))
        if (hitInfo.transform == transform)
        {
            this.GetComponent<InteractionScript>().ButtonSwitchTouch();
        }

    }
}


