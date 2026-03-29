using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Rune.UI
{
    public partial class Typer : UIObject
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _text.OnChange.AddListener(OnChangeText);
            _fontSize.OnChange.AddListener(OnChangeFontSize);
        }

        public override void Refresh()
        {
            base.Refresh();

            _text.Refresh();
            _fontSize.Refresh();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Stop();
            Clear();
        }


        public void Schedule(List<Command> commands)
        {
            foreach (var command in commands)
            {
                if (command == null) continue;

                _commends.Enqueue(command);
            }

            if (!IsTyping)
            {
                StartProcessingQueue();
            }
        }

        public void Schedule(params Command[] commands)
        {
            Schedule(new List<Command>(commands));
        }

        public void Schedule(string text)
        {
            Schedule
            (
                new CommandClear(),
                new CommandAppend { Text = text }
            );
        }

        public void Stop()
        {
            StopProcessingQueue();

            _commends.Clear();

            IsTyping = false;
            IsSkipped = false;
        }

        public void Clear()
        {
            _text.Value = string.Empty;
        }

        public void Skip()
        {
            IsSkipped = true;
        }

        public IEnumerator Wait()
        {
            yield return new WaitUntil(() => !IsTyping);
        }

        public IEnumerator ScheduleAndWait(List<Command> commands)
        {
            Schedule(commands);

            yield return Wait();
        }

        public IEnumerator ScheduleAndWait(params Command[] commands)
        {
            Schedule(commands);

            yield return Wait();
        }

        public IEnumerator ScheduleAndWait(string text)
        {
            Schedule(text);

            yield return Wait();
        }



        public float Speed { get; set; } = 30.0f;

        public bool AutoSkip { get; set; } = false;
        public bool IsTyping { get; set; } = false;
        public bool IsSkipped { get; set; } = false;

        public LooseEvent<string> OnType { get; } = new();



        protected virtual IEnumerator ProcessQueue()
        {
            if (IsTyping) yield break;

            IsTyping = true;
            IsSkipped = false;

            while (_commends.Count > 0)
            {
                var command = _commends.Dequeue();

                if (command == null) continue;

                command.Owner = this;

                yield return command.Play();
            }

            IsTyping = false;
        }


        protected void StartProcessingQueue()
        {
            if (!gameObject.activeInHierarchy) return;

            StopProcessingQueue();

            StartCoroutine(ProcessQueue());
        }

        protected void StopProcessingQueue()
        {
            if (!gameObject.activeInHierarchy) return;

            StopAllCoroutines();
        }


        protected void OnChangeText(string value)
        {
            TextText.text = value;
        }

        protected void OnChangeFontSize(FontSizeType value)
        {
            TextText.fontSize = _fontSizeLookup[value];
        }



        private TextMeshProUGUI _textText = null;
        protected TextMeshProUGUI TextText { get => LazyGetComponent(ref _textText); }



        protected Attribute<FontSizeType> _fontSize = new(FontSizeType.Normal);
        public FontSizeType FontSize { get => _fontSize.Value; set => _fontSize.Value = value; }



        protected Attribute<string> _text = new(string.Empty);

        protected readonly Queue<Command> _commends = new();



        public enum FontSizeType
        {
            Normal,
            Large,
        }



        private static readonly Dictionary<FontSizeType, int> _fontSizeLookup = new()
        {
            [FontSizeType.Normal] = 8,
            [FontSizeType.Large] = 10,
        };
    }
}