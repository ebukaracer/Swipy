using Racer.SaveSystem;
using Racer.Utilities;

internal class VfxToggle : ToggleProvider
{
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
}
