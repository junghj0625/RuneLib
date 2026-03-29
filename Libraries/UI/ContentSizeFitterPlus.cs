using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class ContentSizeFitterPlus : UIBehaviour, ILayoutSelfController
{
    public void SetLayoutHorizontal()
    {
        ApplySizing();
    }

    public void SetLayoutVertical()
    {
        ApplySizing();
    }



    public bool fitWidth = true;
    public bool fitHeight = true;



    protected override void OnEnable()
    {
        base.OnEnable();

        SetDirty();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        SetDirty();
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        SetDirty();
    }
#endif



    private void ApplySizing()
    {
        var size = Rect.sizeDelta;

        if (fitWidth)
        {
            float preferred = LayoutUtility.GetPreferredWidth(Rect);
            if (minWidth > 0) preferred = Mathf.Max(preferred, minWidth);
            if (maxWidth > 0) preferred = Mathf.Min(preferred, maxWidth);
            size.x = preferred;
        }

        if (fitHeight)
        {
            float preferred = LayoutUtility.GetPreferredHeight(Rect);
            if (minHeight > 0) preferred = Mathf.Max(preferred, minHeight);
            if (maxHeight > 0) preferred = Mathf.Min(preferred, maxHeight);
            size.y = preferred;
        }

        Rect.sizeDelta = size;
    }

    private void SetDirty()
    {
        if (!IsActive()) return;

        LayoutRebuilder.MarkLayoutForRebuild(Rect);
    }



    private RectTransform _rect;
    private RectTransform Rect
    {
        get
        {
            if (_rect == null) _rect = GetComponent<RectTransform>();

            return _rect;
        }
    }



    [SerializeField] private float minWidth = -1f;
    [SerializeField] private float minHeight = -1f;

    [SerializeField] private float maxWidth = -1f;
    [SerializeField] private float maxHeight = -1f;
}
