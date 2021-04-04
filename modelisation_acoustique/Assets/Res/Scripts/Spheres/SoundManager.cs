using System.Collections.Generic;
using Res.Scripts.API;
using Res.Scripts.Object;
using Res.Scripts.Sounds;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Res.Scripts.Spheres
{
    public class SoundManager : MonoBehaviour
    {
        private RaycastHit _hit;
        private const float LineOffset = 0.01f;
        private readonly List<GameObject> _spheresList = new List<GameObject>();
        public static SoundManager Instance { get; private set; }
        private List<GameObject> _soundEmitters = new List<GameObject>();

        private void Awake()
        {
            _soundEmitters.AddRange(GameObject.FindGameObjectsWithTag("SoundEmitter"));
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                AcousticCalculation.Instance.UpdateAcousticCalculation();
                StartSound();
                StartCoroutine(ApiRequest.InsertSound(_spheresList));
            }
        }

        private void StartSound()
        {
            _spheresList.Clear();
            for (int j = 0; j < _soundEmitters.Count; j++)
            {
                for (var i = 0; i < SoundData.Instance.NbSpheres + 1; i++)
                {
                    var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))
                        .normalized;
                    var origin = _soundEmitters[j].transform.position + LineOffset * direction;

                    GameObject sphere = SpherePooler.SharedInstance.GetPooledObject();

                    if (sphere != null)
                    {
                        sphere.transform.position = _soundEmitters[j].transform.position;
                        var sphereScript = sphere.GetComponent<Sphere>();
                        sphereScript.Coordinates.Clear();
                        sphereScript.Coordinates.Add(origin);
                        sphereScript.Direction = direction;
                        sphereScript.IsReplayed = false;
                        sphere.SetActive(true);
                        _spheresList.Add(sphere);
                    }

                }
            }
        }

        public void InitSphere(int index, Coord[] coords)
        {
            GameObject sphere = SpherePooler.SharedInstance.GetPooledObject();

            if (sphere != null)
            {
                var sphereScript = sphere.GetComponent<Sphere>();
                sphereScript.Coordinates.Clear();
                for (int i = 0; i < coords.Length; i++)
                {
                    sphereScript.Coordinates.Add(new Vector3(coords[i].x, coords[i].y, coords[i].z));
                }

                sphereScript.IsReplayed = true;
                sphere.SetActive(true);
            }
        }
    }
}