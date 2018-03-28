using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColourController : MonoBehaviour {

    Material[] meshes = new Material[5];
    int[] layers = new int[5];

    GameObject front, back, left, right, top;
    Renderer frontRend, backRend, leftRend, rightRend, topRend;

    void GetMeshes()
    {
        front = transform.Find("Front").gameObject;
        frontRend = front.GetComponent<Renderer>();
        back = transform.Find("Back").gameObject;
        backRend = back.GetComponent<Renderer>();
        left = transform.Find("Left").gameObject;
        leftRend = left.GetComponent<Renderer>();
        right = transform.Find("Right").gameObject;
        rightRend = right.GetComponent<Renderer>();
        top = transform.Find("Top").gameObject;
        topRend = top.GetComponent<Renderer>();
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
                    layers[i] = LayerMask.NameToLayer("PathBlock");
                    break;
                case 1:
                    meshes[i] = Settings.GetMaterialYellow();
                    layers[i] = LayerMask.NameToLayer("YellowPath");
                    break;
                case 2:
                    meshes[i] = Settings.GetMaterialViolet();
                    layers[i] = LayerMask.NameToLayer("VioletPath");
                    break;
                default:
                    Debug.LogError("Unrecognized Option");
                    break;
            }
        }

        frontRend.material = meshes[0];
        front.layer = layers[0];
        backRend.material = meshes[1];
        back.layer = layers[1];
        rightRend.material = meshes[2];
        right.layer = layers[2];
        leftRend.material = meshes[3];
        left.layer = layers[3];
        topRend.material = meshes[4];
        top.layer = layers[4];
        print("Applied Materials");
    }
}
