using System.Collections;



namespace Rune.UI
{
    public partial class Typer
    {
        public class CommandClear : Command
        {
            public override IEnumerator Play()
            {
                Owner.Clear();

                yield break;
            }
        }
    }
}