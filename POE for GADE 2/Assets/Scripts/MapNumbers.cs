using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNumbers : MonoBehaviour
{
    
    public Text MapNumber;
    // Start is called before the first frame update
    void Start()
    {
        MapNumber = GetComponent<Text>();
    }
    public void MapNumberUpdate (float newMapX)
    {
        MapNumber.text = newMapX+"";
    }
    // Update is called once per frame
    void Update()
    {
        MapNumber = GetComponent<Text>();
    }
}
