using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;



namespace Rune.Localization
{
    public partial class Localizer : MonoPlusSingleton<Localizer>
    {
        public override void OnStart()
        {
            base.OnStart();

            LocalizationSettings.SelectedLocaleChanged += (locale) => { OnChangeLocale.Invoke(locale); };
        }


        public static string GetLocalizedText(string table, string entry)
        {
            if (string.IsNullOrEmpty(table) || string.IsNullOrEmpty(entry))
            {
                DebugManager.Log($"Localizer table or entry is empty. (Table: {table}, Entry: {entry})");

                return string.Empty;
            }

            var localizedString = new LocalizedString() { TableReference = table, TableEntryReference = entry };
            
            return localizedString.GetLocalizedString();
        }


        public static void Subscribe(ILocalizable localizable)
        {
            OnChangeLocale.AddListener(localizable.OnChangeLocale);
        }

        public static void Unsubscribe(ILocalizable localizable)
        {
            OnChangeLocale.RemoveListener(localizable.OnChangeLocale);
        }


        public static Locale GetBestLocale()
        {
            var selectors = LocalizationSettings.Instance.GetStartupLocaleSelectors();

            var availableLocales = LocalizationSettings.AvailableLocales;

            Locale foundLocale = null;

            foreach (var selector in selectors)
            {
                foundLocale = selector.GetStartupLocale(availableLocales);

                if (foundLocale != null)
                {
                    break; 
                }
            }

            return foundLocale;
        }



        public static List<Locale> Locales
        {
            get => LocalizationSettings.AvailableLocales.Locales;
        }

        public static Locale CurrentLocale
        {
            get => LocalizationSettings.SelectedLocale;
            set => LocalizationSettings.SelectedLocale = value;
        }



        public static LooseEvent<Locale> OnChangeLocale { get; } = new();
    }
}