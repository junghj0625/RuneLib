using System;



namespace Rune
{
    public interface ISaveable<T>
    {
        public T ExportSaveData();

        public void ImportSaveData(T saveData);
    }



    /* Example of ISaveable */

    public class ISaveableExample : ISaveable<ISaveableExample.SaveData>
    {
        public SaveData ExportSaveData()
        {
            var saveData = new SaveData
            {
                /* Do something */
            };

            return saveData;
        }

        public void ImportSaveData(SaveData saveData)
        {
            if (saveData == null) return;

            /* Do something */
        }



        [Serializable]
        public class SaveData
        {

        }
    }
}