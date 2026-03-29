using System.Collections.Generic;



namespace Rune
{
    public class HurtArea2D : MonoPlus, IHurtArea2D
    {
        public MonoPlus Owner { get; set; } = null;

        public HashSet<string> Keyworks { get; } = new();
    }



    public interface IHurtArea2D
    {
        public MonoPlus Owner { get; }
        
        public HashSet<string> Keyworks { get; }
    }
}