using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsRuntime : MonoBehaviour {

    public Material Black, Yellow, Violet, PlayerDefaultMaterial;
    private static Material BlackAccess, YellowAccess, VioletAccess, DefaultPlayer;

    private void Start()
    {
        BlackAccess = Black;
        YellowAccess = Yellow;
        VioletAccess = Violet;
        DefaultPlayer = PlayerDefaultMaterial;
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

    public static Material GetDefaultPlayer()
    {
        return DefaultPlayer;
    }
}
