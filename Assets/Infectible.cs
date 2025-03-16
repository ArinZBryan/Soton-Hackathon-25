using UnityEngine;
using System.Collections.Generic;
using System;


public class Infectible : MonoBehaviour
{
    SphereCollider infectArea;
    public List<Disease> myDiseases = new List<Disease>();
    public List<Immunity> myImmunity = new List<Immunity>();

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
        for (int i = 0; i < myImmunity.Count; i++) {
            float Immunityduration = myImmunity[i].immunityDuration;
            if (Time.time - myImmunity[i].startTime > Immunityduration)
            {
                myImmunity.RemoveAt(i);
            }
        }

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
                        Infectible collidedWithInfectable = collidedWith.GetComponent<Infectible>();

                        for (int j = 0; j < collidedWithInfectable.myImmunity.Count; j++) {
                            if (myImmunity[j].diseaseType != diseaseType) {
                                collidedWith.AddComponent(diseaseType);
                                collidedWithInfectable.myDiseases.Add((Disease)collidedWith.GetComponent(diseaseType));
                            }
                        }
                    }
                }
            }
        }
    }
}
