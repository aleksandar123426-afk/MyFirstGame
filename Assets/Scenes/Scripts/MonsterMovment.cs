using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovment : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public int patrolDestination;
    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }


    // Update is called once per frame
    void Update()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        // clamp patrolDestination
        if (patrolDestination < 0) patrolDestination = 0;
        transform.localScale = originalScale;
        if (patrolDestination >= patrolPoints.Length) patrolDestination = patrolPoints.Length - 1;

        // move towards current destination
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position, moveSpeed * Time.deltaTime);

        // if close enough, switch destination (toggle between 0 and 1 if two points)
        if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < 0.2f)
        {
            if (patrolPoints.Length > 1)
              transform.localScale = new Vector3(1, 1, 1);
                patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
        }
    }
}   

    
