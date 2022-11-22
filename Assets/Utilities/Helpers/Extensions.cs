using System.Collections;
using UnityEngine;

namespace Racer.Utilities
{
    public static class Extensions
    {
        public static void ToggleActive(this GameObject gameObject, bool state)
        {
            if (state)
            {
                if (!gameObject.activeInHierarchy)
                    gameObject.SetActive(true);
            }
            else
            {
                if (gameObject.activeInHierarchy)
                    gameObject.SetActive(false);
            }
        }
    }
}