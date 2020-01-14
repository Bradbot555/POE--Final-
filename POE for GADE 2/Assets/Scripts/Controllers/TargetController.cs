using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetController : MonoBehaviour
{
    public static UnitSetter unitSetter;
    public static UnitSpawner unitSpawner;
    private int faction;
    private bool inRange = false;
    public float lookRadius = 20f;
    private float distance, lowestDist;
    public float currentSpeed, defaultSpeed;
    public GameObject TargetObj;
    public List<GameObject> Targets = new List<GameObject>();
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Debug.Log(UnitSpawner.instance.Targets.Count);
        unitSetter = UnitSetter.instance;
        faction = UnitSetter.instance.faction;
        currentSpeed = UnitSetter.instance.speed;
        defaultSpeed = this.GetComponent<UnitSetter>().speed;
        for (int i = 0; i < UnitSpawner.instance.Targets.Count; i++)
        {
            Targets.Add(UnitSpawner.instance.Targets[i]);
            //Debug.Log("("+this.name+")["+ this.GetComponent<UnitSetter>().faction +"] ("+ Targets[i].GetComponent<UnitSetter>().name +")["+ Targets[i].GetComponent<UnitSetter>().faction+"]");
        }
        yield return new WaitForSeconds(2);
        // FindTarget(); 
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        FindTarget();
        //Move();
        if (this.GetComponent<UnitSetter>().range >= lowestDist)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
        if (inRange)
        {
            Attack();
        }
        else
        {
            Move();
        }

    }

    private void Move()
    {
        defaultSpeed = this.GetComponent<UnitSetter>().speed;
        if (TargetObj == null || TargetObj.Equals(null))
        {
            FindTarget();
        }
        else if (lowestDist <= lookRadius)
        {
            currentSpeed = defaultSpeed;
            transform.position = Vector3.MoveTowards(transform.position, TargetObj.transform.position, defaultSpeed);
            //FaceTarget();
        }
        /*else if (distance > lookRadius)
        {
            // Vector3 direction;
            int wander = Random.Range(0, 4);
            if (wander == 0)
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
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
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    void FindTarget()
    {
        if (unitSetter.isDone == false)
        {
            return;
        }
        lowestDist = lookRadius;

        for (int x = Targets.Count - 1; x >= 0; x--) //loops through the List for targets from end of list to begnning as we may remove items
        {
            GameObject unit = Targets[x];
             //Calling Get Distance method so we know the distance between this unit and the temp
            if ( unit == null || unit.GetComponent<UnitSetter>().isDead == true  || unit.Equals(null)) //If the temp unit is equal to nothing then remove it from list
            {
                Targets.Remove(unit);
              /*  if (unit == null || unit.Equals(null))
                {
                    Targets.Remove(unit);
                } */
                continue;
            }

            GetDistance(unit);

            if (unit.GetComponent<UnitSetter>().faction == this.GetComponent<UnitSetter>().faction) //Faction Checker
            {
                Targets.Remove(unit);
                continue;
            }
            
            if (unit == this.gameObject) //Checks to see if the object is trying to get itself
            {
                Targets.Remove(unit);
                continue;
            }
            if (distance < lowestDist) //Sets the target finally
            {
                lowestDist = distance;
                TargetObj = unit;
                
                if (this.GetComponent<UnitSetter>().faction == unit.GetComponent<UnitSetter>().faction)
                {
                    Debug.Log(this.GetComponent<UnitSetter>().ToString() + "| ==->~} |" + unit.GetComponent<UnitSetter>().ToString());
                }
                
            }
            
        }
    }
    private void GetDistance(GameObject tempUnit)
    {
        distance = Vector3.Distance(tempUnit.transform.position, transform.position);//Gets the distance between current unit and targeted
    }

    /*void FaceTarget() //Will not roll and try to face the target which is currently locked on
    {
        Vector3 direction = (TargetObj.transform.position - TargetObj.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }*/
    void Attack()
    {
        defaultSpeed = 0;
        currentSpeed = 0;
        TargetObj.GetComponent<UnitSetter>().health = TargetObj.GetComponent<UnitSetter>().health - this.gameObject.GetComponent<UnitSetter>().damage;
    }
    //test1234
}
