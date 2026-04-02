using System;
using UnityEngine.Localization;



namespace Rune.UI
{
    public class TextPlusLocalized : TextPlus
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _data.OnChange.AddListener(OnChangeData);
        }

        public override void InitObjects()
        {
            base.InitObjects();

            _localizedString.StringChanged += OnChangeLocalizedString;
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

        public override void OnDestroy()
        {
            base.OnDestroy();

            _localizedString.StringChanged -= OnChangeLocalizedString;
        }



        private void OnChangeData(BaseData value)
        {
            _localizedString.TableReference = value.Table;
            _localizedString.TableEntryReference = value.Entry;
        }

        private void OnChangeLocalizedString(string value)
        {
            base._data.Value.Value = Data.UseTag ? TagParser.Parse(value) : value;
            base._data.Refresh();
        }



        private readonly new Attribute<BaseData> _data = new(new());
        public new BaseData Data { get => _data.Value; set => _data.Value = value; }



        private readonly LocalizedString _localizedString = new();



        public new class BaseData
        {
            public string Table { get; set; } = string.Empty;
            public string Entry { get; set; } = string.Empty;

            public bool UseTag { get; set; } = false;

            public Func<string, string> Converter { get; set; } = null;
        }
    }
}