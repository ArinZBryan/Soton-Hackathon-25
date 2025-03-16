using UnityEngine;
using System;
using TMPro.Examples;

public class person_disease : Disease
{
    public override void InitializeDisease()
    {
        Debug.Log("Initialised Disease");
        // Set values either via code or Inspector
        spreadProbabilityDuringIncubation = 0.5f;
        spreadProbabilityAfterIncubation = 0.5f;
        interactionRadius = 5f;
        deathProbability = 0.0010f;
        recoveryProbability = 0.0100f;
        incubationPeriodDuration = 5f;


        //Renderer rend = GetComponent<Renderer>();
        //originalMaterial = rend.material;
        //originalColor = originalMaterial.color;
    }

    public override void OnIncubationComplete()
    {
        // Add symptom visual effects or gameplay impacts
        //originalMaterial.color = originalColor * new Color(0.5f, 1f, 0.5f);
    }

    protected override void StaySick()
    {
        // Implement persistent effects
    }

    protected override void Recover()
    {
        //originalMaterial.color = originalColor * new Color(1f, 0.5f, 0.5f);

        Infectible MyInfectable = gameObject.GetComponent<Infectible>();

        MyInfectable.myImmunity.Add(new Immunity(this.GetType()));
        MyInfectable.myDiseases.Remove(this);
        Debug.Log(MyInfectable.myDiseases);
        base.Recover();
    }
}