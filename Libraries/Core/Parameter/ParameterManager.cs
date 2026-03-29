using System;



namespace Rune
{
    public class ParameterManager : MonoPlusSingleton<ParameterManager>, ISaveable<ParameterManager.SaveData>
    {
        public static SaveData ExportSaveDataGlobally()
        {
            return SingletonInstance.ExportSaveData();
        }

        public static void ImportSaveDataGlobally(SaveData saveData)
        {
            SingletonInstance.ImportSaveData(saveData);
        }



        public SaveData ExportSaveData()
        {
            return new()
            {
                parameters = Parameters.ExportSaveData(),
            };
        }

        public void ImportSaveData(SaveData saveData)
        {
            Parameters.ImportSaveData(saveData.parameters);
        }



        public static ParameterSet Parameters { get; } = new();
        public static ParameterSet SystemParameters { get; } = new();



        [Serializable]
        public class SaveData
        {
            public ParameterSet.SaveData parameters = new();
        }
    }
}