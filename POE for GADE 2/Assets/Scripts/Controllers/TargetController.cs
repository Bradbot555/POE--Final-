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
    private float distance;
    public float speed = 0.1f;
    public GameObject gameObj;
    public List<GameObject> Targets = new List<GameObject>();
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Debug.Log(UnitSpawner.instance.Targets.Count);
        for (int i = 0; i < UnitSpawner.instance.Targets.Count; i++)
        {
            Targets.Add(UnitSpawner.instance.Targets[i]);
            Debug.Log(Targets[i]);
        }
        yield return new WaitForEndOfFrame();
        faction = UnitSetter.instance.faction;
        speed = UnitSetter.instance.speed;
        FindTarget();
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
    }

    private void Move()
    {
        if (gameObj == null || gameObj.Equals(null))
        {
            FindTarget();
        }
        else if (distance <= lookRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, gameObj.transform.position, speed);
            FaceTarget();
        }
        else if (distance > lookRadius)
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
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    void FindTarget()
    {
        float lowestDist = lookRadius;

        for (int x = Targets.Count - 1; x >= 0; x--) //loops through the List for targets from end of list to begnning as we may remove items
        {
            GameObject unit = Targets[x];
            GetDistance(unit); //Calling Get Distance method so we know the distance between this unit and the temp

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
                gameObj = unit;
            }
        }
    }
    private void GetDistance(GameObject tempUnit)
    {
        distance = Vector3.Distance(tempUnit.transform.position, transform.position);//Gets the distance between current unit and targeted
    }

    void FaceTarget() //Will not roll and try to face the target which is currently locked on
    {
        Vector3 direction = (gameObj.transform.position - gameObj.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void Attack()
    {

    }

}
