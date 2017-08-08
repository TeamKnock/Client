using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemDB))]
public class ItemDBEditor : Editor
{
    ItemDB db = null;

    void OnEnable()
    {
        //Character 컴포넌트를 얻어오기
        db = (ItemDB)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
    }
}