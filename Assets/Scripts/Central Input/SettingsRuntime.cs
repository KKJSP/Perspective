using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsRuntime : MonoBehaviour {

    public Material Black, ColourOne, ColourTwo, PlayerDefaultMaterial;
    private static Material BlackAccess, ColourOneAccess, ColourTwoAccess, DefaultPlayer;

    private void Awake()
    {
        BlackAccess = Black;
        ColourOneAccess = ColourOne;
        ColourTwoAccess = ColourTwo;
        DefaultPlayer = PlayerDefaultMaterial;
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

    public static Material GetDefaultPlayer()
    {
        return DefaultPlayer;
    }
}
