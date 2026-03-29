using System;
using System.Collections;
using System.IO;
using UnityEngine;



namespace Rune
{
    public class Screenshot : MonoPlusSingleton<Screenshot>
    {
        public static void Capture()
        {
            SingletonInstance.StartCoroutine(SingletonInstance.CaptureRoutine());
        }



        private IEnumerator CaptureRoutine()
        {
            string directory = Application.dataPath + "/../Screenshots/";

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            string path = directory + "screenshot_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";

            ScreenCapture.CaptureScreenshot(path);

            DebugManager.Log("Screenshot captured. (" + path + ")");

            yield return null;
        }
    }
}