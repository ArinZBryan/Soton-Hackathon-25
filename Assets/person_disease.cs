using UnityEngine;

public class person_disease : Disease
{
    private Color originalColor;
    private Material originalMaterial;
    protected override void InitializeDisease()
    {
        // Set values either via code or Inspector
        spreadProbabilityDuringIncubation = 0.1f;
        spreadProbabilityAfterIncubation = 0.4f;
        interactionRadius = 3f;
        deathProbability = 0.05f;
        recoveryProbability = 0.7f;
        incubationPeriodDuration = 10f;

        Renderer rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
        originalColor = originalMaterial.color;
    }

    protected override void OnIncubationComplete()
    {
        // Add symptom visual effects or gameplay impacts
        originalMaterial.color = originalColor * new Color(0.5f, 1f, 0.5f);
    }

    protected override void StaySick()
    {
        // Implement persistent effects
        GetComponent<Movement>().speed *= 0.5f;
    }

    protected override void Recover()
    {
        base.Recover();
    }
}