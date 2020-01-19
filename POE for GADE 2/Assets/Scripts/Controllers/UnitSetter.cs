using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSetter : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material[] materials;
    Material material;

    public static GameObject unit = null;
    public static UnitSetter instance;
    public float health, MaxHealth, faction, type, damage;
    public float speed, range;
    public bool isDead = false;
    public bool isDone = false;
    public string UID;
    public Image healthBar;
    public Image unitTypeMelee; //uhhh
    public Image unitTypeWizard; //This is just to make sure
    public Image unitTypeRanger; //Everyone gets an icon :)
    
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
        isDone = true;
        Debug.Log("is loading done? :" + isDone);
    }
    void SetType() //Sets the units to a random unit type
    {
        if (faction == 2) //Wizards can only be on team 2, no traitors allowed!
        {
            this.ChangeMesh(gameObject, PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Sphere));
            this.name = "Wizard";
            this.faction = 2;
            this.MaxHealth = 60;
            this.health = 60;
            this.speed = 0.01f;
            this.tag = "Wizards";
            this.damage = 20;
            this.range = 3f;
            unitTypeMelee.enabled = false;
            unitTypeRanger.enabled = false;
            MaterialSet();
        }
        else if (type == 0)
        {
            this.ChangeMesh(gameObject, PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube));
            this.name = "Ranger";
            this.MaxHealth = 50;
            this.health = 50;
            this.speed = 0.02f;
            this.damage = 10;
            this.range = 5f;
            MaterialSet();
            unitTypeMelee.enabled = false;
            unitTypeWizard.enabled = false;
        }
        else if (type == 1)
        {
            this.name = "Axeman";
            this.MaxHealth = 100;
            this.health = 100;
            this.speed = 0.03f;
            this.damage = 25;
            this.range = 1f;
            MaterialSet();
            unitTypeRanger.enabled = false;
            unitTypeWizard.enabled = false;
        }
        else
        {
            this.ChangeMesh(gameObject, PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube));
            this.name = "Ranger";
            this.MaxHealth = 50;
            this.health = 50;
            this.speed = 0.02f;
            this.damage = 10;
            this.range = 5f;
            MaterialSet();
            unitTypeMelee.enabled = false;
            unitTypeWizard.enabled = false;
        }
        UID = System.Guid.NewGuid().ToString();
    }
    void SetTeam()//then sets the unit to a team, however in the previous method, if their faction is 2 YOU'RE A WIZARD HARRY!
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

    public override string ToString() //Who am I?
    {

        return "UID;" + UID + "\nfaction:" + faction + "\nName: " + name;
    }

    void Death() //Dying is not fun here
    {
        if (this.health <= 0)
        {
            isDead = true;
        }
        if (isDead == true)
        {
            Destroy(this.gameObject);
        }
    }
    private void Awake() //Are you still there?
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
    IEnumerator MaterialSet() //Changing the look of the unit
    {
        Material[] materials = meshRenderer.materials;
        materials[1] = material;
        //materials[1].color = Color.white;
        meshRenderer.materials = materials;
        Debug.Log("Name: " + meshRenderer.materials[1].name);
        yield return new WaitForSeconds(0.1f);
        meshRenderer.materials = materials;
    }
    private void ChangeMesh(GameObject pObject, Mesh pMesh) //Changing the look of the unit
    {
        if (pMesh == null) return;

        Mesh meshInstance = Instantiate(pMesh) as Mesh;

        pObject.GetComponent<MeshFilter>().mesh = meshInstance;
    }
    private void HealthBarCheck()
    {
        healthBar.fillAmount = health / MaxHealth;
        //Debug.Log(this.ToString() + " \nHealthScale:" + health / MaxHealth); Checking the health calcs
    }
}
