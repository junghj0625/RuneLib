using UnityEngine;



namespace Rune
{
    public abstract class AttributeBase
    {
        public abstract void Refresh();

        public abstract void Reset();
    }



    public class Attribute<T> : AttributeBase
    {
        public Attribute(T defaultValue = default)
        {
            _value = defaultValue;

            _defaultValue = defaultValue;
        }



        public override void Refresh()
        {
            SetValue(_value);
        }

        public override void Reset()
        {
            SetValue(_defaultValue);
        }


        public virtual void SetValue(T value)
        {
            var valueOld = _value;
            var valueNew = value;

            _value = value;

            OnChange?.Invoke(valueNew);

            OnChangeOldToNew?.Invoke(valueOld, valueNew);
        }

        public virtual void SetValueWithoutNotify(T value)
        {
            _value = value;
        }

        public virtual void SetDefaultValue(T defaultValue)
        {
            _defaultValue = defaultValue;
        }



        public T Value
        {
            get => _value;
            set => SetValue(value);
        }

        public T DefaultValue
        {
            get => _defaultValue;
            set => SetDefaultValue(value);
        }



        public LooseEvent<T> OnChange { get; } = new();

        public LooseEvent<T, T> OnChangeOldToNew { get; } = new();



        protected T _value;
        protected T _defaultValue;
    }



    public class AttributeClampedInt : Attribute<int>
    {
        public AttributeClampedInt(int defaultValue = default, int valueMin = int.MinValue, int valueMax = int.MaxValue)
        {
            _valueMin = valueMin;
            _valueMax = valueMax;

            _defaultValue = Mathf.Clamp(defaultValue, _valueMin, _valueMax);

            _value = _defaultValue;
        }



        public override void SetValue(int value)
        {
            int valueOld = _value;
            int valueNew = Mathf.Clamp(value, _valueMin, _valueMax);

            _value = valueNew;

            OnChange?.Invoke(valueNew);

            OnChangeOldToNew?.Invoke(valueOld, valueNew);
        }

        public override void SetValueWithoutNotify(int value)
        {
            _value = Mathf.Clamp(value, _valueMin, _valueMax);
        }

        public override void SetDefaultValue(int defaultValue)
        {
            _defaultValue = Mathf.Clamp(defaultValue, _valueMin, _valueMax);
        }


        public AttributeClampedInt SetValueMin(int value)
        {
            _valueMin = value;

            SetValue(_value);

            return this;
        }

        public AttributeClampedInt SetValueMax(int value)
        {
            _valueMax = value;

            SetValue(_value);

            return this;
        }

        public AttributeClampedInt SetValueMinMax(int min, int max)
        {
            _valueMin = min;
            _valueMax = max;

            SetValue(_value);

            return this;
        }


        public void SetValueToMax()
        {
            SetValue(_valueMax);
        }

        public void SetValueToMin()
        {
            SetValue(_valueMin);
        }



        public int ValueMin
        {
            get => _valueMin;
            set => SetValueMin(value);
        }

        public int ValueMax
        {
            get => _valueMax;
            set => SetValueMax(value);
        }



        private int _valueMin = int.MinValue;
        private int _valueMax = int.MaxValue;
    }



    public class AttributeClampedFloat : Attribute<float>
    {
        public AttributeClampedFloat(float defaultValue = default, float valueMin = float.MinValue, float valueMax = float.MaxValue)
        {
            _valueMin = valueMin;
            _valueMax = valueMax;

            _defaultValue = Mathf.Clamp(defaultValue, _valueMin, _valueMax);

            _value = _defaultValue;
        }



        public override void SetValue(float value)
        {
            float valueOld = _value;
            float valueNew = Mathf.Clamp(value, _valueMin, _valueMax);

            _value = valueNew;

            OnChange?.Invoke(valueNew);

            OnChangeOldToNew?.Invoke(valueOld, valueNew);
        }

        public override void SetValueWithoutNotify(float value)
        {
            _value = Mathf.Clamp(value, _valueMin, _valueMax);
        }

        public override void SetDefaultValue(float defaultValue)
        {
            _defaultValue = Mathf.Clamp(defaultValue, _valueMin, _valueMax);
        }


        public AttributeClampedFloat SetValueMin(float value)
        {
            _valueMin = value;

            SetValue(_value);

            return this;
        }

        public AttributeClampedFloat SetValueMax(float value)
        {
            _valueMax = value;

            SetValue(_value);

            return this;
        }

        public AttributeClampedFloat SetValueMinMax(float min, float max)
        {
            _valueMin = min;
            _valueMax = max;

            SetValue(_value);

            return this;
        }


        public void SetValueToMax()
        {
            SetValue(_valueMax);
        }

        public void SetValueToMin()
        {
            SetValue(_valueMin);
        }



        public float ValueMin
        {
            get => _valueMin;
            set => SetValueMin(value);
        }

        public float ValueMax
        {
            get => _valueMax;
            set => SetValueMax(value);
        }

        public float Ratio
        {
            get => Mathf.InverseLerp(_valueMin, _valueMax, _value);
        }



        private float _valueMin = float.MinValue;
        private float _valueMax = float.MaxValue;
    }
}