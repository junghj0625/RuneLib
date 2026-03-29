using System.Collections;



namespace Rune.UI
{
    public partial class Typer
    {
        public abstract class Command
        {
            public abstract IEnumerator Play();



            public Typer Owner { get; set; } = null;
        }
    }
}