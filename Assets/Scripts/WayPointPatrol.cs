using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WayPointPatrol : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    //array porque no crece ni decrece , están en el mapa
    public Transform[] waypoints;

    int currentWayPointIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //navegamos al primero
        navMeshAgent.SetDestination(waypoints[0].position);

    }

    // Update is called once per frame
    void Update()
    {
        if(navMeshAgent.remainingDistance<navMeshAgent.stoppingDistance)
		{
            currentWayPointIndex= (currentWayPointIndex+1)%waypoints.Length;
            Debug.Log("Waypoint idx:" + currentWayPointIndex);
            navMeshAgent.SetDestination(waypoints[currentWayPointIndex].position);
        }
    }
}
