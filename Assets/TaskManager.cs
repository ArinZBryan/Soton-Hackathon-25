using UnityEngine;
using System.Collections.Generic;
using System;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public Vector3 currentTaskPosition;
    public bool currentTaskWillInfect;
    public float currentTaskDuration;
    System.Random rng = new System.Random();
    AIMovement AIMovement;
    void getNewTask()
    {
        Task currentTaskReference = tasks[rng.Next(0, tasks.Count - 1)];

        currentTaskPosition = currentTaskReference.position;
        currentTaskWillInfect = currentTaskReference.willInfect;
        currentTaskDuration = currentTaskReference.duration;
        AIMovement.targetPos = currentTaskPosition;
        AIMovement.UpdateTarget();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AIMovement = gameObject.GetComponent<AIMovement>();
        getNewTask();

    }

    private void FixedUpdate()
    {
        if (currentTaskDuration > 0)
        {
            if ((currentTaskPosition - transform.position).magnitude < 0.5) { currentTaskDuration -= Time.fixedDeltaTime; }
        }
        else
        {
            getNewTask();
        }
    }
}
