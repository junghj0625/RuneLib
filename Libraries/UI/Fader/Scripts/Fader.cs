using System.Collections;
using UnityEngine;
using UnityEngine.UI;



namespace Rune.UI
{
    public class Fader : UIObject
    {
        public IEnumerator FadeOut(float duration = _defaultDuration)
        {
            gameObject.SetActive(true);

            yield return FadeRoutine(0, 1, duration);
        }

        public IEnumerator FadeIn(float duration = _defaultDuration)
        {
            gameObject.SetActive(true);

            yield return FadeRoutine(1, 0, duration);

            gameObject.SetActive(false);
        }



        public Color Color
        {
            get => Image.color;
            set => Image.color = value;
        }



        private IEnumerator FadeRoutine(float startAlpha, float endAlpha, float duration = _defaultDuration)
        {
            for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
            {
                Image.color = Utils.Math.RGBA(Image.color, Mathf.Lerp(startAlpha, endAlpha, t / duration));

                yield return null;
            }

            Image.color = Utils.Math.RGBA(Image.color, endAlpha);
        }



        private Image _image = null;
        private Image Image { get => LazyGetComponentFromTransform(ref _image, transform.Find("Image")); }



        private const float _defaultDuration = 1.0f;
    }
}