using UnityEngine;

public enum ToggleState { On, Off }

namespace Racer.Utilities
{
    /// <summary>
    /// Provides useful methods for a Custom Toggle Implementation,
    /// useful for a two-way toggle; on/off.
    /// </summary>
    public abstract class ToggleProvider : MonoBehaviour
    {
        // Placeholder for the current effect.
        // This can be substituted with a bool.
        protected int ToggleIndex;

        public ToggleState toggleState;

        // Handy if you'd save the effect's current state.
        public string saveString;


        /// <summary>
        /// Retrieves the current state of the Toggle.
        /// </summary>
        /// <remarks>
        /// Should be called on the Start or Awake function.
        /// Invoke your save-class here(retrieval).
        /// </remarks>
        protected abstract void InitToggle();

        protected abstract void ApplyToggle();

        /// <summary>
        /// Toggles the current effect On/Off.
        /// </summary>
        /// <remarks>
        /// Should be assigned to a button, in other to achieve a Toggle action.
        /// Invoke your save-class here(saving).
        /// </remarks>
        public virtual void Toggle()
        {
            ToggleIndex++;

            ToggleIndex %= (int)ToggleState.Off + 1;
        }

        /// <summary>
        /// Syncs the Toggle's current state.
        /// </summary>
        /// <remarks>
        /// Override this method to add extra logic.
        /// Default state is 0 -> play.
        /// </remarks>
        protected virtual void SyncToggle()
        {
            // 0 = play, 1 = stop
            toggleState = ToggleIndex == 0 ? ToggleState.On : ToggleState.Off;

            ApplyToggle();
        }
    }
}