using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingComp : MonoBehaviour
{
    [SerializeField] GameObject[] PatrollingPoints;
    private int NextPatrollingCompIndex = 0;

    public GameObject GetNextPatrolPoint()
    {
        if(PatrollingPoints.Length > NextPatrollingCompIndex)
        {
            GameObject patrolPoint = PatrollingPoints[NextPatrollingCompIndex];
            NextPatrollingCompIndex = (NextPatrollingCompIndex + 1) % PatrollingPoints.Length;
            return patrolPoint;
        }
        return null;
    }
}
