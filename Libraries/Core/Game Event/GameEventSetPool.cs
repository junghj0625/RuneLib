using System.Collections.Generic;



namespace Rune
{
    public class GameEventSetPool : KeyedDataPool<GameEventSet>
    {
        public override void OnAwake()
        {
            base.OnAwake();

            Add(preloadedSOList.ConvertAll(so => GameEventSet.FromSO(so)));
        }



        public List<GameEventDataSetSO> preloadedSOList = new();
    }
}