using UnityEngine;
using UnityEngine.AI; // Required for NavMesh

public class AIMovement : MonoBehaviour
{
    public Vector3 targetPos;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // Get the NavMeshAgent component
    }

    void Update()
    {
        agent.SetDestination(targetPos);
    }
}