using UnityEngine;
using System.Collections.Generic;
using System;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    Vector3 currentTaskPosition;
    bool currentTaskWillInfect;
    public float currentTaskDuration;
    public AIMovement AIMovement;
    System.Random rng;
    void getNewTask()
    {
        int chosenTask = rng.Next(0, tasks.Count - 1); 
        Task currentTaskReference = tasks[chosenTask];
        Debug.Log(chosenTask);

        currentTaskPosition = currentTaskReference.position;
        currentTaskWillInfect = currentTaskReference.willInfect;
        currentTaskDuration = currentTaskReference.duration;
        AIMovement.targetPos = currentTaskPosition;
        AIMovement.UpdateTarget();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] TP_objects = GameObject.FindGameObjectsWithTag("TaskPoint");
        Debug.Log("Found " + TP_objects.Length.ToString() + " tasks");
        for (int i = 0; i < TP_objects.Length; i++) { tasks.Add(TP_objects[i].GetComponent<Task>()); }
        rng = new System.Random();
        getNewTask();

    }

    private void FixedUpdate()
    {
        if (currentTaskDuration > 0)
        {
            if ((currentTaskPosition - transform.position).magnitude < 2) { currentTaskDuration -= Time.fixedDeltaTime; }
        }
        else
        {
            getNewTask();
        }
    }
}