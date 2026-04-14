using System;
using UnityEngine.Localization.Metadata;



namespace Rune.Localization
{
    [Metadata(AllowedTypes = MetadataType.Locale)]
    [Serializable]
    public class LocaleReleaseMetadata : IMetadata
    {
        public bool isReleased;
    }
}