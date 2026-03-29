using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Rune
{
    public class GameEventIf : GameEvent
    {
        public GameEventIf() : base()
        {
            
        }

        public GameEventIf(GameEventIfData data) : base(data)
        {
            Condition = ConditionFactory.Create(data.condition);

            TrueThen = new(data.trueThen.Select(t => FromData(t)).ToList());
            
            FalseThen = new(data.falseThen.Select(t => FromData(t)).ToList());
        }



        public bool Result
        {
            get => Condition == null || Condition.Result;
        }



        public Condition Condition { get; set; } = null;

        public List<GameEvent> TrueThen { get; set; } = new();
        public List<GameEvent> FalseThen { get; set; } = new();
    }



    [Serializable]
    [GameEvent(typeof(GameEventIf))]
    public class GameEventIfData : GameEventData
    {
        [SerializeReference, SubclassSelector]
        public ConditionData condition = null;

        [SerializeReference, SubclassSelector]
        public List<GameEventData> trueThen = new();
        
        [SerializeReference, SubclassSelector]
        public List<GameEventData> falseThen = new();
    }
}