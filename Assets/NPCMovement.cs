using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Assign in the Inspector
    public NavMeshAgent agent;
    private int currentWaypoint = 0;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {

            // Move to the next waypoint
            agent.SetDestination(waypoints[currentWaypoint].position);
            float speed = agent.velocity.magnitude;
            // Wait until NPC reaches the waypoint
            while (agent.pathPending || agent.remainingDistance > 0.5f)
            {
                speed = agent.velocity.magnitude;
                animator.SetFloat("Speed", speed); // Sync AI movement with animation
                yield return null;
            }

            // Pause for a moment
            yield return new WaitForSeconds(2f);  // NPC pauses for 2 seconds

            // Go to the next waypoint
            currentWaypoint = UnityEngine.Random.Range(0, waypoints.Length);
        }
    }
}
