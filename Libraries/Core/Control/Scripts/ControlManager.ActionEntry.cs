using System;
using UnityEngine.InputSystem;



namespace Rune.Controls
{
    public partial class ControlManager
    {
        public class ActionEntry
        {
            public string GetDevice(int bindingIndex)
            {
                if (bindingIndex < 0 || bindingIndex >= Action.bindings.Count) return string.Empty;

                Action.GetBindingDisplayString(bindingIndex, out string device, out string control);

                return device;
            }

            public string GetControl(int bindingIndex)
            {
                if (bindingIndex < 0 || bindingIndex >= Action.bindings.Count) return string.Empty;

                return GetControlTextFromBindingPath(Action.bindings[bindingIndex].effectivePath);
            }



            public string Text
            {
                get => FuncGetText?.Invoke();
            }

            public string Device
            {
                get => GetDevice(0);
            }

            public string Control
            {
                get => GetControl(0);
            }

            public InputAction Action
            {
                get
                {
                    _action ??= InputSystem.actions.FindAction(ActionPath);

                    return _action;
                }
            }



            public bool IsRemappable { get; set; } = false;

            public string Type { get; set; } = string.Empty;
            public string ActionPath { get; set; } = string.Empty;

            public Func<string> FuncGetText { get; set; } = null;

            public Func<Controller, Action<InputAction.CallbackContext>> FuncGetCallback { get; set; } = null;



            private InputAction _action = null;
        }
    }
}