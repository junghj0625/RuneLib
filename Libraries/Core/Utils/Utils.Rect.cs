using UnityEngine;



namespace Rune.Utils
{
    public readonly struct Rect
    {
        public static Vector2[] GetScreenCorners(RectTransform rect, Camera camera = null)
        {
            Vector3[] worldCorners = new Vector3[4];

            rect.GetWorldCorners(worldCorners);

            Vector2[] screenCorners = new Vector2[4];

            for (int i = 0; i < 4; i++)
            {
                screenCorners[i] = RectTransformUtility.WorldToScreenPoint(null, worldCorners[i]);
            }

            return screenCorners;         
        }
    }
}