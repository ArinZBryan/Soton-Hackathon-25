using UnityEngine;
using System.Collections.Generic;
using System;


public class Infectible : MonoBehaviour
{
    SphereCollider infectArea;
    public List<Disease> myDiseases;

    LayerMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infectArea = GetComponent<SphereCollider>();
        infectArea.enabled = true;
        //mask = LayerMask.GetMask("Villagers");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        //if (collider.includeLayers == mask)
        {
            GameObject collidedWith = collider.gameObject;
            for (int i = 0; i < myDiseases.Count; i++)
            {
                Type diseaseType = myDiseases[i].GetType();
                if (myDiseases[i].TryInfect())
                {
                    if (collidedWith.GetComponent(diseaseType) == null)
                    {
                        collidedWith.AddComponent(diseaseType);
                        collidedWith.GetComponent<Infectible>().myDiseases.Add((Disease)collidedWith.GetComponent(diseaseType));
                    }
                }
            }
        }
    }
}
