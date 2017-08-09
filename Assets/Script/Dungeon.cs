using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 던전을 정의 합니다.
/// </summary>
public class Dungeon : MonoBehaviour
{
    /// <summary>
    /// 이름
    /// </summary>
    public string dungeonName;
    /// <summary>
    /// 아이디
    /// </summary>
    public int id;
    /// <summary>
    /// 던전 종류
    /// </summary>
    public DungeonCategory category;
    /// <summary>
    /// 던전 설명
    /// </summary>
    [TextArea]
    public string context;
    /// <summary>
    /// 맵 사이즈
    /// </summary>
    public Vector3 size;
    /// <summary>
    /// 마지막 포인트 리스트
    /// </summary>
    public List<LastPoint> lastPointList;

    [HideInInspector]
    public Transform tr;

    void Awake()
    {
        tr = GetComponent<Transform>();
    }

    public void FindLastPoint()
    {
        lastPointList.Clear();
        Transform[] tempTransforms = GetComponentsInChildren<Transform>();

        foreach (Transform child in tempTransforms)
        {
            if (child.name.Contains("LastPoint"))
            {
                lastPointList.Add(new LastPoint(child));
            }
        }
    }

    public void FindLastPointAndRemove(MapDirection _dir)
    {
        MapDirection findDir = MapDirection.forward;

        switch (_dir)
        {
            case MapDirection.forward: findDir = MapDirection.back; break;
            case MapDirection.back: findDir = MapDirection.forward; break;
            case MapDirection.up: findDir = MapDirection.down; break;
            case MapDirection.down: findDir = MapDirection.up; break;
            case MapDirection.left: findDir = MapDirection.right; break;
            case MapDirection.right: findDir = MapDirection.left; break;
        }

        for (int i = 0; i < lastPointList.Count; i++) {

            if (lastPointList[i].direction == findDir)
            {
                lastPointList[i].Remove();
                lastPointList.RemoveAt(i);
            }
        }
        
    }

    public bool isAlreadyDir(MapDirection _dir)
    {
        MapDirection findDir = MapDirection.forward;

        switch (_dir)
        {
            case MapDirection.forward: findDir = MapDirection.back; break;
            case MapDirection.back: findDir = MapDirection.forward; break;
            case MapDirection.up: findDir = MapDirection.down; break;
            case MapDirection.down: findDir = MapDirection.up; break;
            case MapDirection.left: findDir = MapDirection.right; break;
            case MapDirection.right: findDir = MapDirection.left; break;
        }

        for (int i = 0; i < lastPointList.Count; i++)
        {

            if (lastPointList[i].direction == findDir)
            {
                return true;
            }
        }

        return false;
    }


    public float GetSize(MapDirection _dir)
    {
        switch (_dir)
        {
            case MapDirection.forward: return size.z;
            case MapDirection.back: return size.z;
            case MapDirection.up: return size.y;
            case MapDirection.down: return size.y;
            case MapDirection.left: return size.x;
            case MapDirection.right: return size.x;
        }
        return 0;
    }

    public bool CheckRoomSize(LastPoint _point, Dungeon _currentDungeon, Dungeon _nextDungeon)
    {

        RaycastHit[] hit;
        hit = Physics.BoxCastAll((_point.point.position + Vector3.up * _nextDungeon.size.y * 0.5f) + _point.ConvertVector3(), _nextDungeon.size * 0.5f, _point.ConvertVector3(),
             Quaternion.identity, _nextDungeon.GetSize(_point.direction) * 0.5f, LayerMask.GetMask("Room"));


        int cnt = 0;
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject.GetComponent<Dungeon>() != _currentDungeon && hit[i].collider.gameObject.GetComponent<Dungeon>() != _nextDungeon)
                ++cnt;
        }


        //print("hit = " + cnt);

        if (cnt == 0)
            return false;
        else
            return true;

        //return Physics.BoxCast((_point.point.position + Vector3.up * _nextDungeon.size.y * 0.5f), _nextDungeon.size, _point.ConvertVector3(),
        //     Quaternion.identity, _currentDungeon.GetSize(_point.direction), LayerMask.GetMask("Room"));


    }

}

/// <summary>
/// 던전의 마지막 포인트를 정의힙니다.
/// </summary>
[System.Serializable]
public class LastPoint
{
    /// <summary>
    /// 좌표
    /// </summary>
    public Transform point;
    /// <summary>
    /// 진행 방향
    /// </summary>
    public MapDirection direction;


    float distance = 10;

    public LastPoint()
    {

    }

    public LastPoint(Transform _tr)
    {
        point = _tr;
    }

    public LastPoint(LastPoint _point)
    {
        point = _point.point;
        direction = _point.direction;
    }

    public Quaternion ConvertQuaternion(Quaternion _rot)
    {
        Quaternion rot;

        rot = _rot;

        switch (direction)
        {
            case MapDirection.forward: return Quaternion.Euler(rot.x + 0, rot.y + 0, rot.z + 0);
            case MapDirection.back: return Quaternion.Euler(rot.x + 0, rot.y + 180, rot.z + 0);
            case MapDirection.up: return Quaternion.Euler(rot.x + 0, rot.y + 0, rot.z + 0);
            case MapDirection.down: return Quaternion.Euler(rot.x + 0, rot.y + 0, rot.z + 0);
            case MapDirection.left: return Quaternion.Euler(rot.x + 0, rot.y + -90, rot.z + 0);
            case MapDirection.right: return Quaternion.Euler(rot.x + 0, rot.y + 90, rot.z + 0);
        }
        return Quaternion.identity;
    }

    public Vector3 ConvertVector3()
    {
        switch (direction)
        {
            case MapDirection.forward: return Vector3.forward;
            case MapDirection.back: return Vector3.back;
            case MapDirection.up: return Vector3.up;
            case MapDirection.down: return Vector3.down;
            case MapDirection.left: return Vector3.left;
            case MapDirection.right: return Vector3.right;
        }
        return Vector3.zero;
    }

    public Vector3 GetPosition()
    {
        //Debug.Log(point.position + "/" + point.localPosition);
        return point.position;
    }

    public bool CheckDir()
    {
        bool isAlready = Physics.Raycast(point.position + Vector3.up - ConvertVector3(), ConvertVector3(), distance, LayerMask.GetMask("Room"));

        if (isAlready)
            Debug.DrawRay(point.position + Vector3.up - ConvertVector3(), ConvertVector3() * distance, Color.blue, 0.1f);
        else
            Debug.DrawRay(point.position + Vector3.up - ConvertVector3(), ConvertVector3() * distance, Color.red, 0.1f);

        return isAlready;
    }

    public void Remove()
    {
        point.gameObject.SetActive(false);

    }

}


/// <summary>
/// 던전 진행 방향
/// </summary>
[System.Serializable]
public enum MapDirection
{
    /// <summary>
    /// 앞
    /// </summary>
    forward,
    /// <summary>
    /// 뒤 
    /// </summary>
    back,
    /// <summary>
    /// 위
    /// </summary>
    up,
    /// <summary>
    /// 아래
    /// </summary>
    down,
    /// <summary>
    /// 왼쪽
    /// </summary>
    left,
    /// <summary>
    /// 오른쪽
    /// </summary>
    right
}

/// <summary>
/// 던전 카테고리를 정의합니다.
/// </summary>
[System.Serializable]
public enum DungeonCategory
{
    /// <summary>
    /// 전투
    /// </summary>
    Battle,
    /// <summary>
    /// 이벤트
    /// </summary>
    Event,
    /// <summary>
    /// 상점
    /// </summary>
    Shop,
    /// <summary>
    /// 휴식
    /// </summary>
    Rest,
    /// <summary>
    /// 보상
    /// </summary>
    Prize
}