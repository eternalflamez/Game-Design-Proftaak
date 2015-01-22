using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class BubbleSort : MonoBehaviour
{
    /// <summary>
    /// An unsorted array of integers
    /// </summary>
    private static List<ScoreModel> unsorted;

    /// <summary>
    /// Indicates whether or not the array was changed since the last iteration
    /// </summary>
    private static bool changed = false;

    /// <summary>
    /// The highest point to iterate to
    /// </summary>
    private static int maxPoint;

    public static List<ScoreModel> Sort(List<ScoreModel> unsorted)
    {
        maxPoint = unsorted.Count;

        int index = 0;
        while (index != -1)
        {
            index = SolvePair(index);
        }

        return unsorted;
    }

    /// <summary>
    /// Sorts a pair of numbers, the current index, and the number after that
    /// </summary>
    /// <param name="index">The current index to sort</param>
    /// <returns>The new index to solve</returns>
    private static int SolvePair(int index)
    {
        if (index + 1 != maxPoint)
        {
            if (unsorted[index].getScore() > unsorted[index + 1].getScore())
            {
                ScoreModel first = unsorted[index];
                unsorted[index] = unsorted[index + 1];
                unsorted[index + 1] = first;
                changed = true;
            }

            return ++index;
        }
        else
        {
            if (changed)
            {
                maxPoint--;
                changed = false;
                return 0;
            }

            return -1;
        }
    }
}