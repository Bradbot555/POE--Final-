using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScale : MonoBehaviour
{
    NumberHolder NumberHolder = new NumberHolder();
    public float length { get; set; }
    public float width { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(10,1,10);
    }
    public void AdjustSizeX (float newLength)
    {
        length = newLength;
        
    }
    public void AdjustSizeY (float newWidth)
    {
        width = newWidth;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(length, 1, width);
    }
    private void FixedUpdate()
    {
        transform.localScale = new Vector3(length, 1, width);

        NumberHolder.MapY = width;
        NumberHolder.MapX = length;
    }
    
}
