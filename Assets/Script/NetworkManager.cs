using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;


/// <summary>
/// 네트워크를 관리하는 유일한 매니저 입니다.
/// </summary>
public class NetworkManager : MonoBehaviour
{

    public static NetworkManager instance;

    public bool isLocalDebug = false;

    /// <summary>
    /// 현재 함께하는 유저 리스트
    /// </summary>
    public List<User> userList;


    SocketIOComponent socket;

    private void Awake()
    {
        socket = GetComponent<SocketIOComponent>();

        if (instance != null)
            Destroy(this.gameObject);

        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    // Socket 에서 들어오는 데이터 값들은 항상 여기서 미리 선언 해야해요.
    void Start()
    {
        socket.On("join", OnJoin);
        socket.On("gameLoad", OnGameLoad);
        socket.On("move", OnMove);
        socket.On("rotate", OnRotate);
        socket.On("userData", OnUserData);


        if (isLocalDebug)
            return;

        StartCoroutine(TestConnect());
    }

    IEnumerator TestConnect()
    {
        yield return new WaitForSeconds(1f);
        EmitJoin("User" + Random.Range(0, 1000));
    }

    #region JoinMethod

    
    public void OnJoin(SocketIOEvent e)
    {
        JSONObject json = e.data;
        PlayerDataManager.instance.my = new User(json.GetField("name").str);

    }

    /// <summary>
    /// 닉네임 정보를 보냅니다.
    /// </summary>
    /// <param name="name"></param>
    public void EmitJoin(string _name)
    {
        if (isLocalDebug)
            return;

        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", _name);

        socket.Emit("join", json);
    }

    #endregion


    #region GameLoadMethod

    /// <summary>
    /// 게임 로드 완료를 받습니다.
    /// </summary>
    /// <param name="e"></param>
    public void OnGameLoad(SocketIOEvent e)
    {
        JSONObject json = e.data;

        float readyCount = json.GetField("ready").f;

        print(readyCount + "is count");
    }

    /// <summary>
    /// 게임 로드의 완료를 보냅니다.
    /// </summary>
    public void EmitGameLoad()
    {
        if (isLocalDebug)
            return;

        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("ready", true);

        socket.Emit("gameLoad", json);
    }

    #endregion

    #region UserDataMethod

    /// <summary>
    /// 유저 정보를 받습니다.
    /// </summary>
    /// <param name="e"></param>
    public void OnUserData(SocketIOEvent e)
    {
        print("set user Data");

        JSONObject json = e.data;
        string name = json.GetField("name").str;

        if (CheckExistUser(name))
            return;

        if (name == PlayerDataManager.instance.my.name)
            userList.Add(PlayerDataManager.instance.my);
        else
            userList.Add(new User(name));
        

    }

    /// <summary>
    /// 유저 데이터를 보냅니다.
    /// </summary>
    public void EmitUserData()
    {
        if (isLocalDebug)
            return;

        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", PlayerDataManager.instance.my.name);

        socket.Emit("userData", json);
    }

    #endregion

    #region PositionMethod

    /// <summary>
    /// 위치를 받습니다.
    /// </summary>
    /// <param name="e"></param>
    public void OnMove(SocketIOEvent e)
    {
        JSONObject json = e.data;
        string name = json.GetField("name").str;
        Vector3 pos = new Vector3(json.GetField("posX").f, json.GetField("posY").f, json.GetField("posZ").f);
        Vector3 vel = new Vector3(json.GetField("velX").f, json.GetField("velY").f, json.GetField("velZ").f);

        FindUserController(name).SetPosition(pos, vel);
    }

    /// <summary>
    /// 위치를 보냅니다.
    /// </summary>
    public void EmitMove(Vector3 _pos, Vector3 _vel)
    {
        if (isLocalDebug)
            return;

        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", PlayerDataManager.instance.my.name);
        json.AddField("posX", _pos.x);
        json.AddField("posY", _pos.y);
        json.AddField("posZ", _pos.z);
        json.AddField("velX", _vel.x);
        json.AddField("velY", _vel.y);
        json.AddField("velZ", _vel.z);

        socket.Emit("move", json);
    }

    #endregion

    #region RotationMethod

    /// <summary>
    /// 회전을 받습니다.
    /// </summary>
    /// <param name="e"></param>
    public void OnRotate(SocketIOEvent e)
    {
        JSONObject json = e.data;
        string name = json.GetField("name").str;
        Quaternion rot = new Quaternion(json.GetField("rotX").f, json.GetField("rotY").f, json.GetField("rotZ").f, json.GetField("rotW").f);

        FindUserController(name).SetRotation(rot);
    }

    /// <summary>
    /// 회전을 보냅니다.
    /// </summary>
    public void EmitRotate(Quaternion _rot)
    {
        if (isLocalDebug)
            return;

        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("name", PlayerDataManager.instance.my.name);
        json.AddField("rotX", _rot.x);
        json.AddField("rotY", _rot.y);
        json.AddField("rotZ", _rot.z);
        json.AddField("rotW", _rot.w);

        socket.Emit("rotate", json);
    }
    #endregion


    #region Utility

    /// <summary>
    /// 유저가 중복되어 있는지 체크합니다.
    /// </summary>
    /// <param name="_name">유저 이름</param>
    /// <returns></returns>
    public bool CheckExistUser(string _name)
    {
        for (int i = 0; i < userList.Count; i++)
        {
            if (userList[i].name == _name)
                return true;
        }

        return false;
    }

    /// <summary>
    /// 이름으로 유저 컨트롤러를 검색합니다.
    /// </summary>
    /// <param name="_name">유저 이름</param>
    /// <returns></returns>
    public PlayerController FindUserController(string _name)
    {
        for (int i = 0; i < userList.Count; i++)
        {
            if (userList[i].name == _name)
                return userList[i].controller;
        }
        return null;
    }

    /// <summary>
    /// 유저 리스트에 있는 모든 유저의 조작을 허용합니다.
    /// </summary>
    /// <param name="_isInput">조작 가능한가?</param>
    public void SetAllUserInput(bool _isInput)
    {
        for (int i = 0; i < userList.Count; i++)
        {
            userList[i].controller.SetInput(_isInput);
        }
    }
    #endregion

}
