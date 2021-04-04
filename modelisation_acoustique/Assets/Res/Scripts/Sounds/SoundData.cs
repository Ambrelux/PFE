using System.Collections.Generic;
using Res.Scripts.Object;
using UnityEngine;

namespace Res.Scripts.Sounds
{
    public class SoundData : MonoBehaviour
    {
        private int _frequency;
        private int _nbSpheres;
        private float _sphereDuration;
        private static SoundData _instance;
        public static SoundData Instance { get { return _instance; } }

        private void Awake()
        {
            _instance = this;
            _frequency = 250;
            _nbSpheres = 10;
            _sphereDuration = 5f;
        }

        public void UpdateFrequency(int freq)
        {
            _frequency = freq;
            UpdateAllAbsorbCoeff();
            AcousticCalculation.Instance.UpdateAcousticCalculation();
        }

        public void UpdateNbSpheres(int nbSpheres)
        {
            _nbSpheres = nbSpheres;
        }

        public void UpdateSphereDuration(float duration)
        {
            _sphereDuration = duration;
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

        public float SphereDuration => _sphereDuration;
    }
}

