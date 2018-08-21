using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MyPatrol :Action {


    public SharedFloat speed;
 
    public SharedFloat angularSpeed;

    public SharedFloat arriveDistance = 0.1f;

    private EnermyHealth enermyHealth;

    public SharedBool randomPatrol = false;

    public SharedTransformList waypoints;

    private UnityEngine.Animator animator;
    // A cache of the NavMeshAgent
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    // The current index that we are heading towards within the waypoints array
    private int waypointIndex;
    private int HP;
    private int fleeHP;
    public override void OnAwake()
    {
        // cache for quick lookup
        animator = this.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override void OnStart()
    {
        enermyHealth = this.GetComponent<EnermyHealth>();
        HP = enermyHealth.HP;
        fleeHP = enermyHealth.fleeHP;
        animator.SetBool("isInView", false);
        // initially move towards the closest waypoint
        float distance = Mathf.Infinity;
        float localDistance;
        for (int i = 0; i < waypoints.Value.Count; ++i)
        {
            if ((localDistance = Vector3.Magnitude(transform.position - waypoints.Value[i].position)) < distance)
            {
                distance = localDistance;
                waypointIndex = i;
            }
        }

        // set the speed, angular speed, and destination then enable the agent
        navMeshAgent.speed = speed.Value;
        navMeshAgent.angularSpeed = angularSpeed.Value;
        navMeshAgent.enabled = true;
        navMeshAgent.destination = Target();
    }

    // Patrol around the different waypoints specified in the waypoint array. Always return a task status of running. 
    public override TaskStatus OnUpdate()
    {
        if (!navMeshAgent.pathPending)
        {
            var thisPosition = transform.position;
            thisPosition.y = navMeshAgent.destination.y; // ignore y
            if (Vector3.SqrMagnitude(thisPosition - navMeshAgent.destination) < arriveDistance.Value)
            {
                if (randomPatrol.Value)
                {
                    waypointIndex = Random.Range(0, waypoints.Value.Count);
                }
                else
                {
                    waypointIndex = (waypointIndex + 1) % waypoints.Value.Count;
                }
                navMeshAgent.destination = Target();
            }
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        // Disable the nav mesh
        navMeshAgent.enabled = false;
    }

    // Return the current waypoint index position
    private Vector3 Target()
    {
        return waypoints.Value[waypointIndex].position;
    }

    // Reset the public variables
    public override void OnReset()
    {
        arriveDistance = 0.1f;
        waypoints = null;
        randomPatrol = false;
    }

    // Draw a gizmo indicating a patrol 
    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (waypoints == null)
        {
            return;
        }
        var oldColor = UnityEditor.Handles.color;
        UnityEditor.Handles.color = Color.yellow;
        for (int i = 0; i < waypoints.Value.Count; ++i)
        {
            UnityEditor.Handles.SphereCap(0, waypoints.Value[i].position, waypoints.Value[i].rotation, 1);
        }
        UnityEditor.Handles.color = oldColor;
#endif
    }

}
