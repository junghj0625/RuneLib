using System.Collections.Generic;
using UnityEngine;



namespace Rune
{
    [CreateAssetMenu(fileName = "Game Event Data Set SO", menuName = "Rune/Game Event Data Set SO")]
    public class GameEventDataSetSO : ScriptableObject
    {
        public string key;
        
        [SerializeReference, SubclassSelector]
        public List<GameEventData> gameEvents = new();
    }
}