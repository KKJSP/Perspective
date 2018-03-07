using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    static float pan, camRotationSpeed, deltaPos;
    GameObject mainCamera, player;
    float angle;
    bool changeAngle;
    static Vector3 pos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        changeAngle = true;
        angle = 0;
        pan = 0;
        camRotationSpeed = 10f;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if(pan == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            player.GetComponent<PlayerController>().CheckPath(ray);
            pan = 0;
        }
        if (pan==3)
        {
            if (changeAngle)
            {
                mainCamera.transform.Rotate(25, 0, 0, Space.Self);
                mainCamera.transform.position.Set(mainCamera.transform.position.x, 6, mainCamera.transform.position.z);
                changeAngle = false;
            }
            mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, deltaPos*camRotationSpeed);
            angle += deltaPos*camRotationSpeed;
        }
        if(pan==2 && changeAngle==false)
        {
            int finalAngle;
            finalAngle = Mathf.RoundToInt(angle / 90);
            finalAngle = finalAngle * 90;
            pan = 0;
            StartCoroutine(SnapRotation(finalAngle));    // Fixing Camera After Input End

        }


	}

    IEnumerator SnapRotation(int toAngle)
    {
        int rotDir = (int)Mathf.Sign(toAngle-angle);
        while (Mathf.Abs(angle - toAngle) > 6)
        {
            mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, 5*rotDir);
            angle += 5*rotDir;
            yield return null;
        }

        mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, toAngle - angle);
        angle = 0;
        mainCamera.transform.Rotate(-25, 0, 0, Space.Self);
        mainCamera.transform.position.Set(mainCamera.transform.position.x, -2, mainCamera.transform.position.z);
        player.GetComponent<PlayerController>().MovePlayer(toAngle);
        changeAngle = true;
    }

    
    // Setter Functions
    public static void SetPan(float value)
    {
        pan = value;
    }

    public static void SetCamRotationSpeed(float value)
    {
        camRotationSpeed = value;
    }

    public static void SetDeltaPos(float value)
    {
        deltaPos = value;
    }

    public static void SetPos(Vector3 value)
    {
        pos = value;
    }

}
