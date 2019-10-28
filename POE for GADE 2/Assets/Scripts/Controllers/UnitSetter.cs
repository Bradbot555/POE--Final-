using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSetter : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material[] materials;
    Material material;

    public static GameObject unit = null;
    public static UnitSetter instance;
    public int health, faction, type, damage;
    public float speed, range;
    public bool isDead = false;

    TargetController Target;
    // Start is called before the first frame update
    void Start()
    {
        //materials = meshRenderer.materials;
        //material = this.meshRenderer.material;
        meshRenderer = this.GetComponent<MeshRenderer>();
        faction = Random.Range(0, 3);
        type = Random.Range(0, 2);
        SetTeam();
        SetType();

    }
    void SetType() //Sets the units to a random unit type
    {
        if (faction == 2)
        {
            this.ChangeMesh(gameObject, PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Sphere));
            this.name = "Wizard";
            this.faction = 2;
            this.health = 60;
            this.speed = 0.1f;
            this.tag = "Wizards";
            this.damage = 20;
            this.range = 3f;
            MaterialSet();
        }
        else if (type == 0)
        {
            this.ChangeMesh(gameObject, PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube));
            this.name = "Ranger";
            this.health = 50;
            this.speed = 0.2f;
            this.damage = 10;
            this.range = 5f;
            MaterialSet();
        }
        else if (type == 1)
        {
            this.name = "Axeman";
            this.health = 100;
            this.speed = 0.3f;
            this.damage = 25;
            this.range = 1f;
            MaterialSet();
        }
        else
        {
            this.ChangeMesh(gameObject, PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube));
            this.name = "Ranger";
            this.health = 50;
            this.speed = 0.2f;
            this.damage = 10;
            this.range = 5f;
            MaterialSet();
        }
    }
    void SetTeam()//then sets the unit to a team, however in the previous method, if their name is Wizard they get set to the team wizards automatically
    {
        if (faction == 0)
        {
            this.tag = "Team 1";
            meshRenderer.material.SetColor("_Color", Color.green);
        }
        else
        if (faction == 1)
        {
            this.tag = "Team 2";
            //materials[1].color = Color.red;
            meshRenderer.material.SetColor("_Color", Color.red);
        }
        else if (faction == 2)
        {
            //materials[1].color = Color.blue;
            meshRenderer.material.SetColor("_Color", Color.blue);
        }


    }

    void Death()
    {
        if (isDead == true)
        {
            Destroy(this);
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
    IEnumerator MaterialSet()
    {
        Material[] materials = meshRenderer.materials;
        materials[1] = material;
        //materials[1].color = Color.white;
        meshRenderer.materials = materials;
        Debug.Log("Name: " + meshRenderer.materials[1].name);
        yield return new WaitForSeconds(0.1f);
        meshRenderer.materials = materials;
    }
    private void ChangeMesh(GameObject pObject, Mesh pMesh)
    {
        if (pMesh == null) return;

        Mesh meshInstance = Instantiate(pMesh) as Mesh;

        pObject.GetComponent<MeshFilter>().mesh = meshInstance;
    }
}
