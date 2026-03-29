using System;
using UnityEngine.InputSystem;



namespace Rune.Controls
{
    public class Remapper : MonoPlusSingleton<Remapper>
    {
        public static void PerformInteractiveRebindingAndCheckConflict(ControlManager.ActionEntry targetActionEntry, ControlManager.ActionEntry cancelActionEntry, Action<RebindResult> onResult)
        {
            var action = targetActionEntry.Action;
            var cancel = cancelActionEntry.Action;

            action.Disable();


            var remapper = action.PerformInteractiveRebinding();

            if (cancel.controls.Count > 0)
            {
                remapper.WithCancelingThrough(cancel.controls[0]);
            }


            remapper.OnApplyBinding((operation, bindingPath) =>
            {
                ControlManager.ActionEntry foundConflict = null;

                var actionEntries = ControlManager.GetAllActionEntries();

                foreach (var entry in actionEntries)
                {
                    if (!entry.Remapable || entry.Action == action) continue;

                    foreach (var binding in entry.Action.bindings)
                    {
                        if (string.IsNullOrEmpty(binding.effectivePath)) continue;

                        if (binding.effectivePath == bindingPath)
                        {
                            foundConflict = entry;

                            break;
                        }
                    }

                    if (foundConflict != null) break;
                }


                action.Enable();


                onResult?.Invoke(new()
                {
                    target = targetActionEntry,
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

        public static void ApplyBindingOverride(ControlManager.ActionEntry target, string bindingPath)
        {
            target.Action.RemoveAllBindingOverrides();
            target.Action.ApplyBindingOverride(bindingPath);
        }

        public static void RemoveAllBindingOverrides(ControlManager.ActionEntry target)
        {
            target.Action.RemoveAllBindingOverrides();
        }
    }



    public struct RebindResult
    {
        public ControlManager.ActionEntry target;
        public ControlManager.ActionEntry conflict;

        public string bindingPath;
        
        public bool isCancelled;
    }
}