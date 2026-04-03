using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Rune.UI
{
    public partial class ValueBar : UIObject
    {
        public override void InitObjects()
        {
            base.InitObjects();

            RectValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, RectArea.rect.width);
            RectValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RectArea.rect.height);
            RectValue.pivot = _pivotLookup[direction];

            RectValueChanged.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, RectArea.rect.width);
            RectValueChanged.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RectArea.rect.height);
            RectValueChanged.pivot = _pivotLookup[direction];
        }

        public override void OnEnable()
        {
            base.OnEnable();

            Change(false);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            SetAttributeValue(null);
        }


        public void SetAttributeValue(AttributeBase attribute)
        {
            if (attribute != null && attribute is not AttributeClampedInt && attribute is not AttributeClampedFloat)
            {
                Debug.LogWarning("Invalid attribute type.");

                return;
            }


            // Unset attribute
            switch (_attributeValue)
            {
                case AttributeClampedInt attributeClampedInt:
                    attributeClampedInt.OnChange.RemoveListener(OnValueChange);
                    break;

                case AttributeClampedFloat attributeClampedFloat:
                    attributeClampedFloat.OnChange.RemoveListener(OnValueChange);
                    break;
            }


            // Set attribute
            _attributeValue = attribute;

            switch (_attributeValue)
            {
                case AttributeClampedInt attributeClampedInt:
                    attributeClampedInt.OnChange.AddListener(OnValueChange);
                    break;

                case AttributeClampedFloat attributeClampedFloat:
                    attributeClampedFloat.OnChange.AddListener(OnValueChange);
                    break;
            }


            Change(false);
        }



        public DirectionType direction = DirectionType.LeftToRight;

        public bool smoothTransition = true;

        public float smoothTransitionSpeed = 30.0f;
        public float smoothTransitionWaitTime = 1.0f;



        private void OnValueChange(int value)
        {
            Change(smoothTransition);
        }

        private void OnValueChange(float value)
        {
            Change(smoothTransition);
        }

        private void Change(bool smooth)
        {
            if (!gameObject.activeInHierarchy) return;
            
            
            float currentValue, maxValue;
            
            if (_attributeValue is AttributeClampedInt ai)
            {
                currentValue = ai.Value;
                maxValue = ai.ValueMax;
            }
            else if (_attributeValue is AttributeClampedFloat af)
            {
                currentValue = af.Value;
                maxValue = af.ValueMax;
            }
            else
            {
                return;
            }


            float ratio = Mathf.Clamp(currentValue / maxValue, 0.0f, 1.0f);

            RectValue.anchoredPosition = RatioToAnchoredPosition(ratio);


            bool reducing = direction switch
            {
                DirectionType.LeftToRight => RectValueChanged.anchoredPosition.x > RectValue.anchoredPosition.x,
                DirectionType.RightToLeft => RectValueChanged.anchoredPosition.x < RectValue.anchoredPosition.x,
                DirectionType.DownToUp => RectValueChanged.anchoredPosition.y > RectValue.anchoredPosition.y,
                DirectionType.UpToDown => RectValueChanged.anchoredPosition.y < RectValue.anchoredPosition.y,
                _ => new(),
            };


            if (reducing && smooth)
            {
                _smoothTransitionPlayer.Run(this, SmoothTransition());
            }
            else
            {
                RectValueChanged.anchoredPosition = RectValue.anchoredPosition;
            }


            Vector2 RatioToAnchoredPosition(float ratio)
            {
                return direction switch
                {
                    DirectionType.LeftToRight => new Vector2(-RectArea.rect.width * (1.0f - ratio), 0),
                    DirectionType.RightToLeft => new Vector2(RectArea.rect.width * (1.0f - ratio), 0),
                    DirectionType.DownToUp => new Vector2(0, -RectArea.rect.height * (1.0f - ratio)),
                    DirectionType.UpToDown => new Vector2(0, RectArea.rect.height * (1.0f - ratio)),
                    _ => new(),
                };
            }
        

            IEnumerator SmoothTransition()
            {
                if (smoothTransition)
                {
                    yield return new WaitForSecondsRealtime(smoothTransitionWaitTime);

                    while (Vector2.Distance(RectValue.anchoredPosition, RectValueChanged.anchoredPosition) > 0.01f)
                    {
                        RectValueChanged.anchoredPosition = Vector2.MoveTowards(RectValueChanged.anchoredPosition, RectValue.anchoredPosition, smoothTransitionSpeed * Time.unscaledDeltaTime);

                        yield return null;
                    }
                }
                
                
                RectValueChanged.anchoredPosition = RectValue.anchoredPosition;
            }
        }



        private RectTransform _rectArea = null;
        private RectTransform RectArea { get => LazyGetComponentFromTransform(ref _rectArea, transform.Find("Value Area")); }

        private RectTransform _rectValue = null;
        private RectTransform RectValue { get => LazyGetComponentFromTransform(ref _rectValue, transform.Find("Value Area/Value")); }

        private RectTransform _rectValueChanged = null;
        private RectTransform RectValueChanged { get => LazyGetComponentFromTransform(ref _rectValueChanged, transform.Find("Value Area/Value Changed")); }



        private readonly RoutinePlayer _smoothTransitionPlayer = new();

        private AttributeBase _attributeValue = null;



        private static readonly Dictionary<DirectionType, Vector2> _pivotLookup = new()
        {
            [DirectionType.LeftToRight] = new(0.0f, 0.5f),
            [DirectionType.RightToLeft] = new(1.0f, 0.5f),
            [DirectionType.DownToUp] = new Vector2(0.5f, 0.0f),
            [DirectionType.UpToDown] = new Vector2(0.5f, 1.0f),
        };



        public enum DirectionType
        {
            LeftToRight,
            RightToLeft,
            DownToUp,
            UpToDown,
        }
    }
}