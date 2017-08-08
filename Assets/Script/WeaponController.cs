using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{

    public Weapon weapon;
    public GameObject model;

    public Action action;

    public void Set()
    {

        model = Instantiate(weapon.model);
        model.transform.SetParent(transform);
        model.transform.localPosition = Vector3.zero;

        switch (weapon.category)
        {
            case WeaponCategory.Sword:
                break;
            case WeaponCategory.Bow:
                break;
            case WeaponCategory.Cane:
                break;
            case WeaponCategory.Shield:
                break;
            case WeaponCategory.Gun:
                action += model.GetComponent<Gun>().Attack;
                break;
        }


    }

    public void Attack()
    {
        action.Invoke();
    }


}
