using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberHolder 
{
    private static float mapX, mapY;
    public static float MapX
    {
        get
        {
            return mapX;
        }
        set
        {
            mapX = value;
        }
    }
    public static float MapY
    {
        get
        {
            return mapY;
        }
        set
        {
            mapY = value;
        }
    }
}
