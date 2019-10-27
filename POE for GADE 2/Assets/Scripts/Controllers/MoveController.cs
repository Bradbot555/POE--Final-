using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public int health, faction, speed, type;

    TargetController Target;
    // Start is called before the first frame update
    void Start()
    {
        faction = Random.Range(0, 2);
        type = Random.Range(0, 3);
        SetType();
        SetTeam();


    }
    void SetType()
    {
        if (type == 0)
        {
            this.name = "Ranger";
            this.health = 50;
            this.speed = 2;
        }
        else
        if (type == 1)
        {
            this.name = "Axeman";
            this.health = 100;
            this.speed = 3;
        }
        else if (type == 2)
        {
            this.name = "Wizard";
            this.health = 60;
            this.speed = 1;
            this.tag = "Wizards";
        }
    }
    void SetTeam()
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
    void FaceTarget()
    {
        Vector3 direction = (Target.Target.transform.position - Target.Target.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    // Update is called once per frame
    void Update()
    {
        //FaceTarget();
        //transform.position = Vector3.MoveTowards(transform.position, Target.Target.transform.position, speed);
    }
}
