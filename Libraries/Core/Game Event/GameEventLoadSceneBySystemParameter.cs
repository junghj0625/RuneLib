using System;
using System.Collections;



namespace Rune
{
    public class GameEventLoadSceneBySystemParameter : GameEvent
    {
        public GameEventLoadSceneBySystemParameter() : base()
        {
            
        }

        public GameEventLoadSceneBySystemParameter(GameEventLoadSceneBySystemParameterData data) : base(data)
        {
            Key = data.key;
        }



        public override IEnumerator Play()
        {
            yield return Utils.Asset.LoadScene(ParameterManager.SystemParameters.GetString(Key));
        }



        public string Key { get; set; } = string.Empty;
    }



    [Serializable]
    [GameEvent(typeof(GameEventLoadSceneBySystemParameter))]
    public class GameEventLoadSceneBySystemParameterData : GameEventData
    {
        public string key;
    }
}