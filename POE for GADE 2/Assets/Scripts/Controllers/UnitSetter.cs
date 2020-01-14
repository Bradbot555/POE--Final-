﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSetter : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material[] materials;
    Material material;

    public static GameObject unit = null;
    public static UnitSetter instance;
    public int health, MaxHealth, faction, type, damage;
    public float speed, range;
    public bool isDead = false;
    public bool isDone = false;
    public string UID;
    
    
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
        if (faction == 2)
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
        }
        UID = System.Guid.NewGuid().ToString();
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

    public override string ToString()
    {

        return "UID;" + UID + "\nfaction:" + faction + "\nName: " + name;
    }

    void Death()
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
}
