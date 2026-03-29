using System;
using TMPro;



namespace Rune.UI
{
    public class TextPlus : UIObject
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _data.OnChange.AddListener(OnChangeData);
        }

        public override void InitObjects()
        {
            base.InitObjects();

            _defaultText = Text.text;
        }

        public override void Refresh()
        {
            base.Refresh();

            _data.Refresh();
        }

        public override void Start()
        {
            base.Start();

            Refresh();
        }



        public BaseData Data
        {
            get => _data.Value;
            set => _data.Value = value;
        }



        private void OnChangeData(BaseData value)
        {
            string textValue = _data.Value.Value;

            if (textValue != null)
            {
                var converter = _data.Value.Converter;

                Text.text = converter != null ? converter(textValue) : textValue;
            }
            else
            {
                Text.text = _defaultText;
            }
        }



        protected TextMeshProUGUI _text = null;
        public TextMeshProUGUI Text { get => LazyGetComponent(ref _text); }



        protected readonly Attribute<BaseData> _data = new(new());

        private string _defaultText = string.Empty;



        public class BaseData
        {
            public string Value { get; set; } = null;

            public Func<string, string> Converter { get; set; } = null;
        }
    }
}