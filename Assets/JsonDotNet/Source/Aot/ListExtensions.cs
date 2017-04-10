using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        Random rng = new Random();
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


    public static List<T> Condense<T>(this List<List<T>> list)
    {
        List<T> final = new List<T>();

        foreach (List<T> innerList in list)
        {
            foreach (T element in innerList)
                final.Add(element);
        }

        return final;
    }

    public static List<IndexAndContent> AvailableChoices(this List<IndexAndContent> list)
    {
        List<IndexAndContent> final = new List<IndexAndContent>();
        foreach (IndexAndContent i in list)
        {
            if (!i.Picked())
                final.Add(i);
            else
            {
                // Debug.Log(i.content + " has already been picked");
            }
        }

        return final;
    }

    public static List<IndexAndContent> SetChoiceAsPicked(this List<IndexAndContent> list, IndexAndContent ic)
    {
        foreach (IndexAndContent i in list)
        {
            if (i.content == ic.content && i.index == ic.index)
            {
                i.SetAsPicked();
            }

        }

        return list;
    }

}


