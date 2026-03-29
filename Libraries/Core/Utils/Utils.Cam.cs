using System.Collections.Generic;
using UnityEngine;



namespace Rune.Utils
{
    public struct Cam
    {
        public static Vector2 NormalizedScreenPosition(Vector2 screenPos)
        {
            return screenPos / new Vector2(Screen.width, Screen.height);
        }

        public static Vector2 NormalizedMousePosition()
        {
            return NormalizedScreenPosition(Input.mousePosition);
        }


        public static Vector2 WorldToScreen(Vector3 worldPos, Camera camera = null)
        {
            if (camera == null) camera = Camera.main;

            return camera.WorldToScreenPoint(worldPos);
        }

        public static Vector2 WorldToScreenNormalized(Vector3 worldPos, Camera camera = null)
        {
            return NormalizedScreenPosition(WorldToScreen(worldPos, camera));
        }


        public static List<Vector2> ScreenCorners(RectTransform rect, Canvas canvas, Camera camera = null)
        {
            if (rect == null || canvas == null) return null;

            if (camera == null) camera = Camera.main;


            var worldCorners = new Vector3[4];

            rect.GetWorldCorners(worldCorners);

            var screenCorners = new List<Vector2>(4);


            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                for (int i = 0; i < 4; i++) screenCorners.Add(worldCorners[i]);
            }
            else
            {
                for (int i = 0; i < 4; i++) screenCorners.Add(camera.WorldToScreenPoint(worldCorners[i]));
            }


            return screenCorners;
        }

        public static List<Vector2> ScreenCornersNormalized(RectTransform rect, Canvas canvas, Camera camera = null)
        {
            return ScreenCorners(rect, canvas, camera).ConvertAll(c => NormalizedScreenPosition(c));
        }

        public static Vector2 ScreenCorner(RectTransform rect, CornerType cornerType, Canvas canvas, Camera camera = null)
        {
            var screenCorners = ScreenCorners(rect, canvas, camera);

            return screenCorners != null ? screenCorners[(int)cornerType] : default;
        }

        public static Vector2 ScreenCornerNormalized(RectTransform rect, CornerType cornerType, Canvas canvas, Camera camera = null)
        {
            return NormalizedScreenPosition(ScreenCorner(rect, cornerType, canvas, camera));
        }


        public static void ClampToScreen(RectTransform rect, Canvas canvas, Camera camera = null)
        {
            if (rect == null) return;

            if (camera == null) camera = Camera.main;


            var screenCorners = ScreenCorners(rect, canvas, camera);


            float minX = Mathf.Min(screenCorners[0].x, screenCorners[1].x, screenCorners[2].x, screenCorners[3].x);
            float maxX = Mathf.Max(screenCorners[0].x, screenCorners[1].x, screenCorners[2].x, screenCorners[3].x);
            float minY = Mathf.Min(screenCorners[0].y, screenCorners[1].y, screenCorners[2].y, screenCorners[3].y);
            float maxY = Mathf.Max(screenCorners[0].y, screenCorners[1].y, screenCorners[2].y, screenCorners[3].y);

            float screenW = Screen.width;
            float screenH = Screen.height;

            float offsetX = 0f;
            float offsetY = 0f;


            if (minX < 0)
            {
                offsetX = -minX;
            }
            else if (maxX > screenW)
            {
                offsetX = screenW - maxX;
            }

            if (minY < 0)
            {
                offsetY = -minY;
            }
            else if (maxY > screenH)
            {
                offsetY = screenH - maxY;
            }


            rect.anchoredPosition += new Vector2(offsetX, offsetY);
        }
    
    
    
        public enum CornerType
        {
            BottomLeft = 0,
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 3,
        }
    }
}