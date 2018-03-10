using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    static float camRotationSpeed, deltaPos;
    public static bool lock3D = false;

    bool changeAngle;

    float angle;

    GameObject mainCamera;

    public delegate void VoidIntDelegate(int i);
    public delegate void VoidRayDelegate(Ray ray);
    public static VoidIntDelegate Snapped;
    public static VoidRayDelegate OnClickFunctions;

#if UNITY_EDITOR

    bool drag = false;
    int mouseButtonReleaseBlurRange = 2;
    Vector3 Button0DownPoint;
    Vector3 Button0UpPoint;

#endif



    void Start()
    {
        changeAngle = true;
        angle = 0;
        camRotationSpeed = 10f;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Snapped(0);
    }



#if UNITY_EDITOR


    void Update()
    {

        //  CLICKS FOR MOUSE BUTTON

        if (Input.GetButtonDown("Fire1"))
        {
            Button0DownPoint = Input.mousePosition;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Button0UpPoint = Input.mousePosition;

            if (IsInRange(Button0DownPoint, Button0UpPoint))
            {
                Mouse0Click();  //Function where all actions associated with LMB clicks are performed
            }

            if (drag && changeAngle == false)
            {
                int finalAngle;
                finalAngle = Mathf.RoundToInt(angle / 90);
                finalAngle = finalAngle * 90;
                StartCoroutine(SnapRotation(finalAngle));    // Fixing Camera After Input End
                drag = false;
            }
        }


        //  DRAGS FOR MOUSE

        if (Input.GetButton("Fire1"))
        {
            if (Input.mousePosition != Button0DownPoint)
            {
                Mouse0Drag();//Function where all actions associated with LMB drag are performed
            }
        }


    }

    bool IsInRange(Vector2 v1, Vector2 v2)
    {
        if (Vector2.Distance(v1, v2) < mouseButtonReleaseBlurRange)
        {
            return true;
        }
        return false;
    }

    void Mouse0Click()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (OnClickFunctions != null)
            OnClickFunctions(ray);

    }

    void Mouse0Drag()
    {
        if (!lock3D)
        {
            deltaPos = Input.GetAxis("Mouse X");
            if (changeAngle)
            {
                mainCamera.transform.Rotate(25, 0, 0, Space.Self);
                mainCamera.transform.position.Set(mainCamera.transform.position.x, 6, mainCamera.transform.position.z);
                changeAngle = false;
            }
            mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, deltaPos * camRotationSpeed);
            angle += deltaPos * camRotationSpeed;
            drag = true;
        }
    }


#endif



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
        if(Snapped != null)
            Snapped(toAngle);
        changeAngle = true;
    }



}
