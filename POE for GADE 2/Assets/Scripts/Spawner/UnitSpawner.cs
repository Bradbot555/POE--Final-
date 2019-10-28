using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner instance;

    NumberHolder NumberHolder = new NumberHolder();
    public GameObject prefab;
    public static GameObject unit = null;
    public List<GameObject> Targets = new List<GameObject>();
    public float MapX;
    public float MapY;
    // Start is called before the first frame update
    void Start()
    {
        MapX = NumberHolder.MapX / 2;
        MapY = NumberHolder.MapY / 2;
        //Vector3 rndPos = new Vector3(Random.Range(-5f, 5f), Random.Range(1, 1), Random.Range(-5f, 5f));
        for (int i = 0; i < 11; i++)
        {
            unit = Instantiate(prefab, new Vector3(Random.Range(-MapX, MapX), 1, Random.Range(-MapY, MapY)), Quaternion.identity);
            Targets.Add(unit);
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
