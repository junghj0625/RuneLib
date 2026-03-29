using System.Collections;
using UnityEngine;



namespace Rune.UI
{
    public partial class ValueBar : UIObject
    {
        public override void InitObjects()
        {
            base.InitObjects();

            RectValue.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, RectArea.rect.width);
            
            RectValueChanged.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, RectArea.rect.width);

            if (direction == Direction.Horizontal)
            {
                RectValue.pivot = new Vector2(0.0f, 0.5f);
            }
            else if (direction == Direction.Vertical)
            {
                RectValue.pivot = new Vector2(0.5f, 0.0f);
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            switch (_attributeValue)
            {
                case AttributeClampedInt attributeClampedInt:
                    _valueNew = attributeClampedInt.Value;
                    _valueMaxNew = attributeClampedInt.ValueMax;
                    break;

                case AttributeClampedFloat attributeClampedFloat:
                    _valueNew = attributeClampedFloat.Value;
                    _valueMaxNew = attributeClampedFloat.ValueMax;
                    break;

                default:
                
                    switch (idleMode)
                    {
                        case IdleMode.Full:
                            _valueNew = 1;
                            _valueMaxNew = 1;
                            break;

                        case IdleMode.Empty:
                            _valueNew = 0;
                            _valueMaxNew = 1;
                            break;
                    }

                    break;
            }

            UpdateBar();
        }

        public override void OnEnable()
        {
            base.OnEnable();

            Refresh();
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
                    attributeClampedInt.OnChangeOldToNew.RemoveListener(OnValueChange);
                    break;

                case AttributeClampedFloat attributeClampedFloat:
                    attributeClampedFloat.OnChangeOldToNew.RemoveListener(OnValueChange);
                    break;
            }


            _attributeValue = attribute;


            // Set attribute
            switch (_attributeValue)
            {
                case AttributeClampedInt attributeClampedInt:
                    attributeClampedInt.OnChangeOldToNew.AddListener(OnValueChange);
                    break;

                case AttributeClampedFloat attributeClampedFloat:
                    attributeClampedFloat.OnChangeOldToNew.AddListener(OnValueChange);
                    break;
            }


            Refresh();
        }



        public Direction direction = Direction.Horizontal;

        public IdleMode idleMode = IdleMode.Full;

        public bool smoothTransition = true;



        private void OnValueChange(int oldValue, int newValue)
        {
            _valueOld = oldValue;
            _valueNew = newValue;

            UpdateBar();
        }

        private void OnValueChange(float oldValue, float newValue)
        {
            _valueOld = oldValue;
            _valueNew = newValue;

            UpdateBar();
        }

        private void UpdateBar()
        {
            if (!gameObject.activeInHierarchy) return;


            _damaged = _valueOld > _valueNew;


            float ratio = Mathf.Clamp(_valueNew / _valueMaxNew, 0.0f, 1.0f);

            if (direction == Direction.Horizontal)
            {
                RectValue.anchoredPosition = new Vector2(-RectArea.rect.width * (1.0f - ratio), 0);
            }
            else if (direction == Direction.Vertical)
            {
                RectValue.anchoredPosition = new Vector2(0, -RectArea.rect.height * (1.0f - ratio));
            }


            if (_damaged)
            {
                if (_coroutineChanging != null) StopCoroutine(_coroutineChanging);

                _coroutineChanging = StartCoroutine(ChangeRoutine());
            }
            else
            {
                if (_coroutineChanging == null)
                {
                    if (direction == Direction.Horizontal)
                    {
                        RectValueChanged.anchoredPosition = new Vector2(-RectArea.rect.width * (1.0f - ratio), 0);
                    }
                    else if (direction == Direction.Vertical)
                    {
                        RectValueChanged.anchoredPosition = new Vector2(0, -RectArea.rect.height * (1.0f - ratio));
                    }
                }
            }
        }

        private IEnumerator ChangeRoutine()
        {
            if (_damaged && smoothTransition)
            {
                yield return new WaitForSecondsRealtime(_changeWaitingTime);


                if (direction == Direction.Horizontal)
                {
                    while (RectValueChanged.anchoredPosition.x > RectValue.anchoredPosition.x)
                    {
                        RectValueChanged.anchoredPosition = new Vector2(RectValueChanged.anchoredPosition.x - Time.unscaledDeltaTime * _changeTransitionSpeed, 0);

                        yield return null;
                    }
                }
                else if (direction == Direction.Vertical)
                {
                    while (RectValueChanged.anchoredPosition.y > RectValue.anchoredPosition.y)
                    {
                        RectValueChanged.anchoredPosition = new Vector2(0, RectValueChanged.anchoredPosition.y - Time.unscaledDeltaTime * _changeTransitionSpeed);

                        yield return null;
                    }
                }
            }


            RectValueChanged.anchoredPosition = RectValue.anchoredPosition;

            _coroutineChanging = null;
        }



        private RectTransform _rectArea = null;
        private RectTransform RectArea { get => LazyGetComponentFromTransform(ref _rectArea, transform.Find("Value Area")); }

        private RectTransform _rectValue = null;
        private RectTransform RectValue { get => LazyGetComponentFromTransform(ref _rectValue, transform.Find("Value Area/Value")); }

        private RectTransform _rectValueChanged = null;
        private RectTransform RectValueChanged { get => LazyGetComponentFromTransform(ref _rectValueChanged, transform.Find("Value Area/Value Changed")); }



        private bool _damaged = false;

        private float _valueOld = 0.0f;
        private float _valueNew = 0.0f;
        private float _valueMaxNew = 0.0f;

        private float _changeWaitingTime = 1.0f;
        private float _changeTransitionSpeed = 30.0f;

        private Coroutine _coroutineChanging = null;

        private AttributeBase _attributeValue = null;



        public enum Direction
        {
            Horizontal,
            Vertical,
        }



        public enum IdleMode
        {
            None,
            Full,
            Empty,
        }
    }
}