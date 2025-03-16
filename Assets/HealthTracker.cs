using System.Linq;
using TMPro;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{

    public TextMeshProUGUI infectedText;
    public TextMeshProUGUI immuneText;

    int withImmunity = 0;
    int withDisease = 0;

    void Start()
    {

    }

    void Update()
    {
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);


        withDisease = allObjects.Count(obj => obj.GetComponent<Disease>() != null);
        //int withImmunity = allObjects.Count(obj => obj.GetComponent<Infectible>().myImmunity.Count != 0);
        //int total = allObjects.Count(obj => obj.GetComponent<Infectible>() != null);

        var infectibleObjects = allObjects
        .Select(obj => obj.GetComponent<Infectible>())
        .Where(infectible => infectible != null).ToList();

        withImmunity = infectibleObjects.Count(i => i.myImmunity.Count != 0);

        Debug.Log($"Objects with disease: {withDisease}");
        Debug.Log($"Objects with Immunity: {withImmunity}");
        Debug.Log($"Total objects: {infectibleObjects.Count}");
    }
}
