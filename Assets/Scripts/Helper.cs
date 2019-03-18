using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static Dictionary<Color, string> ColorToString = new Dictionary<Color, string>()
    {
        {Color.white, "w" },
        {Color.blue, "b" },
        {Color.green, "g" },
        {Color.yellow, "y" },
        {Color.red, "r" }
    };

    public static Dictionary<string, Color> StringToColor = new Dictionary<string, Color>()
    {
        { "w" , Color.white },
        { "b" , Color.blue },
        { "g" , Color.green },
        { "y" , Color.yellow },
        { "r", Color.red }
    };

    public static Color RandomColor()
    {
        float rand = Random.value;
        int i;
        if (rand < .1) i = 0;       //P(w) = .1
        else if (rand < .3) i = 1;  //P(b) = .2
        else if (rand < .7) i = 2;  //P(g) = .4
        else if (rand < .9) i = 3;  //P(y) = .2
        else i = 4;                 //P(r) = .1
        return HexGrid.colors[i];
    }
}
