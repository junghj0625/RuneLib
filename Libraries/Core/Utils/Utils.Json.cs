// Title: Utils.Json.cs
// Version: 1.0.1
// Date: 2025-06-12



using UnityEngine;



namespace Rune.Utils
{
    public readonly struct Json
    {
        public static T ReadJson<T>(string path) where T : class
        {
            /* T must be a class with [System.Serializable] attribute. */

            TextAsset jsonText = Resources.Load<TextAsset>(path);

            if (jsonText != null)
            {
                try
                {
                    return JsonUtility.FromJson<T>(jsonText.text);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"JSON parsing error. ({ex.Message})");

                    return null;
                }
            }
            else
            {
                Debug.LogError($"JSON file not found. ({path})");

                return null;
            }
        }
    }
}