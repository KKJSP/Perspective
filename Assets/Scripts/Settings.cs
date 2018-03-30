using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Settings : MonoBehaviour {

    public Material materialBlack, materialYellow, materialViolet;

    private static Material BlackAccess, YellowAccess, VioletAccess;

    public void SaveChanges() {
        BlackAccess = materialBlack;
        YellowAccess = materialYellow;
        VioletAccess = materialViolet;
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

}
