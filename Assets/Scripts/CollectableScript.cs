using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour {

    int layer, parameter;

    Material black, ColourOne, ColourTwo;
    RaycastHit hitInfo = new RaycastHit();

    public delegate void VoidIntDelegate(int i);
    public static VoidIntDelegate Functions;

    private void OnEnable()
    {
        if (tag == "ColourPellet")
        {
            Functions += PlayerController.ChangePlayerLayer;
        }
        PlayerController.PlayerMovedUnit += CheckTaken;
    }

    private void OnDisable()
    {
        if (tag == "ColourPellet")
        {
            Functions -= PlayerController.ChangePlayerLayer;
        }
        PlayerController.PlayerMovedUnit -= CheckTaken;
    }

    void Start()
    {
        black = SettingsRuntime.GetMaterialBlack();
        ColourOne = SettingsRuntime.GetMaterialColourOne();
        ColourTwo = SettingsRuntime.GetMaterialColourTwo();

        layer = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));

        if (tag == "ColourPellet")
        {
            if (GetComponent<Renderer>().sharedMaterial == black)
            {
                SetParam(8);
            }
            else if (GetComponent<Renderer>().sharedMaterial == ColourOne)
            {
                SetParam(12);
            }
            else if (GetComponent<Renderer>().sharedMaterial == ColourTwo)
            {
                SetParam(13);
            }
        }
    }


    void CheckTaken(int pos)
    {
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

        if (Physics.Raycast(position, dir, out hitInfo, PlayerController.maxRayDist, layer) && hitInfo.transform == transform)
        {
            Collect();
        }
    }

    void Collect()
    {
        if (Functions != null)
        {
            Functions(parameter);
        }

        Destroy(gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collect();
        }
    }

    public void SetParam(int value)
    {
        parameter = value;
    }

}
