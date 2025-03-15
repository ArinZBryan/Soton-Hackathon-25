using System.Collections;
using UnityEngine;

public abstract class Disease : MonoBehaviour
{
    private Color originalColor;
    private Material originalMaterial;

    [Header("Transmission Settings")]
    [SerializeField][Range(0, 1)] protected float spreadProbabilityDuringIncubation = 0.2f;
    [SerializeField][Range(0, 1)] protected float spreadProbabilityAfterIncubation = 0.5f;
    [SerializeField] protected float interactionRadius = 2f;

    [Header("Outcome Probabilities")]
    [SerializeField][Range(0, 1)] protected float deathProbability = 0.1f;
    [SerializeField][Range(0, 1)] protected float recoveryProbability = 0.3f;
    protected float staySickProbability; // Calculated automatically

    [Header("Disease Timing")]
    [SerializeField] protected float incubationPeriodDuration = 5f;
    protected bool isIncubating = true;

    // Public properties for read-only access
    public float StaySickProbability => staySickProbability;
    public bool IsIncubating => isIncubating;

    protected virtual void OnValidate()
    {
        // Ensure probabilities sum to 1
        float total = deathProbability + recoveryProbability;

        if (total > 1f)
        {
            Debug.LogWarning($"Probabilities exceed 1! Adjusting values on {gameObject.name}");
            float overflow = total - 1f;
            deathProbability -= overflow / 2f;
            recoveryProbability -= overflow / 2f;
        }

        staySickProbability = 1f - deathProbability - recoveryProbability;
    }

    protected virtual void Start()
    {
        StartCoroutine(IncubationPeriod());
        InitializeDisease();
    }

    public virtual IEnumerator IncubationPeriod()
    {
        yield return new WaitForSeconds(incubationPeriodDuration);
        isIncubating = false;
        OnIncubationComplete();
    }

    public abstract void InitializeDisease(); // To be implemented by child classes
    public virtual void OnIncubationComplete() { } // Optional override

    public virtual bool TryInfect()
    {
        if (isIncubating) return Random.value <= spreadProbabilityDuringIncubation;
        return Random.value <= spreadProbabilityAfterIncubation;
    }

    public virtual void ResolveDisease()
    {
        float random = Random.value;

        if (random < deathProbability) Die();
        else if (random < deathProbability + recoveryProbability) Recover();
        else StaySick();
    }

    protected virtual void Die() { 
        // run done the death animation
        Destroy(gameObject);
    } // Example implementation
    protected virtual void Recover() {
        originalMaterial.color = originalColor;
        Destroy(this); 
    } // Remove disease component
    protected virtual void StaySick() { } // Custom logic in child classes

    private void FixedUpdate() {
        ResolveDisease();
    }
}