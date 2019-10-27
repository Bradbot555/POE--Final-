using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject prefab;
    GameObject unit;
    public List<GameObject> Targets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Vector3 rndPos = new Vector3(Random.Range(-5f, 5f), Random.Range(0, 0), Random.Range(-5f, 5f));
        for (int i = 0; i < 11; i++)
        {
            unit = Instantiate(prefab, rndPos, Quaternion.identity);
            Targets.Add(unit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
