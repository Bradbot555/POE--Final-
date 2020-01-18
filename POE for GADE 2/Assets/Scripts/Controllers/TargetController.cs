using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetController : MonoBehaviour
{
    public static UnitSetter unitSetter; //Inherit
    public static UnitSpawner unitSpawner; //Inherit 2
    public static NumberHolder numberHolder = new NumberHolder(); //Inherit 3
    private int faction; //Unit's faction
    private float attackDelay = 1.0f; //How long until they attack again
    private float nextDamageEvent; //The event of attacking
    private bool inRange = false; //Are they in range to attack
    public bool isWandering = false;
    public float lookRadius = 200f; //how far they can look
    private float distance, lowestDist;
    public GameObject TargetObj; //The target of this unit
    public List<GameObject> Targets = new List<GameObject>(); //Creating the array
    // Start is called before the first frame update
    IEnumerator Start()
    {
        unitSetter = UnitSetter.instance;
        Debug.Log(UnitSpawner.instance.Targets.Count);
        faction = UnitSetter.instance.faction;
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
            if (Time.time >= nextDamageEvent)
            {
                nextDamageEvent = Time.time + attackDelay;
                Attack();
            }
        }
        else
        {
            nextDamageEvent = Time.time + attackDelay;
            Move();
        }

    }

    private void Move()
    {
        if (TargetObj == null || TargetObj.Equals(null))
        {
            FindTarget();
            /*if (isWandering == false)
            {
                //Wander();             // Wander method is dead....
            }*/
        }
        else if (lowestDist <= lookRadius)
        {
            isWandering = false;
            transform.position = Vector3.MoveTowards(transform.position, TargetObj.transform.position, this.GetComponent<UnitSetter>().speed);
            //FaceTarget();
        }
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
        //isWandering = false;
        transform.position = Vector3.MoveTowards(transform.position, TargetObj.transform.position, 0);
        TargetObj.GetComponent<UnitSetter>().health = TargetObj.GetComponent<UnitSetter>().health - this.gameObject.GetComponent<UnitSetter>().damage;
    }
    
    /*void Wander() // Will fix at a later date
    {
        Vector3 pos = new Vector3(Random.Range(0, NumberHolder.MapX), 1, Random.Range(0, NumberHolder.MapY));
        isWandering = true;
        if (isWandering == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, this.GetComponent<UnitSetter>().speed);
            Debug.Log(this.GetComponent<UnitSetter>().ToString() + " I am wandering to: " + pos);
        }
    }*/

   /* IEnumerator WaitforLoad()
    {
        if (this.GetComponent<UnitSetter>().isDone == false)
        {
            yield return new WaitForSeconds(2);
        }
        else
        {
            
            //currentSpeed = UnitSetter.instance.speed;
        }
    } */
    //test1234
}
