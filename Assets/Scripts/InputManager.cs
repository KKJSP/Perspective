using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    static float pan, camRotationSpeed, deltaPos;
    GameObject mainCamera, player;
    float angle;

    // Use this for initialization
    void Start()
    {
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
            mainCamera.transform.Rotate(25, 0, 0, Space.Self);
            mainCamera.transform.position.Set(mainCamera.transform.position.x, 6, mainCamera.transform.position.z);
            pan = 3;            
        }
        if (pan==3)
        {
            mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, deltaPos*camRotationSpeed);
            angle += deltaPos*camRotationSpeed;
            //print(angle);
        }
        if(pan==2)
        {
            int finalAngle;
            finalAngle = Mathf.RoundToInt(angle / 90);
            finalAngle = finalAngle * 90;
            //print(finalAngle);
            pan = 0;
            StartCoroutine(SnapRotation(finalAngle));
            
        }


	}

    IEnumerator SnapRotation(int toAngle)
    {
        int rotDir = (int)Mathf.Sign(toAngle-angle);
        while (Mathf.Abs(angle - toAngle) > 6)
        {
            mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, 5*rotDir);
            angle += 5*rotDir;
            //print(angle);
            yield return null;
        }

        mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, toAngle - angle);
        angle = 0;
        mainCamera.transform.Rotate(-25, 0, 0, Space.Self);
        mainCamera.transform.position.Set(mainCamera.transform.position.x, -2, mainCamera.transform.position.z);
        player.GetComponent<PlayerController>().MovePlayer(toAngle);
    }

    

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

}
