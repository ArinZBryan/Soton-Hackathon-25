using UnityEngine;
using UnityEngine.AI; // Required for NavMesh
using System.Collections;

public class AIMovement : MonoBehaviour
{
    public Vector3 targetPos = new Vector3(0, 0, 0);
    public NavMeshAgent agent;
    public Animator animator;

    public void Start() {
        animator = GetComponent<Animator>();
    }

    public void Update() {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed); // Sync AI movement with animation
    }

    protected IEnumerator RotateToTarget(Quaternion targetRotation, float speed)
    {
        agent.isStopped = true; // Stop movement
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f) // Check if rotation is close enough
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            yield return null; // Wait until next frame
        }

        transform.rotation = targetRotation; // Ensure it snaps to the exact target at the end
        agent.isStopped = false; // Resume movement
    }

    public void UpdateTarget()
    {
        RotateToTarget(Quaternion.LookRotation(targetPos - transform.position), 3.5f); // Rotate towards target
        // RotateToTarget(Quaternion.LookRotation(targetPos - transform.position), 0.5f); // Rotate towards target
        agent.SetDestination(targetPos);
    }
}