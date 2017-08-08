using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 방어구의 최상위 클래스
/// </summary>
[System.Serializable]
public class Armor : Item {

    public ArmorCategory category;

    /// <summary>
    /// 아이템 옵션
    /// </summary>
    public ItemOption options;
    
    public GameObject model;

    public  void Use()
    {

    }

    public  void Equip()
    {

    }

}

public enum ArmorCategory
{
    Head,
    Body,
    Hand,
    Foot
}