﻿using System.Collections.Generic;
using UnityEngine;

namespace Racer.Utilities
{
    public static class Utility
    {
        private static Camera _cameraMain;
        /// <summary>
        /// Gets a one time reference to the Camera.Main Method. 
        /// </summary>
        public static Camera CameraMain
        {
            get
            {
                if (_cameraMain == null)
                    _cameraMain = Camera.main;

                return _cameraMain;
            }
        }

        private static readonly Dictionary<float, WaitForSeconds> WaitDelay = new Dictionary<float, WaitForSeconds>();
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
    }
}