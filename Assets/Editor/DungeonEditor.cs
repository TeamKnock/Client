using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dungeon))]
public class DungeonEditor : Editor
{
    Dungeon dungeon = null;

    void OnEnable()
    {
        //Character 컴포넌트를 얻어오기
        dungeon = (Dungeon)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Find LastPoint"))
        {
            dungeon.FindLastPoint();
        }
    }
}