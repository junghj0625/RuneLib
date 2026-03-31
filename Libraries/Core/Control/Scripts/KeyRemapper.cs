using System;
using UnityEngine.InputSystem;



namespace Rune.Controls
{
    public class KeyRemapper : MonoPlusSingleton<KeyRemapper>
    {
        public static void PerformInteractiveRebindingAndCheckConflict(ControlManager.ActionEntry targetActionEntry, int bindingIndex, ControlManager.ActionEntry cancelActionEntry, Action<RebindResult> onResult)
        {
            var action = targetActionEntry.Action;
            var cancel = cancelActionEntry.Action;

            action.Disable();


            var remapper = action.PerformInteractiveRebinding();

            remapper.WithTargetBinding(bindingIndex);

            if (cancel.controls.Count > 0)
            {
                remapper.WithCancelingThrough(cancel.controls[0]);
            }

            remapper.OnApplyBinding((operation, bindingPath) =>
            {
                BindingSlot? foundConflict = null;

                var actionEntries = ControlManager.GetAllActionEntries();

                foreach (var entry in actionEntries)
                {
                    if (!entry.IsRemappable) continue;

                    for (int i = 0; i < entry.Action.bindings.Count; i++)
                    {
                        if (entry.Action == action && bindingIndex == i) continue;

                        var binding = entry.Action.bindings[i];

                        if (string.IsNullOrEmpty(binding.effectivePath)) continue;

                        if (binding.effectivePath == bindingPath)
                        {
                            foundConflict = new() { entry = entry, bindingIndex = i };

                            break;
                        }
                    }

                    if (foundConflict != null) break;
                }


                action.Enable();


                onResult?.Invoke(new()
                {
                    target = new() { entry = targetActionEntry, bindingIndex = bindingIndex },
                    conflict = foundConflict,
                    bindingPath = bindingPath,
                    isCancelled = false,
                });


                operation.Dispose();
            })
            .OnCancel((operation) =>
            {
                action.Enable();


                onResult?.Invoke(new()
                {
                    isCancelled = true,
                }); 


                operation.Dispose();
            })
            .Start();
        }

        public static void ApplyBindingOverride(ControlManager.ActionEntry target, int bindingIndex, string bindingPath)
        {
            target.Action.RemoveBindingOverride(bindingIndex);
            target.Action.ApplyBindingOverride(bindingIndex, bindingPath);
        }

        public static void ApplyBindingOverrideAsNone(ControlManager.ActionEntry target, int bindingIndex)
        {
            target.Action.ApplyBindingOverride(bindingIndex, "<None>");
        }

        public static void RemoveBindingOverride(ControlManager.ActionEntry target, int bindingIndex)
        {
            target.Action.RemoveBindingOverride(bindingIndex);
        }
    }



    public struct RebindResult
    {
        public BindingSlot target;
        public BindingSlot? conflict;

        public string bindingPath;
        
        public bool isCancelled;
    }


    public struct BindingSlot
    {
        public ControlManager.ActionEntry entry;

        public int bindingIndex;
    }
}