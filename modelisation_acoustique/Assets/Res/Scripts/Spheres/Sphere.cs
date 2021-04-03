using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Object;
using Res.Scripts.UserInterface;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;

namespace Res.Scripts.Spheres

{
    public class Sphere : MonoBehaviour
    {
        private bool _isReplayed;
        private Vector3 _direction;
        private List<Vector3> _coordinates = new List<Vector3>();
        private int _nbBounce;
        
        private Renderer _objectRenderer;      
        private readonly Color _startColor = new Color32(71, 255, 78, 255);
        private readonly Color _endColor = new Color32(255, 0,0 , 255);
        private Color _objectColor;
        private Color _rayColor;

        private void Awake()
         {
             _objectRenderer = transform.gameObject.GetComponent<Renderer>();
             _rayColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
         }
        
        private void OnEnable()
        {
            _nbBounce = 0;
            _objectRenderer.material.SetColor("_Color",_startColor);
            _objectRenderer.material.SetColor("_EmissionColor",_startColor);
            if(!_isReplayed)
                ComputeCoords();

            if (_coordinates.Count == 0)
            {
                transform.gameObject.SetActive(false);
                return;
            }

            StartCoroutine(MoveSphere());
        }
        
        private void ComputeCoords()
        {
            if (_coordinates.Count == 0)
                return;
            
            var direction = _direction;
            var origin = _coordinates[0];
                
            for (var i = 1; i <= 500; i++)
            {
                RaycastHit _hit;
                if (!Physics.Raycast(origin, direction, out _hit, 100f)) continue;
                direction = Vector3.Reflect(direction.normalized, _hit.normal);
                origin = _hit.point + 0.01f * direction;
                _coordinates.Add(origin);
            }
        }
        



        private IEnumerator MoveSphere()
        {
            if (_coordinates.Count == 0)
                yield break;

            var distCovered = 0f;
            
            for (int i = 0; i < _coordinates.Count -2; i++)
            {
                var startCoord = _coordinates[i];
                var endCoord = _coordinates[i+1];
                var startTime = Time.time;
                var journeyLength = Vector3.Distance(startCoord, endCoord);
                var lastSegmentLength = 0f;
                while (Vector3.Distance(transform.position, endCoord) > 0.05f)
                {
                    var fractionOfJourney = (Time.time - startTime) * 20f / journeyLength;
                    var lastPosition = transform.position;
                    
                    if (AcousticCalculation.Instance.ReverbDistance > distCovered)
                    {
                        transform.position =
                            Vector3.Lerp(startCoord, endCoord, fractionOfJourney);
                        lastSegmentLength += Vector3.Distance(lastPosition, transform.position);
                        distCovered += Vector3.Distance(lastPosition, transform.position);
                        
                        var interColor = distCovered / AcousticCalculation.Instance.ReverbDistance;
                        _objectColor = Color.Lerp(_startColor, _endColor, interColor);
                        _objectRenderer.material.SetColor("_Color",_objectColor);
                        _objectRenderer.material.SetColor("_EmissionColor",_objectColor);
                        yield return null;
                    }
                    else
                    {
                        _nbBounce = i;
                        if (UiToggle.toggle)
                        {
                            DrawRaycast(lastSegmentLength);    
                        }
                        
                        yield return new WaitForSeconds(5);
                        transform.gameObject.SetActive(false);
                    }
                }

            }
            
        }

        private void DrawRaycast(float lastSegmentLength)
        {
            if (_coordinates.Count < 2)
                return;
            
            for (var j = 0; j <= _nbBounce; j++)
            {
                var startPoint = _coordinates[j];
                var endPoint = _coordinates[j+1];

                if (j == _nbBounce)
                {
                    var dir = (endPoint - startPoint).normalized;
                    endPoint = startPoint + (dir * lastSegmentLength);
                    Debug.DrawLine(startPoint, endPoint, _rayColor, 5f, false);
                }
                else
                {
                    Debug.DrawLine(startPoint, endPoint, _rayColor, 5f, false);
                }
            }
        }
        
        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }
        
        public List<Vector3> Coordinates
        {
            get => _coordinates;
            set => _coordinates = value;
        }

        public bool IsReplayed
        {
            get => _isReplayed;
            set => _isReplayed = value;
        }
    } 
}

// public GameObject sphereObject;
//         private List<Vector3> _waveCoordData = new List<Vector3>();
//         //private AcousticCalculation _acousticCalculation;
//         private readonly Color _startColor = new Color32(71, 255, 78, 255);
//         private readonly Color _endColor = new Color32(255, 0,0 , 255);
//         private Color _objectColor;
//         private Renderer _objectRenderer;
//         private int _nbBounce = 0;
//         private Color _rayColor;
//         public Color RayColor => _rayColor;
//
//         public int NbBounce
//         {
//             get => _nbBounce;
//             set => _nbBounce = value;
//         }
//
//         public List<Vector3> WaveCoordData
//         {
//             get => _waveCoordData;
//             set => _waveCoordData = value;
//         }
//
//         private IEnumerator MoveSphere()
//         {
//             if (_waveCoordData.Count == 0)
//             {
//                 yield break;
//             }
//
//             var distCovered = 0f;
//             
//             for (int i = 1; i < _waveCoordData.Count; i++)
//             {
//                 var startCoord = _waveCoordData[i - 1];
//                 var endCoord = _waveCoordData[i];
//                 var startTime = Time.time;
//                 var journeyLength = Vector3.Distance(startCoord, endCoord);
//                 
//                 while (Vector3.Distance(transform.position, endCoord) > 0.05f)
//                 {
//                     var fractionOfJourney = (Time.time - startTime) * 20f / journeyLength;
//                     var lastPosition = transform.position;
//                     if (AcousticCalculation.Instance.ReverbDistance > distCovered)
//                     {
//                         transform.position =
//                             Vector3.Lerp(startCoord, endCoord, fractionOfJourney);
//
//                         distCovered += Vector3.Distance(lastPosition, transform.position);
//                         
//                         var interColor = distCovered / AcousticCalculation.Instance.ReverbDistance;
//                         _objectColor = Color.Lerp(_startColor, _endColor, interColor);
//                         _objectRenderer.material.SetColor("_Color",_objectColor);
//                         yield return null;
//                     }
//                     else
//                     {
//                         _nbBounce = i;
//                         yield return new WaitForSeconds(5);
//                         sphereObject.SetActive(false);
//                     }
//                 }
//
//             }
//             
//         }
//

//
//         public void StartMovement()
//         {
//             StartCoroutine(MoveSphere());
//         }
//         
//     } 