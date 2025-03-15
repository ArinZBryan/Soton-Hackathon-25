using UnityEngine;
using System.Collections.Generic;

public class SpriteHealth : MonoBehaviour
{
    private List<Disease> activeDiseases = new List<Disease>();

    public void AcquireDisease(Disease newDisease)
    {
        activeDiseases.Add(newDisease);
        newDisease.InitializeDisease();
        StartCoroutine(newDisease.IncubationPeriod());
    }
}
