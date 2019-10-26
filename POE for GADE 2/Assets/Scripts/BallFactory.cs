using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public GameObject ballTemplate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            copyball();
        }
    }

    public void copyball()
    {
        Vector3 position = Vector3.left * Random.Range(-1f, 1f);
        GameObject ballCopy = Instantiate(ballTemplate,position,Quaternion.identity);
        
    }
}
