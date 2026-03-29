using System.Collections;
using UnityEngine;



namespace Rune.UI
{
    public partial class Typer
    {
        public class CommandAppend : Command
        {
            public override IEnumerator Play()
            {
                string startText = Owner._text.Value;

                string appendedText = TagParser.Parse(Text);

                float duration = 1.0f / Owner.Speed / SpeedMultiplier;


                float time = 0.0f;

                bool tag = false;

                for (int i = 0; i < appendedText.Length; i++)
                {
                    if (Owner.AutoSkip || Owner.IsSkipped) break;


                    // Write one character
                    char c = appendedText[i];

                    if (c == '<')
                    {
                        tag = true;
                    }
                    else if (c == '>')
                    {
                        tag = false;
                    }

                    Owner._text.Value += appendedText[i];

                    if (tag) continue;


                    // Write multiple characters in wait duration
                    if (duration < Time.deltaTime)
                    {
                        if (time < Time.deltaTime)
                        {
                            time += duration;

                            continue;
                        }
                        else
                        {
                            time -= Time.deltaTime;
                        }
                    }


                    // Wait
                    Owner.OnType.Invoke(Owner._text.Value);

                    yield return new WaitForSeconds(duration);
                }

                Owner._text.Value = startText + appendedText;
            }



            public string Text { get; set; } = string.Empty;

            public float SpeedMultiplier { get; set; } = 1.0f;
        }
    }
}