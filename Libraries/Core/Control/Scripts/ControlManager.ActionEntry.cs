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

                return InputControlPath.ToHumanReadableString
                (
                    Action.bindings[bindingIndex].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice
                );
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



            public string Type { get; set; } = string.Empty;

            public string ActionPath { get; set; } = string.Empty;

            public bool Remapable { get; set; } = false;

            public Func<string> FuncGetText { get; set; } = null;

            public Func<Controller, Action<InputAction.CallbackContext>> FuncGetCallback { get; set; } = null;



            private InputAction _action = null;
        }
    }
}