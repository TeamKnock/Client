using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 메인 카메라의 컨트롤러 입니다.
/// </summary>
public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    /// <summary>
    /// 타겟과의 거리
    /// </summary>
    public Vector3 distance;

    //바라볼 타겟
    public Transform target;
    public Transform hand;

    public float lerpTime = 10f;

    public float minX = -360f, maxX = 360f;
    public float minY = -60f, maxY = 60f;

    public float sensitivity = 15f;

    float rotationX, rotationY;

    Transform tr;

    float h, v;

    void Awake()
    {
        instance = this;
        tr = GetComponent<Transform>();
    }

    void Start()
    {
        SetCursor();
    }


    void Update()
    {
        if (!Cursor.visible)
        {

            rotationX = tr.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            tr.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursor();
        }

    }

    void SetCursor()
    {
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    void LateUpdate()
    {
        if (target != null)
        {
            tr.position = target.position + distance;
            target.localEulerAngles = Vector3.Lerp(tr.localEulerAngles, new Vector3(0, rotationX, 0), Time.smoothDeltaTime * lerpTime);
            hand.localEulerAngles = Vector3.Lerp(tr.localEulerAngles, new Vector3(-rotationY, 0, 0), Time.smoothDeltaTime * lerpTime);
        }
    }

}
