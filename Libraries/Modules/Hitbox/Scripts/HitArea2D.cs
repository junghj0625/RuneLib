using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Rune
{
    public class HitArea2D : MonoPlus, IHitArea2D
    {
        public override void Update()
        {
            base.Update();


            _enteredHurtAreas.RemoveWhere(h => h == null);


            if (_pendingEnter.Count > 0)
            {
                foreach (var h in _pendingEnter) _enteredHurtAreas.Add(h);

                _pendingEnter.Clear();
            }

            if (_pendingExit.Count > 0)
            {
                foreach (var h in _pendingExit) _enteredHurtAreas.Remove(h);

                _pendingExit.Clear();
            }


            foreach (var hurtArea in _enteredHurtAreas)
            {
                _processedHurtAreas.Add(hurtArea);

                Hit(hurtArea);
            }
        }


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IHurtArea2D>(out var hurtArea)) return;

            _pendingEnter.Add(hurtArea);

            Enter(hurtArea);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent<IHurtArea2D>(out var hurtArea)) return;

            _pendingExit.Add(hurtArea);

            Exit(hurtArea);
        }


        public void Enter(IHurtArea2D hurtArea)
        {
            if (!IsValidTarget(hurtArea)) return;

            OnEnter.Invoke(hurtArea);
        }

        public void Exit(IHurtArea2D hurtArea)
        {
            if (!IsValidTarget(hurtArea)) return;

            OnExit.Invoke(hurtArea);
        }

        public void Hit(IHurtArea2D hurtArea)
        {
            if (!IsValidTarget(hurtArea)) return;

            OnHit.Invoke(hurtArea);
        }



        public MonoPlus Owner { get; set; } = null;

        public LooseEvent<IHurtArea2D> OnEnter { get; } = new();
        public LooseEvent<IHurtArea2D> OnExit { get; } = new();
        public LooseEvent<IHurtArea2D> OnHit { get; } = new();

        public HashSet<string> TargetKeywords { get; } = new();
        


        private bool IsValidTarget(IHurtArea2D hurtArea)
        {
            return
                hurtArea.Owner != Owner &&
                hurtArea.Keyworks.Any(k => TargetKeywords.Contains(k));
        }



        private readonly HashSet<IHurtArea2D> _enteredHurtAreas = new();
        private readonly HashSet<IHurtArea2D> _processedHurtAreas = new();
        private readonly HashSet<IHurtArea2D> _pendingEnter = new();
        private readonly HashSet<IHurtArea2D> _pendingExit = new();
    }



    public interface IHitArea2D
    {
        public void Enter(IHurtArea2D hurtArea);
        public void Exit(IHurtArea2D hurtArea);
        public void Hit(IHurtArea2D hurtArea);

        public MonoPlus Owner { get; }

        public LooseEvent<IHurtArea2D> OnHit { get; }

        public HashSet<string> TargetKeywords { get; }
    }
}