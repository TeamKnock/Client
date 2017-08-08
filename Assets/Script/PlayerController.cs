using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 조종하는 컨트롤러 클래스입니다.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 클라이언트의 플레이어 인가?
    /// </summary>
    public bool isLocal = false;
    /// <summary>
    /// 조종가능한 상태인가?
    /// </summary>
    public bool isInput = false;

    /// <summary>
    /// 착지 상태인가?
    /// </summary>
    public bool isGround = false;

    public bool isGroundCheck = true;

    /// <summary>
    /// 유저 정보
    /// </summary>
    public User user;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 5f;
    /// <summary>
    /// 회전 속도
    /// </summary>
    public float rotateSpeed = 10f;

    public float jumpPower = 20f;

    /// <summary>
    /// 서버와의 상태 보간 값
    /// </summary>
    [SerializeField]
    float moveLerp = 20f, rotateLerp = 20f;


    Vector3 oldPos;

    /// <summary>
    /// 서버에서 받는 이동 값
    /// </summary>
    public Vector3 currentPos, currentVel;

    Quaternion oldRot;

    /// <summary>
    /// 서버에서 받는 회전 값
    /// </summary>
    public Quaternion currentRot;

    float h, v;

    /// <summary>
    /// 지연 시간 값을 구합니다.
    /// </summary>
    [SerializeField]
    float syncTime;

    //캐싱
    Transform tr;
    Rigidbody ri;

    public WeaponController weaponController;

    /// <summary>
    /// 클라이언트 유저를 세팅합니다.
    /// </summary>
    /// <param name="_isLocal"></param>
    public void SetLocal(bool _isLocal)
    {
        isLocal = _isLocal;
        ri.useGravity = _isLocal;
    }

    /// <summary>
    /// 조종 가능한 상태를 세팅합니다.
    /// </summary>
    /// <param name="_isInput"></param>
    public void SetInput(bool _isInput)
    {
        isInput = _isInput;
    }


    private void Awake()
    {
        //컴포넌트 캐싱
        tr = GetComponent<Transform>();
        ri = GetComponent<Rigidbody>();

        //초기 값 세팅
        oldPos = tr.position;
        oldRot = tr.rotation;

        weaponController.Set();
    }


    void Update()
    {
        //클라이언트 유저이고 조종 가능한 상태인가?
        if (isLocal && isInput)
        {
            //에디터 전용 움직임
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            if (Input.GetMouseButtonDown(0)) {
                weaponController.Attack();
            }

        }
        else
        {
            //클라이언트가 아닌 경우
            syncTime += Time.deltaTime;
            if (currentVel.normalized != Vector3.zero)
            {
                // 달리는 애니메이션 실행

            }
            else
            {
                //대기 상태 애니메이션 실행

            }

        }

    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        if (isGroundCheck)
            GroundCheck();
    }

    /// <summary>
    /// 플레이어를 이동합니다.
    /// </summary>
    void Move()
    {
        //클라이언트 유저
        if (isLocal)
        {
            Vector3 movePos = ((tr.forward * v * moveSpeed) + (tr.right * h * moveSpeed));
            movePos.y = ri.velocity.y;

            //에디터 전용 움직임
            ri.velocity = movePos;

            SendPosition();
        }
        else
        {
            //현재 위치 = 이전 위치 + ( 속도 * 시간 ) + ( 1 / 2 * 가속도 * 시간 ^ 2 )
            tr.position = Vector3.Lerp(tr.position, currentPos + ((currentVel * syncTime) + (0.5f * currentVel * syncTime * syncTime)), Time.smoothDeltaTime * moveLerp);
        }
    }

    /// <summary>
    /// 플레이어를 회전합니다.
    /// </summary>
    void Rotate()
    {
        //클라이언트 유저
        if (isLocal)
        {
            Vector3 targetDir = Vector3.zero;
            Vector3 pos = new Vector3(h, 0, v).normalized;

            if (pos.sqrMagnitude > 0.1f)
                targetDir = pos.normalized;

            if (targetDir != Vector3.zero)
                tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * rotateSpeed);

            SendRotation();
        }
        else
        {
            tr.rotation = Quaternion.Lerp(tr.rotation, currentRot, Time.smoothDeltaTime * rotateLerp);
        }
    }

    void Jump() {
        if (!isGround)
            return;
        
        ri.velocity = new Vector3(ri.velocity.x, jumpPower, ri.velocity.z);

        isGround = false;

        if (groundCheckDelay != null)
        {
            StopCoroutine(groundCheckDelay);
        }

        groundCheckDelay = StartCoroutine(GroundCheckDelay());

    }

    void GroundCheck() {
        RaycastHit hit;
        if (Physics.Raycast(tr.position, -tr.up, out hit, 1.5f)) {
            isGround = true;
        }
    }

    Coroutine groundCheckDelay = null;

    IEnumerator GroundCheckDelay() {
        isGroundCheck = false;
        yield return new WaitForSeconds(0.1f);
        isGroundCheck = true;
    }

    /// <summary>
    /// 플레이어의 위치 정보를 서버에 전송합니다.
    /// </summary>
    public void SendPosition()
    {
        if (oldPos != tr.position)
        {
            oldPos = tr.position;
            NetworkManager.instance.EmitMove(tr.position, ri.velocity);
        }
    }

    /// <summary>
    /// 플레이어의 회전 정보를 서버에 전송합니다.
    /// </summary>
    public void SendRotation()
    {
        if (oldRot != tr.rotation)
        {
            oldRot = tr.rotation;
            NetworkManager.instance.EmitRotate(tr.rotation);
        }
    }

    /// <summary>
    /// 위치 정보를 세팅합니다.
    /// </summary>
    /// <param name="_currentPos">현재 위치</param>
    /// <param name="_currentVel">현재 속도</param>
    public void SetPosition(Vector3 _currentPos, Vector3 _currentVel)
    {
        syncTime = 0;
        currentPos = _currentPos;
        currentVel = _currentVel;
    }

    /// <summary>
    /// 회전 정보를 세팅합니다.
    /// </summary>
    /// <param name="_rot">회전 값</param>
    public void SetRotation(Quaternion _rot)
    {
        currentRot = _rot;
    }

}