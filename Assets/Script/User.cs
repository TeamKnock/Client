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
    /// 스텟
    /// </summary>
    public Status status;
    /// <summary>
    /// 장비 한 아이템들
    /// </summary>
    public Equipment equipment;
    /// <summary>
    /// 컨트롤러
    /// </summary>
    public PlayerController controller;


    public User()
    {
        name = "Loading..";
        level = 1;
    }

    public User(string _name)
    {
        name = _name;
        level = 1;
    }

    public User(string _name, int _level, float _maxHp, float _maxMp, float _hp, float _mp)
    {
        name = _name;
        level = _level;
    }

}

/// <summary>
/// 스텟
/// </summary>
[System.Serializable]
public class Status
{
    /*
    /// <summary>
    /// 공격력 : 데미지 증가, 크리티컬 데미지 증가
    /// </summary>
    public float attack = 0f;
    /// <summary>
    /// 회복력 : 마나 회복 속도 증가, 치유량 증가
    /// </summary>
    public float recover = 0f;
    /// <summary>
    /// 방어력 : 피해 감소, 회피율 증가
    /// </summary>
    public float defence = 0f;
    /// <summary>
    /// 탐사력 : 골드 획득량 증가, 아이템 드랍 확률 증가
    /// </summary>
    public float luck = 0f;
    /// <summary>
    /// 최대 체력 포인트
    /// </summary>
    public float maxHp = 0;
    /// <summary>
    /// 체력 포인트
    /// </summary>
    public float hp = 0;
    /// <summary>
    /// 최대 마나 포인트
    /// </summary>
    public float maxMp = 0;
    /// <summary>
    /// 마나 포인트
    /// </summary>
    public float mp = 0;
    /// <summary>
    /// 데미지
    /// </summary>
    public float damage = 0f;
    /// <summary>
    /// 크리티컬 확률
    /// </summary>
    public float critical = 0f;
    /// <summary>
    /// 크리티컬 데미지
    /// </summary>
    public float criticalDamage = 0f;
    /// <summary>
    /// 체력 회복 속도
    /// </summary>
    public float recoverHpSpeed = 0f;
    /// <summary>
    /// 마나 회복 속도
    /// </summary>
    public float recoverMpSpeed = 0f;
    /// <summary>
    /// 회피율
    /// </summary>
    public float evasion = 0f;
    /// <summary>
    /// 골드 획득량
    /// </summary>
    public float getMoney = 0f;
    /// <summary>
    /// 아이템 획득 확률
    /// </summary>
    public float getItem = 0f;
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed = 0f;
    /// <summary>
    /// 공격 속도
    /// </summary>
    public float attackSpeed = 0f;
    /// <summary>
    /// 점프 횟수
    /// </summary>
    public int jumpCount = 0;
    /// <summary>
    /// 점프 높이
    /// </summary>
    public float jumpPower = 0f;
    */

    /// <summary>
    /// 스텟 종류
    /// </summary>
    public StatusCategory category;
    /// <summary>
    /// 수치 값
    /// </summary>
    public float value = 0f;
    /// <summary>
    /// 퍼센트 값 인가?
    /// </summary>
    public bool isPercent = false;
}

/// <summary>
/// 스텟의 종류를 정의 합니다.
/// </summary>
public enum StatusCategory
{
    /// <summary>
    /// 공격력 : 데미지 증가, 크리티컬 데미지 증가
    /// </summary>
    Attack,
    /// <summary>
    /// 회복력 : 마나 회복 속도 증가, 치유량 증가
    /// </summary>
    Recover,
    /// <summary>
    /// 방어력 : 피해 감소, 회피율 증가
    /// </summary>
    Defence,
    /// <summary>
    /// 탐사력 : 골드 획득량 증가, 아이템 드랍 확률 증가
    /// </summary>
    Luck,
    /// <summary>
    /// 최대 체력 포인트
    /// </summary>
    MaxHp,
    /// <summary>
    /// 체력 포인트
    /// </summary>
    Hp,
    /// <summary>
    /// 최대 마나 포인트
    /// </summary>
    MaxMp,
    /// <summary>
    /// 마나 포인트
    /// </summary>
    Mp,
    /// <summary>
    /// 데미지
    /// </summary>
    Damage,
    /// <summary>
    /// 크리티컬 확률
    /// </summary>
    Critical,
    /// <summary>
    /// 크리티컬 데미지
    /// </summary>
    CriticalDamage,
    /// <summary>
    /// 체력 회복 속도
    /// </summary>
    RecoverHpSpeed,
    /// <summary>
    /// 마나 회복 속도
    /// </summary>
    RecoverMpSpeed,
    /// <summary>
    /// 회피율
    /// </summary>
    Evasion,
    /// <summary>
    /// 골드 획득량
    /// </summary>
    GetMoney,
    /// <summary>
    /// 아이템 획득 확률
    /// </summary>
    GetItem,
    /// <summary>
    /// 이동 속도
    /// </summary>
    MoveSpeed,
    /// <summary>
    /// 공격 속도
    /// </summary>
    AttackSpeed,
    /// <summary>
    /// 점프 횟수
    /// </summary>
    JumpCount,
    /// <summary>
    /// 점프 높이
    /// </summary>
    JumpPower
}

/// <summary>
/// 착용한 아이템들
/// </summary>
[System.Serializable]
public class Equipment
{
    /// <summary>
    /// 머리
    /// </summary>
    public Item head;
    /// <summary>
    /// 몸통
    /// </summary>
    public Item body;
    /// <summary>
    /// 손
    /// </summary>
    public Item hand;
    /// <summary>
    /// 발
    /// </summary>
    public Item foot;
    /// <summary>
    /// 무기
    /// </summary>
    public Weapon weapon;

}


