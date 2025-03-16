using System;
using TMPro.Examples;
using UnityEngine;

[System.Serializable]
public class Immunity
{
    public float immunityDuration = 10f;
    public Type diseaseType;
    public float startTime;
    
    public Immunity(Type MyDisease)
    {
        diseaseType = MyDisease;
        startTime = Time.time;
    }
}
