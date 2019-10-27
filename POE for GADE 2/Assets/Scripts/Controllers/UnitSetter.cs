using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSetter : MonoBehaviour
{
    public int health, faction, type, damage;
    public float speed,range;
    public bool isDead = false;

    TargetController Target;
    // Start is called before the first frame update
    void Start()
    {
        faction = Random.Range(0, 2);
        type = Random.Range(0, 3);
        SetTeam();
        SetType();
        


    }
    void SetType() //Sets the units to a random unit type
    {
        if (type == 0)
        {
            this.name = "Ranger";
            this.health = 50;
            this.speed = 0.2f;
            this.damage = 10;
            this.range = 5f;
        }
        else
        if (type == 1)
        {
            this.name = "Axeman";
            this.health = 100;
            this.speed = 0.3f;
            this.damage = 25;
            this.range = 1f;
        }
        else if (type == 2)
        {
            this.name = "Wizard";
            this.health = 60;
            this.speed = 0.1f;
            this.tag = "Wizards";
            this.damage = 20;
            this.range = 3f;
        }
    }
    void SetTeam()//then sets the unit to a team, however in the previous method, if their name is Wizard they get set to the team wizards automatically
    {
        if (faction == 0)
        {
            this.tag = "Team 1";
        }
        else
        if (faction == 1)
        {
            this.tag = "Team 2";
        }
        else
        {
            this.tag = "Wizards";
        }

    }

    void Death()
    {
        if (isDead == true)
        {
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
