using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingSetter : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material[] materials;
    Material material;
    public static BuildingSetter instance;
    public float health, maxHealth, faction, type, production, resources;
    public bool isDead = false;
    public bool isBuilding = true;
    public Image healthBar;
    TargetController Target;
    // Start is called before the first frame update
    void Start()
    {
        //materials = meshRenderer.materials;
        //material = this.meshRenderer.material;
        meshRenderer = this.GetComponent<MeshRenderer>();
        faction = Random.Range(0, 2);
        type = Random.Range(0, 2);
        SetTeam();
        SetType();

    }
    void SetType() //Sets the units to a random unit type
    {
        if (type == 0)
        {
            this.ChangeMesh(gameObject, PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube));
            this.name = "Factory";
            this.production = 50;
            this.maxHealth = 200;
            this.health = 200;
            MaterialSet();
        }
        else if (type == 1)
        {
            this.name = "Resouce Building";
            this.resources = 1000;
            this.maxHealth = 100;
            this.health = 100;
            MaterialSet();
        }
        else
        {
            this.ChangeMesh(gameObject, PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube));
            this.name = "Factory";
            this.production = 50;
            this.maxHealth = 200;
            this.health = 200;
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


    }

    void Death()
    {
        if (resources == 0)
        {
            Destroy(this.gameObject);
        }
        if (isDead == true)
        {
            Destroy(this.gameObject);
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
        HealthBarCheck();
        Death();
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
    private void HealthBarCheck()
    {
        healthBar.fillAmount = health / maxHealth;
        //Debug.Log(this.ToString() + " \nHealthScale:" + health / maxHealth); Debugging the health checks
    }
}
