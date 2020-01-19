using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphereController : MonoBehaviour
{
    /*
     * This was supposed to be the wizard AOE attack
     * But things got complicated with that
     * So now everything is handled by the target script
     * this can be marked as obsolete
     */
    // Start is called before the first frame update
    public List<Collider> TriggerList = new List<Collider>();
    Collider closestEnemy;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!TriggerList.Contains(other))
        {
            TriggerList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TriggerList.Contains(other))
        {
            TriggerList.Remove(other);
        }
    }
    Transform GetClosestEnemy(List<Transform> enemies, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
    // Update is called once per frame
    void Update()
    {
       // closestEnemy = GetClosestEnemy(TriggerList, this.transform);
    }
}
