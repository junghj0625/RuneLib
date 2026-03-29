using UnityEngine;



namespace Rune
{
    public class HitCircle2D : HitArea2D
    {
        private CircleCollider2D _collider = null;
        public CircleCollider2D Collider { get => LazyGetComponent(ref _collider); }
    }
}