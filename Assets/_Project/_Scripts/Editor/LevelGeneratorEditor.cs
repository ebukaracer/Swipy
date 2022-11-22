#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LevelGenerator))]
internal class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var levelGenerator = (LevelGenerator)target;

        GUILayout.BeginHorizontal();
        var spawnLevelBtn = GUILayout.Button("Spawn Levels");

        var clearLevelBtn = GUILayout.Button("Clear Levels");
        GUILayout.EndHorizontal();

        if (spawnLevelBtn)
            levelGenerator.SpawnLevels();
        if (clearLevelBtn)
            levelGenerator.ClearLevels();
    }
}
#endif
