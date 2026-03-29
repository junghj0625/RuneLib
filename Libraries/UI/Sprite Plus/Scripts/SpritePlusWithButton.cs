using System;
using System.Collections.Generic;
using Rune.Pools;
using UnityEngine;
using UnityEngine.UI;



namespace Rune.UI
{
    public class SpritePlusWithButton : UIObject
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _baseData.OnChange.AddListener(OnChangeBaseData);

            _color.OnChange.AddListener(OnChangeColor);

            _material.OnChange.AddListener(OnChangeMaterial);
        }

        public override void InitObjects()
        {
            base.InitObjects();

            Button.onClick.AddListener(OnClick);
        }

        public override void Refresh()
        {
            base.Refresh();

            _baseData.Refresh();

            _color.Refresh();

            _material.Refresh();
        }



        public BaseData Data
        {
            get => _baseData.Value;
            set => _baseData.Value = value;
        }



        private void OnChangeBaseData(BaseData value)
        {
            SpriteRequester.Data = new()
            {
                Sprite = value.Sprite,

                ActionSetSprite = value.ActionSetSprite,
            };

            _color.Value = value.Color;

            _material.Value = value.Material;
        }

        private void OnChangeColor(Color? value)
        {
            if (value != null) Image.color = value.Value;
        }

        private void OnChangeMaterial(MaterialType value)
        {
            if (MaterialPool.TryGet(_materialLookup[value], out var material))
            {
                Image.material = material;
            }
            else
            {
                Image.material = null;
            }
        }

        private void OnClick()
        {
            _baseData.Value.ActionOnClick?.Invoke();
        }



        private SpriteRequester _spriteRequester = null;
        private SpriteRequester SpriteRequester { get => LazyGetComponent(ref _spriteRequester); }

        private Image _image = null;
        public Image Image { get => LazyGetComponent(ref _image); }

        private Button _button = null;
        public Button Button { get => LazyGetComponent(ref _button); }



        private readonly Attribute<BaseData> _baseData = new(new());

        private readonly Attribute<Color?> _color = new(null);

        private readonly Attribute<MaterialType> _material = new(MaterialType.Default);



        public class BaseData
        {
            public string Sprite { get; set; } = null;

            public Color? Color { get; set; } = null;

            public MaterialType Material { get; set; } = MaterialType.Default;

            public Action<Sprite> ActionSetSprite { get; set; } = null;

            public Action ActionOnClick { get; set; } = null;
        }



        public enum MaterialType
        {
            Default,
            Rainbow,
            Grayscale,
        }



        private static readonly Dictionary<MaterialType, string> _materialLookup = new()
        {
            [MaterialType.Default] = null,
            [MaterialType.Rainbow] = "Material:UI Rainbow",
            [MaterialType.Grayscale] = "Material:UI Grayscale",
        };
    }
}