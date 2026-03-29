using UnityEngine.Localization;



namespace Rune.Localization
{
    public interface ILocalizable
    {
        public void OnChangeLocale(Locale locale);
    }



    /* Example of ILocalizable */

    public class ILocalizableExample : MonoPlus, ILocalizable
    {
        public override void OnEnable()
        {
            base.OnEnable();

            Localizer.Subscribe(this);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Localizer.Unsubscribe(this);
        }


        public void OnChangeLocale(Locale locale)
        {
            /* Do something */
        }
    }
}