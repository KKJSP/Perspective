using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Settings : MonoBehaviour {

    public Material materialBlack, materialYellow, materialViolet, defaultPlayer;

    private static Material BlackAccess, YellowAccess, VioletAccess, defaultPlayerAccess;

    public void SaveChanges() {
        BlackAccess = materialBlack;
        YellowAccess = materialYellow;
        VioletAccess = materialViolet;
        defaultPlayerAccess = defaultPlayer;
        print("Saved");
    }

    public static Material GetMaterialBlack()
    {
        return BlackAccess;
    }

    public static Material GetMaterialYellow()
    {
        return YellowAccess;
    }

    public static Material GetMaterialViolet()
    {
        return VioletAccess;
    }

    public static Material GetMaterialPlayerDefault()
    {
        return defaultPlayerAccess;
    }
}
