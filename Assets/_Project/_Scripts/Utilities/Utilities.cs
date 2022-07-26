using Racer.LoadManager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    static readonly Dictionary<float, WaitForSeconds> WaitDelay = new Dictionary<float, WaitForSeconds>();
    /// <summary>
    /// Container that stores/reuses newly created WaitForSeconds.
    /// </summary>
    /// <param name="time">time(s) to wait</param>
    /// <returns>new WaitForSeconds</returns>
    public static WaitForSeconds GetWaitForSeconds(float time)
    {
        if (WaitDelay.TryGetValue(time, out var waitForSeconds)) return waitForSeconds;

        WaitDelay[time] = new WaitForSeconds(time);

        return WaitDelay[time];
    }

    public static void LoadNewScene(int index) =>
        LoadManager.Instance.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + index);
}

