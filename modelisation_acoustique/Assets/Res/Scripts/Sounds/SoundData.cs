using System;
using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Object;
using UnityEngine;

public class SoundData : MonoBehaviour
{
    private int _frequency;
    private int _nbSpheres;
    private static SoundData _instance;
    public static SoundData Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
        _frequency = 250;
        _nbSpheres = 10;
    }

    public void UpdateFrequency(int freq)
    {
        _frequency = freq;
        UpdateAllAbsorbCoeff();
        AcousticCalculation.Instance.UpdateAcousticCalculation();
    }

    public void UpdateAllAbsorbCoeff()
    {
        List<GameObject> materialList = new List<GameObject>();
        List<GameObject> furnitureList= new List<GameObject>();  
        List<GameObject> personList= new List<GameObject>();
        
        materialList.AddRange(GameObject.FindGameObjectsWithTag("Material"));
        furnitureList.AddRange(GameObject.FindGameObjectsWithTag("Furniture"));
        personList.AddRange(GameObject.FindGameObjectsWithTag("Person"));

        foreach (GameObject gameObj in materialList)
        {
            gameObj.GetComponent<ObjectData>().absorptionCoef = gameObj.GetComponent<ObjectData>().GetAbsorptionCoef();
        }
        

    }

    public int NbSpheres => _nbSpheres;

    public int Frequency => _frequency;
}

