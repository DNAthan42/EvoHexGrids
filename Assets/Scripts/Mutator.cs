using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutator : MonoBehaviour
{

    public static float MutationProb = .1f;

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
}
