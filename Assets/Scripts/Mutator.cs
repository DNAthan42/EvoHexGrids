using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutator : MonoBehaviour
{

    public static float MutationProb = .1f;
    public static int CrossoverPoint = 9;

    public static string Mutate(string source)
    {
        string[] data = source.Split(',');
        string mutated = "";
        for (int i = 0; i < data.Length; i++)
        {
            //don't prepend a comma to the start of the list.
            mutated += (i == 0) ? "" : ",";

            //mutate at prob.
            if (Random.value <= MutationProb)
            {
                string temp = data[i];
                //don't mutate to the same color.
                while (temp == data[i])
                    temp = Helper.ColorToString[HexGrid.colors[(int)Random.Range(0, 4)]];

                mutated += temp;
            }

            //keep this cells values if mutation fails.
            else mutated += data[i];
        }

        return mutated;
    }

    public static string[] Crossover(string first, string second)
    {
        if (first.Length != second.Length)
        {
            throw new System.ArgumentException("Sources do not have an equal format.");
        }

        string[] children = new string[] { "", "" };
        string[] firstData = first.Split(',');
        string[] secondData = second.Split(',');


        // safety measure for large values.
        int k = (CrossoverPoint > firstData.Length || CrossoverPoint < 0) ? CrossoverPoint % firstData.Length : CrossoverPoint;

        for (int i = 0; i < firstData.Length; i++)
        {
            if (i != 0)
            {
                children[0] += ",";
                children[1] += ",";
            }

            if (i < k)
            {
                children[0] += firstData[i];
                children[1] += secondData[i];
            }
            else
            {
                children[0] += secondData[i];
                children[1] += firstData[i];
            }
        }

        return children;
    }
}
