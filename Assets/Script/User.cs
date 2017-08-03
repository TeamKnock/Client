using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유저의 정보
/// </summary>
[System.Serializable]
public class User
{
    /// <summary>
    /// 이름
    /// </summary>
    public string name = "Loading..";
    /// <summary>
    /// 레벨
    /// </summary>
    public int level = 1;
    /// <summary>
    /// 최대 체력 포인트
    /// </summary>
    public float maxHp = 0f;
    /// <summary>
    /// 체력 포인트
    /// </summary>
    public float hp = 0f;
    /// <summary>
    /// 최대 마나 포인트
    /// </summary>
    public float maxMp = 0;
    /// <summary>
    /// 마나 포인트
    /// </summary>
    public float mp = 0f;
    /// <summary>
    /// 컨트롤러
    /// </summary>
    public PlayerController controller;

    public User()
    {
        name = "Loading..";
        level = 1;
        maxHp = 0;
        maxMp = 0;
        hp = 0;
        mp = 0;
    }

    public User(string _name)
    {
        name = _name;
        level = 1;
        maxHp = 0;
        maxMp = 0;
        hp = 0;
        mp = 0;
    }

    public User(string _name, int _level, float _maxHp, float _maxMp, float _hp, float _mp)
    {
        name = _name;
        level = _level;
        maxHp = _maxHp;
        maxMp = _maxMp;
        hp = _hp;
        mp = _mp;
    }

}