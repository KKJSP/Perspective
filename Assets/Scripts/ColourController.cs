using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColourController : MonoBehaviour {

    Material[] meshes = new Material[5];

    Renderer frontRend, backRend, leftRend, rightRend, topRend;

    void GetMeshes()
    {
        frontRend = transform.Find("Front").GetComponent<Renderer>();
        backRend = transform.Find("Back").GetComponent<Renderer>();
        leftRend = transform.Find("Left").GetComponent<Renderer>();
        rightRend = transform.Find("Right").GetComponent<Renderer>();
        topRend = transform.Find("Top").GetComponent<Renderer>();
    }

    public void ApplyMaterials(int[] selectedindex)
    {
        GetMeshes();

        for(int i = 0; i < 5; i++)
        {
            switch(selectedindex[i])
            {
                case 0:
                    meshes[i] = Settings.GetMaterialBlack();
                    break;
                case 1:
                    meshes[i] = Settings.GetMaterialYellow();
                    break;
                case 2:
                    meshes[i] = Settings.GetMaterialViolet();
                    break;
                default:
                    Debug.LogError("Unrecognized Option");
                    break;
            }
        }

        frontRend.material = meshes[0];
        backRend.material = meshes[1];
        rightRend.material = meshes[2];
        leftRend.material = meshes[3];
        topRend.material = meshes[4];
        print("Applied Materials");
    }
}
