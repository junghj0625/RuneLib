using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Rune.Utils
{
    public static class Game
    {
        public static T RandomPick<T>(IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T RandomPick<T>(IEnumerable<T> collection)
        {
            return RandomPick(collection.ToList());
        }

        public static List<T> RandomPick<T>(List<T> list, int n)
        {
            var shuffled = Shuffle(list);

            return shuffled.GetRange(0, n);
        }

        public static T RandomPickOrDefault<T>(IList<T> list)
        {
            if (list.Count == 0) return default;

            return RandomPick(list);
        }

        public static T RandomPickOrDefault<T>(IEnumerable<T> collection)
        {
            return RandomPickOrDefault(collection.ToList());
        }

        public static T RandomPickWeighted<T>(List<(T value, float weight)> list)
        {
            float total = 0f;

            foreach (var (value, weight) in list) total += weight;

            float r = UnityEngine.Random.value * total;

            foreach (var (value, weight) in list)
            {
                r -= weight;

                if (r <= 0f) return value;
            }

            return list[^1].value;
        }

        public static T RandomPickWeighted<T>(IEnumerable<(T value, float weight)> collection)
        {
            return RandomPickWeighted(collection.ToList());
        }

        public static T RandomPickExcept<T>(this IList<T> list, T except)
        {
            if (list == null || list.Count == 0) throw new InvalidOperationException("List is empty.");

            if (list.Count == 1) throw new InvalidOperationException("Cannot exclude the only element.");


            T value;

            do
            {
                value = RandomPick(list);
            }
            while (EqualityComparer<T>.Default.Equals(value, except));


            return value;
        }


        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var list = source.ToList();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);

                (list[i], list[j]) = (list[j], list[i]);
            }

            return list;
        }

        public static List<T> Shuffle<T>(this List<T> source)
        {
            return Shuffle((IEnumerable<T>)source).ToList();
        }


        public static bool Roll(float accuracy)
        {
            return UnityEngine.Random.value < accuracy;
        }


        public static (int nextLevel, int neededExp, int remainExp) ApplyExp(int currentLevel, int currentExp, int gainedExp, Func<int, int> funcGetNextExp)
        {
            int totalExp = currentExp + gainedExp;

            int level = currentLevel;

            while (true)
            {
                int required = funcGetNextExp(level);

                if (totalExp >= required)
                {
                    totalExp -= required;

                    level++;
                }
                else
                {
                    return (level, required, totalExp);
                }
            }
        }


        public static IEnumerator MoveSmooth(Vector3 start, Vector3 end, float duration, bool isClamped, Action<Vector3> action)
        {
            if (duration <= 0f)
            {
                action?.Invoke(end);

                yield break;
            }

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t = isClamped ? Mathf.Clamp01(elapsed / duration) : elapsed / duration;

                float easedT = 1f - Mathf.Pow(1f - t, 3f); // Ease out cubic

                Vector3 current = Vector3.LerpUnclamped(start, end, easedT);

                action?.Invoke(current);

                yield return null;
            }
        }

        public static IEnumerator MoveLinear(Vector3 start, Vector3 end, float duration, bool isClamped, Action<Vector3> action)
        {
            if (duration <= 0f)
            {
                action?.Invoke(end);

                yield break;
            }

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t = isClamped ? Mathf.Clamp01(elapsed / duration) : elapsed / duration;

                Vector3 current = Vector3.LerpUnclamped(start, end, t);
                
                action?.Invoke(current);

                yield return null;
            }
        }



        public enum DirectionType
        {
            Up,
            Down,
            Left,
            Right,
        }
    }
}