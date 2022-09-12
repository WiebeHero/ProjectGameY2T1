using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CustomEditor(typeof(MovingTerrain))]
public class MovingTerrainTempEditor : Editor
{
    override public void OnInspectorGUI()
    {
        MovingTerrainTemp movingTerrain = target as MovingTerrainTemp;
        
        GUILayoutOption[] options = new GUILayoutOption[0];
        
        movingTerrain.spawnType = (MovingTerrainTemp.SpawnType)EditorGUILayout.EnumPopup("Cursor", movingTerrain.spawnType);
        bool visible = false;
        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(visible)))
        {
            
        }
    }
}
