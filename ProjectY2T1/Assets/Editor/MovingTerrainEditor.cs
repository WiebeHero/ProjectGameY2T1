using TerrainMovement;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MovingTerrain)), CanEditMultipleObjects]
    public class MovingTerrainEditor : UnityEditor.Editor
    {
        public SerializedProperty
            god_Prop,
            road_Prop,
            settingType_Prop,
            timeToSpawn_Prop,
            spawnAfterTile_Prop;

        void OnEnable()
        {
            god_Prop = serializedObject.FindProperty("god");
            road_Prop = serializedObject.FindProperty("road");
            settingType_Prop = serializedObject.FindProperty("settingType");
            timeToSpawn_Prop = serializedObject.FindProperty("timeToSpawn");
            spawnAfterTile_Prop = serializedObject.FindProperty("spawnAfterTile");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            MovingTerrain movingTerrain = target as MovingTerrain;
            EditorGUILayout.ObjectField(god_Prop, new GUIContent("God"));
            EditorGUILayout.ObjectField(road_Prop, new GUIContent("Road"));
            EditorGUILayout.Toggle(new GUIContent("Disabled"),movingTerrain.disabled);
            EditorGUILayout.Toggle(new GUIContent("Remove on Move"),movingTerrain.removeOnDespawn);
        
            EditorGUILayout.PropertyField(settingType_Prop, new GUIContent("Settings"));
        
            MovingTerrain.SettingType spawnType = (MovingTerrain.SettingType)settingType_Prop.enumValueIndex;
            switch (spawnType)
            {
                case MovingTerrain.SettingType.SpawnAfterTimePassed:
                    EditorGUILayout.PropertyField( timeToSpawn_Prop, new GUIContent("Time to spawn"));
                    break;
                case MovingTerrain.SettingType.SpawnAfterTilesPassed:
                    EditorGUILayout.PropertyField( timeToSpawn_Prop, new GUIContent("Spawn After Tiles Passed"));
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        
        }
    }
}
