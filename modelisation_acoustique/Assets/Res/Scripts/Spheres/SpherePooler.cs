using System.Collections.Generic;
using UnityEngine;

namespace Res.Scripts.Spheres
{
    public class SpherePooler : MonoBehaviour
    {
        public static SpherePooler SharedInstance;
        public List<GameObject> pooledSpheres;
        public GameObject sphereToPool;
        public Transform parent;
        public int amountToPool;
        public bool shouldExpand = true;
        void Awake() {
            SharedInstance = this;
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
