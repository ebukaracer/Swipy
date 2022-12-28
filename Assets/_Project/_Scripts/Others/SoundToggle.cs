using Racer.SaveSystem;
using Racer.SoundManager;
using Racer.Utilities;
using TMPro;
using UnityEngine;

public class SoundToggle : ToggleProvider
{
    [Space(5), Header("TARGET GRAPHICS")]
    public TextMeshProUGUI parentText;
    public string[] onOffTexts;


    private void Awake()
    {
        InitToggle();
    }

    protected override void InitToggle()
    {
        ToggleIndex = SaveSystem.GetData<int>(saveString);

        SyncToggle();
    }

    public override void Toggle()
    {
        base.Toggle();

        SaveSystem.SaveData(saveString, ToggleIndex);

        SyncToggle();
    }

    protected override void SyncToggle()
    {
        base.SyncToggle();

        parentText.text = $"Sound: {onOffTexts[ToggleIndex]}";
    }

    protected override void ApplyToggle()
    {
        switch (toggleState)
        {
            case ToggleState.On:
                SoundManager.Instance.EnableMusic(true);
                SoundManager.Instance.EnableSfx(true);
                break;
            case ToggleState.Off:
                SoundManager.Instance.EnableMusic(false);
                SoundManager.Instance.EnableSfx(false);
                break;
        }
    }
}