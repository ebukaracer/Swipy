#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


class LevelManagerEditor : Editor
{
    [CustomEditor(typeof(LevelManager))]
    public class LevelGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var levelManager = (LevelManager)target;

            GUILayout.BeginHorizontal();
            bool initializeLevels = GUILayout.Button("Initialize Levels");
            GUILayout.EndHorizontal();

            if (initializeLevels)
                levelManager.InitializeLevels();
        }
    }
}
#endif
