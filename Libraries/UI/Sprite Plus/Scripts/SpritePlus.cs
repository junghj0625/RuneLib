using System;
using System.Collections.Generic;
using Rune.Pools;
using UnityEngine;
using UnityEngine.UI;



namespace Rune.UI
{
    public class SpritePlus : UIObject
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _data.OnChange.AddListener(OnChangeData);

            _color.OnChange.AddListener(OnChangeColor);

            _material.OnChange.AddListener(OnChangeMaterial);
        }

        public override void Refresh()
        {
            base.Refresh();

            _data.Refresh();

            _color.Refresh();

            _material.Refresh();
        }        



        private void OnChangeData(BaseData value)
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



        private SpriteRequester _spriteRequester = null;
        public SpriteRequester SpriteRequester { get => LazyGetComponent(ref _spriteRequester); }

        private Image _image = null;
        public Image Image { get => LazyGetComponent(ref _image); }



        private readonly Attribute<BaseData> _data = new(new());
        public BaseData Data { get => _data.Value; set => _data.Value = value; }

        private readonly Attribute<Color?> _color = new(null);
        public Color? Color { get => _color.Value; set => _color.Value = value; }

        private readonly Attribute<MaterialType> _material = new(MaterialType.Default);
        public MaterialType Material { get => _material.Value; set => _material.Value = value; }



        public class BaseData
        {
            public string Sprite { get; set; } = null;

            public Color? Color { get; set; } = null;

            public MaterialType Material { get; set; } = MaterialType.Default;

            public Action<Sprite> ActionSetSprite { get; set; } = null;
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