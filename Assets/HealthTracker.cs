using System.Linq;
using System;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HealthTracker : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI infectedText;
    [SerializeField] public TextMeshProUGUI immuneText;

    [SerializeField] public TextMeshProUGUI personD;
    [SerializeField] public TextMeshProUGUI secondD;
    [SerializeField] public TextMeshProUGUI LeprosyD;
    [SerializeField] public TextMeshProUGUI TuberculosisD;

    private List<Type> diseaseList = new List<Type>();

    int withImmunity = 0;
    int withDisease = 0;

    void Start()
    {
        infectedText.text = "Infected: " + withDisease.ToString();
        immuneText.text = "Immune: " + withImmunity.ToString();
    }

    void Update()
    {
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);


        withDisease = allObjects.Count(obj => obj.GetComponent<Disease>() != null);

        diseaseList = allObjects
            .Select(obj => obj.GetComponent<Infectible>()) // Get Infectible component
            .Where(infectible => infectible != null)        // Filter out nulls
            .SelectMany(infectible => infectible.myDiseases) // Flatten all Diseases
            .Where(disease => disease != null)              // Ensure disease exists
            .Select(disease => disease.GetType())           // Get the actual type
            .Distinct()                                     // Remove duplicates
            .ToList();


        var infectibleObjects = allObjects
        .Select(obj => obj.GetComponent<Infectible>())
        .Where(infectible => infectible != null).ToList();

        withImmunity = infectibleObjects.Count(i => i.myImmunity.Count != 0);

        Debug.Log($"Objects with disease: {withDisease}");
        Debug.Log($"Objects with Immunity: {withImmunity}");
        Debug.Log($"Total objects: {infectibleObjects.Count}");
        Debug.Log($"Diseases: {diseaseList}");

        infectedText.text = "Infected: " + withDisease.ToString();
        immuneText.text = "Immune: " + withImmunity.ToString();

        Dictionary<System.Type, int> diseaseData = GetDiseaseDistribution(allObjects);

        personD.text = "Black Death: " + (diseaseData.ContainsKey(typeof(person_disease)) ? ((diseaseData[typeof(person_disease)] / infectibleObjects.Count) * 100).ToString() : "0") + "%";
        secondD.text = "Smallpox: " + (diseaseData.ContainsKey(typeof(second_disease)) ? ((diseaseData[typeof(second_disease)] / infectibleObjects.Count) * 100).ToString() : "0") + "%";
        LeprosyD.text = "Lepropsy: " + (diseaseData.ContainsKey(typeof(Lepropsy)) ? ((diseaseData[typeof(Lepropsy)] / infectibleObjects.Count) * 100).ToString() : "0") + "%";
        TuberculosisD.text = "Tuberculosis: " + (diseaseData.ContainsKey(typeof(Tuberculosis)) ? ((diseaseData[typeof(Tuberculosis)] / infectibleObjects.Count) * 100).ToString() : "0") + "%";

    }

    public Dictionary<System.Type, int> GetDiseaseDistribution(GameObject[] allObjects)
    {
        Dictionary<System.Type, int> diseaseCounts = new Dictionary<System.Type, int>();

        foreach (GameObject obj in allObjects)
        {
            Infectible infectible = obj.GetComponent<Infectible>();
            if (infectible == null) continue;

            foreach (Disease disease in infectible.myDiseases)
            {
                System.Type diseaseType = disease.GetType();
                if (diseaseCounts.ContainsKey(diseaseType))
                {
                    diseaseCounts[diseaseType]++;
                }
                else
                {
                    diseaseCounts[diseaseType] = 1;
                }
            }
        }

        return diseaseCounts;
    }
}
