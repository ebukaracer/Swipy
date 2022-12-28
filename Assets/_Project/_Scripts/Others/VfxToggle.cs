internal class VfxToggle : SoundToggle
{
    protected override void SyncToggle()
    {
        toggleState = ToggleIndex == 0 ? ToggleState.On : ToggleState.Off;

        parentText.text = $"Vfx: {onOffTexts[ToggleIndex]}";
    }
}
