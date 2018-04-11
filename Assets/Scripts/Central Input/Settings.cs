using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Settings : MonoBehaviour {

    public Material materialBlack, materialColourOne, materialColourTwo;

    private static Material BlackAccess, ColourOneAccess, ColourTwoAccess;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    private void Start()
    {
        BlackAccess = materialBlack;
        ColourOneAccess = materialColourOne;
        ColourTwoAccess = materialColourTwo;
    }

    public void SaveChanges() {
        BlackAccess = materialBlack;
        ColourOneAccess = materialColourOne;
        ColourTwoAccess = materialColourTwo;
        print("Saved");
    }

    public static Material GetMaterialBlack()
    {
        return BlackAccess;
    }

    public static Material GetMaterialColourOne()
    {
        return ColourOneAccess;
    }

    public static Material GetMaterialColourTwo()
    {
        return ColourTwoAccess;
    }

}
