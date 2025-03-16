using UnityEngine;
using System;

public class RandomizeAppearance : MonoBehaviour
{
    System.Random rng;
    public Material[] skins;
    public Material[] sickSkins;
    public int skinID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rng = new();
        skinID = rng.Next(0, 8);

        switch (skinID)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = skins[skinID];
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(2).gameObject.GetComponent<Renderer>().material = skins[skinID - 4];
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
