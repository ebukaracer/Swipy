#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Racer.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {

        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public static void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

#if UNITY_EDITOR
        [MenuItem("SaveManager/Clear all Playerprefs?")]
#endif
        public static void ClearAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}