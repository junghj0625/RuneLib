using System;
using System.Collections;



namespace Rune
{
    public class GameEventLoadScene : GameEvent
    {
        public GameEventLoadScene() : base()
        {
            
        }

        public GameEventLoadScene(GameEventLoadSceneData data) : base(data)
        {
            Scene = data.scene;
        }



        public override IEnumerator Play()
        {
            yield return Utils.Asset.LoadScene(Scene);
        }



        public string Scene { get; set; } = string.Empty;
    }



    [Serializable]
    [GameEvent(typeof(GameEventLoadScene))]
    public class GameEventLoadSceneData : GameEventData
    {
        public string scene;
    }
}