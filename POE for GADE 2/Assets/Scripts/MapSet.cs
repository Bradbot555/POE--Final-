using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSet : MonoBehaviour
{
    NumberHolder NumberHolder = new NumberHolder();
    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x == 0)
        {
            transform.localScale = new Vector3(10,1,10);
        }
        else if (transform.localScale.z == 0)
        {
            transform.localScale = new Vector3(10, 1, 10);
        }
        else
        transform.localScale = new Vector3(NumberHolder.MapX, 1, NumberHolder.MapY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
