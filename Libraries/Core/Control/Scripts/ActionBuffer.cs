using System;
using System.Collections.Generic;
using UnityEngine;



namespace Rune.Controls
{
    public class ActionBuffer : MonoPlusModule
    {
        public override void Update()
        {
            base.Update();

            ProcessInputBuffer();
        }


        public void Add(Action action)
        {
            // Play action now
            if (_buffer.Count == 0 && CanAct)
            {
                action?.Invoke();

                return;
            }


            // Dequeue old action
            if (_buffer.Count >= MaxBufferSize) _buffer.Dequeue();


            // Enqueue new action
            _buffer.Enqueue(new() { Action = action, Time = Time.time });
        }

        public void Clear()
        {
            _buffer.Clear();
        }



        public bool CanAct
        {
            get => FuncCanAct == null || FuncCanAct();
        }



        public int MaxBufferSize { get; set; } = 1;

        public float BufferDuration { get; set; } = 0.1f;

        public Func<bool> FuncCanAct { get; set; } = null;



        private void ProcessInputBuffer()
        {
            while (_buffer.Count > 0)
            {
                if (!CanAct) break;

                var bufferedAction = _buffer.Dequeue();

                if (bufferedAction.Time + BufferDuration < Time.time) continue;

                bufferedAction.Action?.Invoke();

                continue;
            }
        }



        private readonly Queue<BufferedAction> _buffer = new();



        public class BufferedAction
        {
            public Action Action { get; set; } = null;

            public float Time { get; set; } = 0.0f;
        }
    }
}