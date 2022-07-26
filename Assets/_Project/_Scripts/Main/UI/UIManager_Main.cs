using Racer.SaveSystem;
using Racer.SoundManager;
using TMPro;
using UnityEngine;

class UIManager_Main : MonoBehaviour
{
    [Header("Start Pane")]

    [SerializeField]
    TextMeshProUGUI mainLevelT;

    [Space(10)]

    // More Panel
    int inverseFxState;
    bool isFxStateInit;

    int inverseSoundState;
    bool isSoundStateInit;

    [Header("More Pane")]

    [SerializeField]
    TextMeshProUGUI trailT;

    [Space(5)]

    [SerializeField]
    TextMeshProUGUI soundT;

    [Space(10)]

    [Header("Stats Pane")]
    [SerializeField]
    TextMeshProUGUI maxComboT;

    [SerializeField, Space(10)]
    TextMeshProUGUI fastestTimeT;

    [SerializeField]
    TextMeshProUGUI slowestTimeT;

    [SerializeField, Space(10)]
    TextMeshProUGUI totalRatingCountT;


    private void Start()
    {
        SetMostRecentLevel();

        InitStatProperties();

        VfxAction();

        SoundAction();
    }


    void SetMostRecentLevel()
    {
        mainLevelT.SetText("Level: {0}", SaveSystem.GetData("MostRecentLevel", 1));
    }

    public void VfxAction()
    {
        if (!isFxStateInit)
        {
            inverseFxState = SaveManager.GetInt("Particles");

            isFxStateInit = true;
        }

        if (inverseFxState == 1)
        {
            // Logic 
            SaveManager.SaveInt("Particles", inverseFxState);

            trailT.SetText("Vfx: On");

            // Reverse
            inverseFxState = 0;
        }
        else
        {
            // Logic
            SaveManager.SaveInt("Particles", inverseFxState);

            trailT.SetText("Vfx: Off");

            // Reverse
            inverseFxState = 1;
        }
    }

    public void SoundAction()
    {
        if (!isSoundStateInit)
        {
            inverseSoundState = SaveManager.GetInt("Sound");

            isSoundStateInit = true;
        }
        if (inverseSoundState == 1)
        {
            // Logic
            SoundManager.Instance.EnableMusic(true);
            SoundManager.Instance.EnableSfx(true);


            SaveManager.SaveInt("Sound", inverseSoundState);

            soundT.SetText("Sound: On");

            // Reverse
            inverseSoundState = 0;
        }

        else
        {
            // Logic
            SoundManager.Instance.EnableMusic(false);
            SoundManager.Instance.EnableSfx(false);

            SaveManager.SaveInt("Sound", inverseSoundState);

            soundT.SetText("Sound: Off");

            // Reverse
            inverseSoundState = 1;
        }
    }

    void InitStatProperties()
    {
        maxComboT.SetText("Max Combo: {0}", SaveSystem.GetData<int>("Combo"));

        fastestTimeT.SetText("Fastest Time: {0:2}", SaveSystem.GetData<float>("Fastest_Time"));

        slowestTimeT.SetText("Slowest Time: {0:2}", SaveSystem.GetData<float>("Slowest_Time"));

        totalRatingCountT.SetText("{0}/180", LevelManager.Instance.GetTotalRatings);
    }

    public void ResetAction()
    {
        // Reset player's progress
        SaveManager.ClearAll();

        SaveSystem.DeleteSaveFile();

#if !UNITY_EDITOR && UNITY_ANDROID
        RestartAndroid.Restart();
#endif
    }


    public void ExitAction()
    {
        // Quits game
#if UNITY_ANDROID
        Application.Quit();
#endif
    }

    public void PlayAction()
    {
        // Play game from main
        Utilities.LoadNewScene(1);
    }
}