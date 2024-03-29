﻿using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Object;
using Res.Scripts.Sounds;
using Res.Scripts.UserInterface;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Res.Scripts.Spheres
{
    public class Sphere : MonoBehaviour
    {
        private bool _isReplayed;
        private Vector3 _direction;
        private readonly List<Vector3> _coordinates = new List<Vector3>();
        private int _nbBounce;
        private Renderer _objectRenderer;      
        private readonly Color _startColor = new Color32(71, 255, 78, 255);
        private readonly Color _endColor = new Color32(255, 0,0 , 255);
        private Color _objectColor;

        private void Awake()
         {
             _objectRenderer = transform.gameObject.GetComponent<Renderer>();
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
        
        /// <summary>
        /// This function calculates the contact points of the sphere before it moves in order to improve performance
        /// </summary>
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

        /// <summary>
        /// This coroutine moves sphere between each point calculated before.
        /// The sphere stops moving when it travelled the same distance as reverberation distance
        /// Sphere's color changes from green to red according to the distance travelled .
        /// </summary>
        /// <returns></returns>
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
                            DrawLine(lastSegmentLength);    
                        }
                        
                        yield return new WaitForSeconds(SoundData.Instance.SphereDuration);
                        transform.gameObject.SetActive(false);
                    }
                }
            }
            transform.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// This function is called when user allows raycast, it draws lines between each point.
        /// The line's color changes depending on distance travelled.
        /// </summary>
        /// <param name="lastSegmentLength">Distance between n-1 point and end point of the sphere.</param>
        private void DrawLine(float lastSegmentLength)
        {
            if (_coordinates.Count < 2)
                return;
            
            for (var j = 0; j <= _nbBounce; j++)
            {
                var interColor = (j + 1f )/ _nbBounce;
                var rayColor = ColorPicker(interColor);
                var startPoint = _coordinates[j];
                var endPoint = _coordinates[j+1];
                
                if (j == _nbBounce)
                {
                    var dir = (endPoint - startPoint).normalized;
                    endPoint = startPoint + (dir * lastSegmentLength);
                    Debug.DrawLine(startPoint, endPoint, rayColor, SoundData.Instance.SphereDuration, false);
                }
                else
                {
                    Debug.DrawLine(startPoint, endPoint, rayColor, SoundData.Instance.SphereDuration, false);
                }
            }
        }

        /// <summary>
        /// This function works in the same way as ColorLerp.
        /// It returns a color depending on interColor's value.
        /// </summary>
        /// <param name="interColor"></param>
        /// <returns></returns>
        /// 
        private Color ColorPicker(float interColor)
        {
            Color orange = new Color32(254, 161, 0, 255);
            Color darkGreen = new Color32(0, 128, 0, 255);

            if (interColor <= 0.2f) return darkGreen;
            if (interColor <= 0.4f) return Color.green;
            if ( interColor <= 0.6f) return Color.yellow; 
            if (interColor <= 0.8f) return orange;
            
            return Color.red;
        }
        
        public Vector3 Direction
        {
            set => _direction = value;
        }
        
        public List<Vector3> Coordinates => _coordinates;

        public bool IsReplayed
        {
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