using UnityEngine;
using UnityEngine.AI; // Required for NavMesh

public class AIMovement : MonoBehaviour
{
    public Vector3 targetPos = new Vector3(0, 0, 0);
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // Get the NavMeshAgent component
    }

    public void UpdateTarget()
    {
        agent.SetDestination(targetPos);
    }
}