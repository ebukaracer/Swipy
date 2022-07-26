#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var levelGenerator = (LevelGenerator)target;

        GUILayout.BeginHorizontal();
        bool spawnLevelBtn = GUILayout.Button("Spawn Levels");

        bool clearLevelBtn = GUILayout.Button("Clear Levels");
        GUILayout.EndHorizontal();

        if (spawnLevelBtn)
            levelGenerator.SpawnLevels();
        if (clearLevelBtn)
            levelGenerator.ClearLevels();
    }
}
#endif
