using System;
using System.Collections;



namespace Rune
{
    public class GameEventLoadSceneByParameter : GameEvent
    {
        public GameEventLoadSceneByParameter() : base()
        {
            
        }

        public GameEventLoadSceneByParameter(GameEventLoadSceneByParameterData data) : base(data)
        {
            Key = data.key;
        }



        public override IEnumerator Play()
        {
            yield return Utils.Asset.LoadScene(ParameterManager.Parameters.GetString(Key));
        }



        public string Key { get; set; } = string.Empty;
    }



    [Serializable]
    [GameEvent(typeof(GameEventLoadSceneByParameter))]
    public class GameEventLoadSceneByParameterData : GameEventData
    {
        public string key;
    }
}