using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 메인 카메라의 컨트롤러 입니다.
/// </summary>
public class CameraController : MonoBehaviour
{

    /// <summary>
    /// 타겟과의 거리
    /// </summary>
    public Vector3 distance;

    //바라볼 타겟
    public Transform target;

    public float lerpTime = 10f;

    public float minX = -360f, maxX = 360f;
    public float minY = -60f, maxY = 60f;

    public float sensitivity = 15f;

    float rotationX, rotationY;

    Transform tr;

    float h, v;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void Start() {
        //Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        rotationX = tr.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        tr.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            tr.position = Vector3.Lerp(tr.position, target.position + distance, Time.smoothDeltaTime * lerpTime);
            target.localEulerAngles = new Vector3(0, rotationX, 0);
        }
    }

}
