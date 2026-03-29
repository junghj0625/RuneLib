using UnityEngine;
using UnityEngine.UI;



namespace Rune.UI
{
    public class ScrollPlus : UIObject
    {
        public void GoToChild(int index)
        {
            float heightViewport = RectViewport.rect.height;
            float heightContent = RectContent.rect.height;

            float delta = heightContent - heightViewport;

            if (delta < 0) return;


            // Get active child
            int childIndex = 0;
            int childCount = RectContent.childCount;

            if (childIndex >= childCount) return;

            for (int k = -1; childIndex < childCount; childIndex++)
            {
                var child = RectContent.GetChild(childIndex);

                if (child.gameObject.activeSelf) k++;

                if (k == index) break;
            }

            var rectChild = RectContent.GetChild(childIndex).GetComponent<RectTransform>();


            // Compute normalized position
            float max = rectChild.anchoredPosition.y + rectChild.rect.height * (1.0f - rectChild.pivot.y);
            float min = max - rectChild.rect.height;

            float top = (1.0f - ScrollRect.verticalNormalizedPosition) * delta * -1;
            float bottom = top - heightViewport;

            if (top < max)
            {
                ScrollRect.verticalNormalizedPosition = 1.0f - (-max / delta);
            }

            else if (bottom > min)
            {
                ScrollRect.verticalNormalizedPosition = (min + heightContent) / delta;
            }
        }

        public void GoToTop()
        {
            ScrollRect.verticalNormalizedPosition = 1.0f;
        }

        public void GoToBottom()
        {
            ScrollRect.verticalNormalizedPosition = 0.0f;
        }



        private RectTransform _rectViewport = null;
        public RectTransform RectViewport { get => LazyGetComponentFromTransform(ref _rectViewport, transform.Find("Viewport")); }

        private RectTransform _rectContent = null;
        public RectTransform RectContent { get => LazyGetComponentFromTransform(ref _rectContent, transform.Find("Viewport/Content")); }

        private ScrollRect _scrollRect = null;
        public ScrollRect ScrollRect { get => LazyGetComponent(ref _scrollRect); }
    }
}