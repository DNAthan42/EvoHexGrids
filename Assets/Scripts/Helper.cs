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
        {Color.yellow, "y" }
    };

    public static Dictionary<string, Color> StringToColor = new Dictionary<string, Color>()
    {
        { "w" , Color.white },
        { "b" , Color.blue },
        { "g" , Color.green },
        { "y" , Color.yellow }
    };
}
