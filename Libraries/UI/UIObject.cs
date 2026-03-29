using UnityEngine;



namespace Rune.UI
{
    public abstract class UIObject : MonoPlus
    {
        private RectTransform _rect = null;
        public RectTransform Rect { get => LazyGetComponent(ref _rect); }
    }
}