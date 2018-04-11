using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigAll : MonoBehaviour {

    static Configurer[] configurers;

	// Use this for initialization
	void Start () {
        configurers = (Configurer[])FindObjectsOfType(typeof(Configurer));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ConfigQuads()
    {
        foreach(Configurer config in configurers)
        {
            config.SetNull();
        }

        foreach (Configurer config in configurers)
        {
            config.Configure();
        }
    }
}
