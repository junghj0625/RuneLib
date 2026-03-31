using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;



namespace Rune.Controls
{
    public partial class ControlManager : MonoPlusSingleton<ControlManager>
    {
        public static void AddActionEntries(List<ActionEntry> actionEntries)
        {
            foreach (var actionEntry in actionEntries) _actionEntries.Add(actionEntry.Type, actionEntry);

            foreach (var registeredController in _registeredControllers)
            {
                RemoveControllerCallbacks(registeredController);

                AddControllerCallbacks(registeredController);
            }
        }


        public static void RegisterController(Controller controller)
        {
            if (controller == null) return;

            _registeredControllers.Add(controller);

            RemoveControllerCallbacks(controller);

            AddControllerCallbacks(controller);
        }

        public static void UnregisterController(Controller controller)
        {
            if (controller == null) return;

            _registeredControllers.Remove(controller);

            RemoveControllerCallbacks(controller);
        }


        public static ActionEntry GetActionEntry(string actionType)
        {
            if (string.IsNullOrEmpty(actionType)) return null;

            return _actionEntries.ContainsKey(actionType) ? _actionEntries[actionType] : null;
        }


        public static string GetActionText(string actionType)
        {
            var actionEntry = GetActionEntry(actionType);

            if (actionEntry == null) return null;

            return actionEntry.FuncGetText();
        }

        public static string GetControlText(string control)
        {
            return control switch
            {
                "Up Arrow" => "↑",
                "Down Arrow" => "↓",
                "Left Arrow" => "←",
                "Right Arrow" => "→",
                "Space" => "␣",
                "Escape" => "ESC",
                "Control" => "Ctrl",
                _ => control,
            };
        }

        public static string GetControlTextFromBindingPath(string bindingPath)
        {
            return InputControlPath.ToHumanReadableString(bindingPath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }

        public static string GetMouseButtonIcon(string control)
        {
            return _mouseIconLookup.GetValueOrDefault(control);
        }

        


        public static List<ActionEntry> GetAllActionEntries()
        {
            return _actionEntries.Values.ToList();
        }



        private static void AddCallback(string actionType, Action<InputAction.CallbackContext> callback)
        {
            if (callback == null) return;

            var action = GetActionEntry(actionType).Action;

            action.started += callback;
            action.performed += callback;
            action.canceled += callback;
        }

        private static void RemoveCallback(string actionType, Action<InputAction.CallbackContext> callback)
        {
            if (callback == null) return;

            var action = GetActionEntry(actionType).Action;

            action.started -= callback;
            action.performed -= callback;
            action.canceled -= callback;
        }


        private static void AddControllerCallbacks(Controller controller)
        {
            foreach (var actionEntry in _actionEntries.Values) AddCallback(actionEntry.Type, actionEntry.FuncGetCallback(controller));
        }

        private static void RemoveControllerCallbacks(Controller controller)
        {
            foreach (var actionEntry in _actionEntries.Values) RemoveCallback(actionEntry.Type, actionEntry.FuncGetCallback(controller));
        }



        private static readonly HashSet<Controller> _registeredControllers = new();

        private static readonly Dictionary<string, ActionEntry> _actionEntries = new();

        private static readonly Dictionary<string, string> _mouseIconLookup = new()
        {
            ["LMB"] = "Icon:Mouse Buttons Left",
            ["RMB"] = "Icon:Mouse Buttons Right",
            ["MMB"] = "Icon:Mouse Buttons Middle",
            ["Forward"] = "Icon:Mouse Buttons Forward",
            ["Back"] = "Icon:Mouse Buttons Back",
        };
    }
}