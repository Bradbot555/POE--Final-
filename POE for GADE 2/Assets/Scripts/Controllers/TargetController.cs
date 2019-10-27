using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetController : MonoBehaviour
{
    UnitSetter unitSetter;
    UnitSpawner unitSpawner;
    private int faction;
    private bool inRange = false;
    public float lookRadius = 20f;
    private float distance;
    public float speed = 0.1f;
    public GameObject Target;
    public List<GameObject> Targets;
    // Start is called before the first frame update
    void Start()
    {
        Targets = unitSpawner.Targets;
        faction = unitSetter.faction;
        speed = unitSetter.speed;
        FindTarget();
        _ = Target.GetComponent<UnitSetter>().faction;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        FindTarget();
        Move();
        /*if (unitSetter.range == distance)
        {
            inRange = true;
        }
        if (inRange)
        {
            Attack();
        }
        else */
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(Targets.Count);
            foreach (GameObject go in Targets)
            {
                Debug.Log(go.name);
            }
        }
    }

    private void Move()
    {
        if (Target == null || Target.Equals(null))
        {

            Targets.Clear();
            FindTarget();
        }
        else if (distance <= lookRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed);
            FaceTarget();
        }
        else if (distance > lookRadius)
        {
            // Vector3 direction;
            int wander = Random.Range(0, 4);
            if (wander == 0)
            {
            }
            if (wander == 1)
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }
            if (wander == 2)
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
            if (wander == 3)
            {
                transform.Translate(Vector3.back * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    void FindTarget()
    {
        Vector3 rndPos = new Vector3(Random.Range(-5, 5), Random.Range(0, 0), Random.Range(-5, 5));
        float lowestDist = lookRadius;
        /*  if (this.tag == "Team 1") //Checks the team the current game object is on
          {
              Targets.Add(GameObject.FindGameObjectWithTag("Wizards"));
              Targets.Add(GameObject.FindGameObjectWithTag("Team 2"));
          }
          else if (this.tag == "Team 2")
          {
              Targets.Add(GameObject.FindGameObjectWithTag("Wizards"));
              Targets.Add(GameObject.FindGameObjectWithTag("Team 1"));
          }
          else if (this.tag == "Wizards")
          {
              Targets.Add(GameObject.FindGameObjectWithTag("Team 1"));
              Targets.Add(GameObject.FindGameObjectWithTag("Team 2"));
          } */

        for (int x = Targets.Count - 1; x >= 0; x--) //loops through the List for targets from end of list to begnning as we may remove items
        {
            GameObject unit = Targets[x];
            GetDistance(unit); //Calling Get Distance method so we know the distance between this unit and the temp

            if (Target != null || Target.Equals(Targets[x])) //If the target is already set then break out of loop
            {
                break;
            }
            else
            if (unit.GetComponent<UnitSetter>().faction == this.faction)
            {
                Targets.RemoveAt(x);
            }
            else
            if (Targets[x] == null || Targets[x].Equals(null)) //If the temp unit is equal to nothing then remove it from list
            {
                Targets.Remove(Targets[x]);
                if (unit == null || unit.Equals(null))
                {
                    Targets.Remove(unit);
                }
            }
            else
            if (Targets[x] == this.gameObject) //Checks to see if the object is trying to get itself
            {
                Targets.Remove(Targets[x]);
            }
            else
            if (distance < lowestDist) //Sets the target finally
            {
                lowestDist = distance;
                Target = unit;
            }
        }

        Targets.Clear();
    }
    private void GetDistance(GameObject tempUnit)
    {
        distance = Vector3.Distance(tempUnit.transform.position, transform.position);//Gets the distance between current unit and targeted
    }

    void FaceTarget() //Will not roll and try to face the target which is currently locked on
    {
        Vector3 direction = (Target.transform.position - Target.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void Attack()
    {

    }

}
