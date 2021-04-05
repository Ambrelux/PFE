using System;
using System.Collections.Generic;
using UnityEngine;

namespace Res.Scripts.Spheres
{
    public class SpherePooler : MonoBehaviour
    {
        public static SpherePooler SharedInstance;
        [SerializeField] private List<GameObject> pooledSpheres;
        [SerializeField] private GameObject sphereToPool;
        [SerializeField] private Transform parent;
        [SerializeField] private int amountToPool;
        [SerializeField] private bool shouldExpand = true;
        void Awake() {
            SharedInstance = this;
            amountToPool = 50;
        }

        private void Start()
        {
            pooledSpheres = new List<GameObject>();
            for (int i = 0; i < amountToPool; i++) {
                GameObject obj = (GameObject)Instantiate(sphereToPool, parent);
                obj.SetActive(false); 
                pooledSpheres.Add(obj);
            }
        }
        
        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pooledSpheres.Count; i++) {
                if (!pooledSpheres[i].activeInHierarchy) {
                    return pooledSpheres[i];
                }
            }
            
            if (shouldExpand) {
                GameObject obj = (GameObject)Instantiate(sphereToPool, parent);
                obj.SetActive(false);
                pooledSpheres.Add(obj);
                return obj;
            }
            
            return null;
        }
    }
}
