using System.Collections.Generic;
using System.Linq;
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
                //DebugManager.Log($"Localizer table or entry is empty. (Table: {table}, Entry: {entry})");

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
            var availableLocales = LocalizationSettings.AvailableLocales;

            Locale foundLocale = null;


            /* Steam Language? */


            // Best language
            if (foundLocale == null)
            {
                var selectors = LocalizationSettings.Instance.GetStartupLocaleSelectors();

                foreach (var selector in selectors)
                {
                    foundLocale = selector.GetStartupLocale(availableLocales);
                
                    if (foundLocale != null) break;
                }
            }


            // Check release metadata
            if (foundLocale != null)
            {
                var meta = foundLocale.Metadata.GetMetadata<LocaleReleaseMetadata>();

                if (meta == null || !meta.isReleased) 
                {
                    foundLocale = null;
                }
            }


            // Fallback
            if (foundLocale == null)
            {
                foundLocale = LocalizationSettings.ProjectLocale;
            }
            
            if (foundLocale == null)
            {
                foundLocale = availableLocales.Locales.FirstOrDefault();
            }


            return foundLocale;
        }



        public static List<Locale> Locales
        {
            get
            {
                var availableLocales = LocalizationSettings.AvailableLocales.Locales;

                return availableLocales.Where(l => l.Metadata.GetMetadata<LocaleReleaseMetadata>().isReleased).ToList();
            }
        }

        public static Locale CurrentLocale
        {
            get => LocalizationSettings.SelectedLocale;
            set => LocalizationSettings.SelectedLocale = value;
        }



        public static LooseEvent<Locale> OnChangeLocale { get; } = new();
    }
}