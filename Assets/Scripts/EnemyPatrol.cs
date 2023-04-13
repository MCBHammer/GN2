using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private Transform[] patrolPoints;
    private int patrolDestination = 0;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position, patrolSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < .2f)
        {
            patrolDestination += 1;
            if(patrolDestination + 1 > patrolPoints.Length)
            {
                patrolDestination = 0;
            }
        }
    }
}
