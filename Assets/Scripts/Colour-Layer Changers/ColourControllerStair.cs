using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourControllerStair : MonoBehaviour {

    Material[] meshes = new Material[5];
    int[] layers = new int[4];

    GameObject steps, flat, R2L, L2R;
    Renderer stepsRend, flatRend, R2LRend, L2RRend;

    void GetMeshes()
    {
        steps = transform.Find("Steps").gameObject;
        stepsRend = steps.GetComponent<Renderer>();
        flat = transform.Find("Flat").gameObject;
        flatRend = flat.GetComponent<Renderer>();
        R2L = transform.Find("R2L").gameObject;
        R2LRend = R2L.GetComponent<Renderer>();
        L2R = transform.Find("L2R").gameObject;
        L2RRend = L2R.GetComponent<Renderer>();
    }

    public void ApplyMaterials(int[] selectedindex)
    {
        GetMeshes();

        for (int i = 0; i < 4; i++)
        {
            switch (selectedindex[i])
            {
                case 0:
                    meshes[i] = Settings.GetMaterialBlack();
                    layers[i] = LayerMask.NameToLayer("PathBlock");
                    break;
                case 1:
                    meshes[i] = Settings.GetMaterialColourOne();
                    layers[i] = LayerMask.NameToLayer("ColourOnePath");
                    break;
                case 2:
                    meshes[i] = Settings.GetMaterialColourTwo();
                    layers[i] = LayerMask.NameToLayer("ColourTwoPath");
                    break;
                default:
                    Debug.LogError("Unrecognized Option");
                    break;
            }
        }

        stepsRend.material = meshes[0];
        steps.layer = layers[0];
        flatRend.material = meshes[1];
        flat.layer = layers[1];
        R2LRend.material = meshes[2];
        R2L.layer = layers[2];
        L2RRend.material = meshes[3];
        L2R.layer = layers[3];
        print("Applied Materials");
    }
}
