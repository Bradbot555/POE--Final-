using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public static BuildingSpawner instance;

    NumberHolder NumberHolder = new NumberHolder();
    public GameObject prefab;
    public static GameObject building = null;
    public List<GameObject> Targets = new List<GameObject>();
    public float MapX;
    public float MapY;
    // Start is called before the first frame update
    void Start()
    {
        if (MapX < 10 || MapX == 0)
        {
            MapX = 10;
        }
        if (MapY < 10 || MapX == 0)
        {
            MapY = 10;
        }
        MapX = NumberHolder.MapX / 2;
        MapY = NumberHolder.MapY / 2;
        //Vector3 rndPos = new Vector3(Random.Range(-5f, 5f), Random.Range(1, 1), Random.Range(-5f, 5f));
        for (int i = 0; i < 11; i++)
        {
            building = Instantiate(prefab, new Vector3(Random.Range(-MapX, MapX), 1, Random.Range(-MapY, MapY)), Quaternion.identity);
            Targets.Add(building);
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
