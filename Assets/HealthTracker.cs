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
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Villager");


        withDisease = allObjects.Count(obj => obj.GetComponent<Disease>() != null);


        var infectibleObjects = allObjects
        .Select(obj => obj.GetComponent<Infectible>())
        .Where(infectible => infectible != null).ToList();

        withImmunity = infectibleObjects.Count(i => i.myImmunity.Count != 0);

        Debug.Log($"Objects with disease: {withDisease}");
        Debug.Log($"Objects with Immunity: {withImmunity}");
        Debug.Log($"Total objects: {infectibleObjects.Count}");

        infectedText.text = "Infected: " + withDisease.ToString();
        immuneText.text = "Immune: " + withImmunity.ToString();

        Dictionary<System.Type, int> diseaseData = GetDiseaseDistribution(allObjects);

        var blackDeathPC = 0f;

        if (diseaseData.ContainsKey(typeof(person_disease)))
        {
            diseaseData.TryGetValue(typeof(person_disease), out int blackDeathCount);
            if (blackDeathCount > 0 && infectibleObjects.Count > 0)
            {
                blackDeathPC = ((float)blackDeathCount / infectibleObjects.Count) * 100f;
            }
        }

        var smallpoxPC = 0f;

        if (diseaseData.ContainsKey(typeof(second_disease)))
        {
            diseaseData.TryGetValue(typeof(second_disease), out int smallpoxCount);
            if (smallpoxCount > 0 && infectibleObjects.Count > 0)
            {
                smallpoxPC = ((float)smallpoxCount / infectibleObjects.Count) * 100f;
            }
        }

        var lepropsyPC = 0f;

        if (diseaseData.ContainsKey(typeof(Lepropsy)))
        {
            diseaseData.TryGetValue(typeof(Lepropsy), out int lepropsyCount);
            if (lepropsyCount > 0 && infectibleObjects.Count > 0)
            {
                lepropsyPC = ((float)lepropsyCount / infectibleObjects.Count) * 100f;
            }
        }

        var tuberculosisPC = 0f;

        if (diseaseData.ContainsKey(typeof(Tuberculosis)))
        {
            diseaseData.TryGetValue(typeof(Tuberculosis), out int tuberculosisCount);
            if (tuberculosisCount > 0 && infectibleObjects.Count > 0)
            {
                tuberculosisPC = ((float)tuberculosisCount / infectibleObjects.Count) * 100f;
            }
        }


        personD.text = "Black Death: " + blackDeathPC.ToString("F2").Substring(0, 4) + "%";
        secondD.text = "Smallpox: " + smallpoxPC.ToString("F2").Substring(0, 4) + "%";
        LeprosyD.text = "Lepropsy: " + lepropsyPC.ToString("F2").Substring(0, 4) + "%";
        TuberculosisD.text = "Tuberculosis: " + tuberculosisPC.ToString("F2").Substring(0, 4) + "%";

        //personD.text = "Black Death: " + (diseaseData.ContainsKey(typeof(person_disease)) ? ((diseaseData[typeof(person_disease)] / infectibleObjects.Count) * 100).ToString() : "0") + "%";
        //secondD.text = "Smallpox: " + (diseaseData.ContainsKey(typeof(second_disease)) ? ((diseaseData[typeof(second_disease)] / infectibleObjects.Count) * 100).ToString() : "0") + "%";
        //LeprosyD.text = "Lepropsy: " + (diseaseData.ContainsKey(typeof(Lepropsy)) ? ((diseaseData[typeof(Lepropsy)] / infectibleObjects.Count) * 100).ToString() : "0") + "%";
        //TuberculosisD.text = "Tuberculosis: " + (diseaseData.ContainsKey(typeof(Tuberculosis)) ? ((diseaseData[typeof(Tuberculosis)] / infectibleObjects.Count) * 100).ToString() : "0") + "%";

    }

    public Dictionary<System.Type, int> GetDiseaseDistribution(GameObject[] allObjects)
    {
        Dictionary<System.Type, int> diseaseCounts = new Dictionary<System.Type, int>();

        foreach (GameObject obj in allObjects)
        {
            Infectible infectible = obj.GetComponent<Infectible>();

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
