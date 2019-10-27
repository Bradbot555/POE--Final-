using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetController : MonoBehaviour
{
    public float lookRadius = 20f;
    public float stopDistance;
    private float distance;
    private float curDistance;
    public float speed = 1.5f;
    public GameObject Target;
    private GameObject nearestTarget;
    public List<GameObject> Targets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        Move();

    }

    private void Move()
    {
        if (Target == null)
        {
            FindTarget();

            Targets.Clear();
        }
        else if (distance <= lookRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed);
            FaceTarget();
        }
        else if (distance > lookRadius)
        {
            Vector3 direction;
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

        Targets.Add(GameObject.FindGameObjectWithTag("Team 1"));
        Targets.Add(GameObject.FindGameObjectWithTag("Team 2"));
        Targets.Add(GameObject.FindGameObjectWithTag("Wizards"));

        float lowestDist = lookRadius;
        foreach (GameObject target in Targets)
        {
            distance = Vector3.Distance(target.transform.position, transform.position);
            if (target.CompareTag(tag))
            {
                Targets.Remove(target);
            }
            else if (target == this.gameObject)
            {
                Targets.Remove(target);
            }
            else if (target == null)
            {
                Targets.Remove(target);
            }
            else
            if (distance < lowestDist)
            {
                lowestDist = distance;
                Target = target;
            }
        }
        Targets.Clear();
    }

    void FaceTarget()
    {
        Vector3 direction = (Target.transform.position - Target.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
