using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSet : MonoBehaviour
{
    static public NumberHolder NumberHolder = new NumberHolder();
    bool onetime = false;
    // Start is called before the first frame update
    void Start()
    {
        MapCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onetime)
        {
            MapCheck();
            onetime = true;
        }
    }

    public void MapCheck()
    {
        if (transform.localScale.x <= 0)
        {
            transform.localScale = new Vector3(10, 1, 10);
            NumberHolder.MapX = 10;
        }
        else if (transform.localScale.z <= 0)
        {
            transform.localScale = new Vector3(10, 1, 10);
            NumberHolder.MapY = 10;
        }
        else
            transform.localScale = new Vector3(NumberHolder.MapX, 1, NumberHolder.MapY);
    }
}
