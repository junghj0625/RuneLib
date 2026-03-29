using System.Collections;
using UnityEngine;



namespace Rune.UI
{
    public partial class Typer
    {
        public class CommandWait : Command
        {
            public override IEnumerator Play()
            {
                yield return new WaitForSeconds(Duration);
            }



            public float Duration { get; set; } = 0.0f;            
        }
    }
}