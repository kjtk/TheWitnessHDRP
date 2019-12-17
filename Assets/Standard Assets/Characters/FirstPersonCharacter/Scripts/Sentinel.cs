using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sentinel : MonoBehaviour {
    public NavMeshAgent ai;
    public Transform[] waypoints;
    int nextWaypoint = -1;

    // Start is called before the first frame update
    void Start() {
        NextWaypoint();

    }

    // Update is called once per frame
    void Update() {

        if (Vector3.Distance(transform.position, waypoints[nextWaypoint].position) < 1) {
           
            NextWaypoint();
        }
    }

    void NextWaypoint() {
        nextWaypoint++;
        if (nextWaypoint >= waypoints.Length) {
            nextWaypoint = 0;
        }
        ai.destination = waypoints[nextWaypoint].position;
    }


}
