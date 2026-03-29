using System;
using UnityEngine;
using UnityEngine.EventSystems;



namespace Rune.Utils
{
    public readonly struct Object
    {
        public static void SetParentAndResetTransform(GameObject child, Transform parent)
        {
            child.transform.SetParent(parent);

            child.transform.localPosition = Vector3.zero;
            child.transform.localEulerAngles = Vector3.zero;
            child.transform.localScale = Vector3.one;
        }


        public static GameObject InstantiateAsSibling(GameObject original, bool active = true)
        {
            if (original == null) return null;

            var instance = UnityEngine.Object.Instantiate(original, original.transform.parent);

            instance.SetActive(active);

            return instance;
        }

        public static GameObject InstantiateTemplate(GameObject template, bool active = true)
        {
            var instance = InstantiateAsSibling(template);

            if (instance == null) return null;

            instance.SetActive(active);

            return instance;
        }

        public static GameObject InstantiateTemplate(GameObject template, Transform parent, bool active = true)
        {
            if (template == null) return null;

            var instance = UnityEngine.Object.Instantiate(template, parent);

            instance.SetActive(active);

            return instance;
        }

        public static T InstantiateTemplate<T>(GameObject template, bool active = true) where T : MonoBehaviour
        {
            var clone = InstantiateTemplate(template, active);

            if (clone == null) return null;

            if (!clone.TryGetComponent<T>(out var component)) return null;

            return component;
        }


        public static string GetFullPath(Transform obj)
        {
            string path = "/" + obj.name;

            while (obj.parent != null)
            {
                obj = obj.parent;

                path = "/" + obj.name + path;
            }

            return path;
        }


        public static void AddEventTriggerEntry(EventTrigger eventTrigger, EventTriggerType triggerType, Action<BaseEventData> action)
        {
            var entry = new EventTrigger.Entry { eventID = triggerType };

            entry.callback.AddListener((e) => action(e));

            eventTrigger.triggers.Add(entry);
        }
    }
}