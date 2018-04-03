using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColourPellet : MonoBehaviour {


    public void ApplyMaterials(int selectedindex)
    {
        switch (selectedindex)
        {
            case 0:
                GetComponent<Renderer>().material = Settings.GetMaterialBlack();
                break;
            case 1:
                GetComponent<Renderer>().material = Settings.GetMaterialColourOne();
                break;
            case 2:
                GetComponent<Renderer>().material = Settings.GetMaterialColourTwo();
                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
        print("Saved Colour");
    }
    
}
