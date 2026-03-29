using System;
using UnityEngine;
using UnityEngine.UI;



namespace Rune.UI
{
    public class SpriteRequester : MonoPlus, IRequester
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _data.OnChange.AddListener(OnChangeBaseData);

            _sprite.OnChange.AddListener(OnChangeSprite);
        }

        public override void Refresh()
        {
            base.Refresh();

            _data.Refresh();

            _sprite.Refresh();
        }



        public Image Image
        {
            get
            {
                if (_image == null) _image = GetComponent<Image>();

                return _image;
            }
        }
        
        

        public string DefaultSpriteKey = null;



        private void RequestSprite(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            if (Pools.SpritePool.TryGet(key, out Sprite sprite))
            {
                SetSprite(sprite);
            }
            else
            {
                Pools.SpritePool.Request(this, key, (sprite) =>
                {
                    SetSprite(sprite);
                });
            }
        }

        private void SetSprite(Sprite sprite)
        {
            Image.sprite = sprite;
            Image.enabled = Image.sprite != null;

            _data.Value?.ActionSetSprite?.Invoke(sprite);
        }


        private void OnChangeBaseData(BaseData value)
        {
            if (value == null) return;

            _sprite.Value = value.Sprite;
        }

        private void OnChangeSprite(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                RequestSprite(DefaultSpriteKey);
            }
            else
            {
                RequestSprite(value);
            }
        }



        private readonly Attribute<BaseData> _data = new(null);
        public BaseData Data { get => _data.Value; set => _data.Value = value; }



        private readonly Attribute<string> _sprite = new(null);

        private Image _image = null;



        public class BaseData
        {
            public string Sprite { get; set; } = null;

            public Action<Sprite> ActionSetSprite { get; set; } = null;
        }
    }



    /* Example of SpriteRequester user */

    public class SpriteRequesterUserExample : MonoPlus
    {
        public override void InitObjects()
        {
            base.InitObjects();

            _spriteRequester = transform.Find("Sprite Requester").GetComponent<SpriteRequester>();
            _spriteRequester.DefaultSpriteKey = "Sprite";
        }

        public override void Refresh()
        {
            base.Refresh();

            _spriteRequester.Refresh();
        }

        public override void Start()
        {
            base.Start();

            _spriteRequester.Data = new() { Sprite = "Sprite New" };
        }


        
        private SpriteRequester _spriteRequester = null;
    }
}