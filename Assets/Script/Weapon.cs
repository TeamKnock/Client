using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기의 최상위 클래스
/// </summary>
public class Weapon : MonoBehaviour {


    public Transform tr;

    public WeaponCategory category;

    /// <summary>
    /// 아이템 옵션
    /// </summary>
    public ItemOption options;

    public GameObject model;

    void Awake() {
        tr = GetComponent<Transform>();
        print("hi");
    }


    public virtual void Use()
    {

    }

    public virtual void Equip()
    {

    }


    
}

public enum WeaponCategory {
    Sword,
    Bow,
    Cane,
    Shield,
    Gun
}
