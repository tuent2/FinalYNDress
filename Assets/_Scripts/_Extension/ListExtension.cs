using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public static class ListExtension
{
    static Random rng = new Random(DateTime.UtcNow.Millisecond);

    public static void Shuffle<T>(this IList<T> list)
    {
        // Random rng = new Random(DateTime.UtcNow.Millisecond);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T GetRandom<T>(this IList<T> list)
    {
        // Random rng = new Random(DateTime.UtcNow.Millisecond);
        // var index = rng.Next(list.Count);
        return list[rng.Next(list.Count)];
    }

    public static T Front<T>(this IList<T> list)
    {
        return list[0];
    }

    public static T Back<T>(this IList<T> list)
    {
        return list[list.Count - 1];
    }

    public static T RemoveFront<T>(this IList<T> list)
    {
        T front = list.Front();
        list.RemoveAt(0);
        return front;
    }

    public static T RemoveBack<T>(this IList<T> list)
    {
        T back = list.Back();
        list.RemoveAt(list.Count - 1);
        return back;
    }

    public static void AddFront<T>(this IList<T> list, T item)
    {
        list.Insert(0, item);
    }

    public static bool IsEmpty<T>(this IList<T> list)
    {
        return list.Count == 0;
    }

    public static bool IsEmpty<T>(this Queue<T> queue)
    {
        return queue.Count == 0;
    }

    public static bool IsEmpty<T>(this Stack<T> stack)
    {
        return stack.Count == 0;
    }

    public static float GetDuration(this AudioClip clip, float defaultDuration = 0)
    {
        try
        {
            return clip.length;
        }
        catch (Exception e)
        {
            //DinoDebugger.LogGameBase("Fail to get duration of audio clip");
            Debug.Log("Fail to get duration of audio clip");
            return defaultDuration;
        }
    }

    public static T RandomUnique<T>(this IList<T> origin, List<T> container)
    {
        // Random rng = new Random(DateTime.UtcNow.Millisecond);
        // var index = rng.Next(list.Count);
        T obj = origin[rng.Next(origin.Count)];

        while (container.Contains(obj))
        {
            obj = origin[rng.Next(origin.Count)];

            if (container.Count == origin.Count)
            {
                return default;
            }
        }

        return obj;
    }
    /// <summary>
    /// Pick index of list unique in container list, Return -1 if cannot pick()
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="origin"></param>
    /// <param name="container">Nơi chứa index random</param>
    /// <returns></returns>
    public static int RandomUniqueIndex<T>(this IList<T> origin, List<T> container)
    {
        // Random rng = new Random(DateTime.UtcNow.Millisecond);
        // var index = rng.Next(list.Count);
        int index = rng.Next(origin.Count);

        while (container.Contains(origin[index]))
        {
            index = rng.Next(origin.Count);

            if (container.Count == origin.Count)
            {
                return -1;
            }
        }

        return index;
    }

    public static string ConvertToString<T>(this IList<T> origin, string sperate = ",")
    {
        string log = "";

        for (int i = 0; i < origin.Count; i++)
        {
            log += i == 0 ? origin[i].ToString() : (sperate + origin[i]);
        }

        return log;
    }

    public static bool SequenceEqualsIgnoreOrder<T>(this List<T> list, List<T> other)
    {
        for (int i = 0; i < other.Count; i++)
        {
            if (list.Contains(other[i]))
            {
                return true;
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (other.Contains(list[i]))
            {
                return true;
            }
        }
        return false;
    }
    public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count)
    {
        var queue = new Queue<T>();

        using (var e = source.GetEnumerator())
        {
            while (e.MoveNext())
            {
                if (queue.Count == count)
                {
                    do
                    {
                        yield return queue.Dequeue();
                        queue.Enqueue(e.Current);
                    } while (e.MoveNext());
                }
                else
                {
                    queue.Enqueue(e.Current);
                }
            }
        }
    }
}