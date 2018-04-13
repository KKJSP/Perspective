using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    static float camRotationSpeed, deltaPos, maxDistanceDelta = 0.1f;
    public static bool lock3D = false, canDrag = true, canMove = true, canFall = true;

    bool changeAngle;
    static int savedAngle;
    float angle;

    Vector3 pos;

    RaycastHit hitInfo = new RaycastHit();

    IEnumerator coroutine;

    public static GameObject mainCamera;

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
        pos = mainCamera.transform.position;
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
                canFall = true;
            }
        }


        //  DRAGS FOR MOUSE

        if (Input.GetButton("Fire1"))
        {
            if (Input.mousePosition != Button0DownPoint)
            {
                Mouse0Drag();       //Function where all actions associated with LMB drag are performed
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
        if (canMove)
        {
            if (OnClickFunctions != null)
                OnClickFunctions(ray);
        }

    }

    void Mouse0Drag()
    {
        if (!lock3D)
        {
            canFall = false;
            deltaPos = Input.GetAxis("Mouse X");
            if (changeAngle)
            {
                canDrag = false;
                StartCoroutine(SwitchView("3D"));
                changeAngle = false;
            }
            if (canDrag)
            {
                mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, deltaPos * camRotationSpeed);
                angle += deltaPos * camRotationSpeed;
                drag = true;
            }
        }
    }


#endif



    IEnumerator SnapRotation(int toAngle)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        if (CheckPerspective(toAngle))
        {
            int rotDir = (int)Mathf.Sign(toAngle - angle);
            while (Mathf.Abs(angle - toAngle) > 6)
            {
                mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, 2 * rotDir);
                angle += 2 * rotDir;
                yield return null;
            }

            mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, toAngle - angle);
            StartCoroutine(SwitchView("2D"));
            angle = toAngle;
            changeAngle = true;
        }
        else
        {
            coroutine = CameraShake(toAngle);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator SwitchView(string view)
    {
        float rotX;
        pos = mainCamera.transform.position;

        if (view == "2D")
        {
            rotX = 0;
            pos.y -= 2;
        }
        else if (view == "3D")
        {
            rotX = 25;
            pos.y += 2;
        }
        else
        {
            rotX = mainCamera.transform.localEulerAngles.x;
        }

        while(Mathf.Abs(mainCamera.transform.localEulerAngles.x - rotX) > 0.1 && Vector3.Distance(mainCamera.transform.position,pos) > 0.1)
        {
            mainCamera.transform.Rotate(Mathf.Sign(rotX - mainCamera.transform.localEulerAngles.x), 0, 0, Space.Self);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, pos, maxDistanceDelta);
            yield return new WaitForSeconds(0.000001f);
        }

        mainCamera.transform.localEulerAngles.Set(rotX, 0, 0);
        mainCamera.transform.position = pos;

        if(view == "3D")
        {
            canDrag = true;
            canMove = false;
        }

        if (Snapped != null && view == "2D")
        {
            CurrentEnabler.EnableCurrents(Mathf.RoundToInt(angle));
            Snapped(Mathf.RoundToInt(angle));
            savedAngle = Mathf.RoundToInt(angle);
            angle = 0;
            canMove = true;
        }
    }

    IEnumerator CameraShake(int toAngle)
    {
        int count=0;

        int rotDir = (int)Mathf.Sign(toAngle - angle);
        while (Mathf.Abs(angle - toAngle) > 6)
        {
            mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, 2 * rotDir);
            angle += 2 * rotDir;
            count++;
            yield return null;
        }

        mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, toAngle - angle);

        //Code To Shake Camera Yet To Be Written ***********************

        mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, angle - toAngle);

        while(count>0)
        {
            mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, -2 * rotDir);
            angle += -2 * rotDir;
            count--;
            yield return null;
        }


    }

    bool CheckPerspective(int viewAngle)
    {
        int view;
        int totalAngle = PlayerController.lookAngle + viewAngle;
        view = (totalAngle / 90) % 4;
        while (view < 0)
        {
            view += 4;
        }

        Vector3 position = PlayerController.player.transform.position;
        Vector3 camPos = Vector3.zero;

        //Setting Raycast Origin and Directions
        if (view == 0)
        {
            camPos = position + new Vector3(0, 0, -20);
        }
        else if (view == 1)
        {
            camPos = position + new Vector3(-20, 0, 0);
        }
        else if (view == 3)
        {
            camPos = position + new Vector3(20, 0, 0);
        }
        else if (view == 2)
        {
            camPos = position + new Vector3(0, 0, 20);
        }
        Vector3 dir = position - camPos;

        int blockLayers = LayerMask.GetMask("PathBlock","Switch","ColourOnePath","ColourTwoPath");
        if (Physics.Raycast(camPos, dir, out hitInfo, PlayerController.maxRayDist, blockLayers))
        {
            if(hitInfo.transform.gameObject == PlayerController.player)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public static int GetAngle()
    {
        return savedAngle;
    }

}
