using System;
using System.Collections;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using System.Numerics;
using Res.Scripts.Sounds;
using UnityEngine;
using UnityEngine.Serialization;

namespace Res.Scripts.Object
{
    public class ObjectData : MonoBehaviour
    {
        
        [SerializeField] private float absorptionCoef;
        [SerializeField] private float surface;
        [SerializeField] private string materialName;
        
        public void UpdateMaterial(string matName)
        {
            materialName = matName;
            absorptionCoef = GetAbsorptionCoef();
            AcousticCalculation.Instance.UpdateAcousticCalculation();
        }

        public float GetAbsorptionArea()
        {
            return absorptionCoef * surface;
        }

        public float GetAbsorptionCoef()
        {
            switch (SoundData.Instance.Frequency)
            {
                case 125:
                    switch (materialName)
                    {
                        case "Tiled floor":
                            return 0.05f;
                             
                        case "Carpet":
                            return 0.15f;
                             
                        case "PVC floor":
                            return 0.04f;
                             
                        case "PVC ceiling":
                            return 0.02f;
                             
                        case "Vibrasto ceiling":
                            return 0.04f;
                             
                        case "Tonga ceiling":
                            return 0.35f;
                             
                        case "Concrete":
                            return 0.02f;
                             
                        case "Prégybel plaster":
                            return 0.67f;
                             
                        case "Agglomerated cork":
                            return 0.15f;
                             
                        case "Acoustic panel":
                            return 0.35f;
                             
                        case "Wooden seat":
                            return 0.03f;
                             
                        case "Fabric armchair":
                            return 0.15f;
                             
                        case "Plastic chair":
                            return 0.09f;
                             
                        case "Fiberboard table":
                            return 0.12f;
                             
                        case "Table":
                            return 0.22f;
                             
                        case "Cupboard":
                            return 0.6f;
                             
                        case "Fiberboard cupboard":
                            return 0.12f;
                             
                        default:
                            return 0.02f;
                             
                    }
                     
                case 250:
                    switch (materialName)
                    {
                        case "Tiled floor":
                            return 0.08f;
                             
                        case "Carpet":
                            return 0.22f;
                             
                        case "PVC floor":
                            return 0.06f;
                             
                        case "PVC ceiling":
                            return 0.03f;
                             
                        case "Vibrasto ceiling":
                            return 0.09f;
                             
                        case "Tonga ceiling":
                            return 0.7f;
                             
                        case "Concrete":
                            return 0.02f;
                             
                        case "Prégybel plaster":
                            return 0.78f;
                             
                        case "Agglomerated cork":
                            return 0.26f;
                             
                        case "Acoustic panel":
                            return 0.45f;
                             
                        case "Wooden seat":
                            return 0.04f;
                             
                        case "Fabric armchair":
                            return 0.20f;
                             
                        case "Plastic chair":
                            return 0.13f;
                             
                        case "Fiberboard table":
                            return 0.21f;
                             
                        case "Table":
                            return 0.28f;
                             
                        case "Cupboard":
                            return 0.4f;
                             
                        case "Fiberboard cupboard":
                            return 0.21f;
                             
                        default:
                            return 0.02f;
                             
                    }
                     
                case 500:
                    switch (materialName)
                    {
                        case "Tiled floor":
                            return 0.02f;
                             
                        case "Carpet":
                            return 0.4f;
                             
                        case "PVC floor":
                            return 0.08f;
                             
                        case "PVC ceiling":
                            return 0.03f;
                             
                        case "Vibrasto ceiling":
                            return 0.16f;
                             
                        case "Tonga ceiling":
                            return 0.9f;
                             
                        case "Concrete":
                            return 0.02f;
                             
                        case "Prégybel plaster":
                            return 0.78f;
                             
                        case "Agglomerated cork":
                            return 0.22f;
                             
                        case "Acoustic panel":
                            return 0.6f;
                             
                        case "Wooden seat":
                            return 0.04f;
                             
                        case "Fabric armchair":
                            return 0.3f;
                             
                        case "Plastic chair":
                            return 0.15f;
                             
                        case "Fiberboard table":
                            return 0.59f;
                             
                        case "Table":
                            return 0.25f;
                             
                        case "Cupboard":
                            return 0.18f;
                             
                        case "Fiberboard cupboard":
                            return 0.59f;
                             
                        default:
                            return 0.02f;
                             
                    }
                     
                case 1000:
                    switch (materialName)
                    {
                        case "Tiled floor":
                            return 0.03f;
                             
                        case "Carpet":
                            return 0.65f;
                             
                        case "PVC floor":
                            return 0.12f;
                             
                        case "PVC ceiling":
                            return 0.04f;
                             
                        case "Vibrasto ceiling":
                            return 0.26f;
                             
                        case "Tonga ceiling":
                            return 0.85f;
                             
                        case "Concrete":
                            return 0.03f;
                             
                        case "Prégybel plaster":
                            return 0.71f;
                             
                        case "Agglomerated cork":
                            return 0.22f;
                             
                        case "Acoustic panel":
                            return 0.7f;
                             
                        case "Wooden seat":
                            return 0.05f;
                             
                        case "Fabric armchair":
                            return 0.40f;
                             
                        case "Plastic chair":
                            return 0.15f;
                             
                        case "Fiberboard table":
                            return 0.74f;
                             
                        case "Table":
                            return 0.2f;
                             
                        case "Cupboard":
                            return 0.18f;
                             
                        case "Fiberboard cupboard":
                            return 0.74f;
                             
                        default:
                            return 0.03f;
                             
                    }
                     
                case 2000:
                    switch (materialName)
                    {
                        case "Tiled floor":
                            return 0.04f;
                             
                        case "Carpet":
                            return 0.9f;
                             
                        case "PVC floor":
                            return 0.04f;
                             
                        case "PVC ceiling":
                            return 0.06f;
                             
                        case "Vibrasto ceiling":
                            return 0.37f;
                             
                        case "Tonga ceiling":
                            return 0.95f;
                             
                        case "Concrete":
                            return 0.04f;
                             
                        case "Prégybel plaster":
                            return 0.62f;
                             
                        case "Agglomerated cork":
                            return 0.2f;
                             
                        case "Acoustic panel":
                            return 0.8f;
                             
                        case "Wooden seat":
                            return 0.05f;
                             
                        case "Fabric armchair":
                            return 0.5f;
                             
                        case "Plastic chair":
                            return 0.11f;
                             
                        case "Fiberboard table":
                            return 0.82f;
                             
                        case "Table":
                            return 0.2f;
                             
                        case "Cupboard":
                            return 0.18f;
                             
                        case "Fiberboard cupboard":
                            return 0.82f;
                             
                        default:
                            return 0.04f;
                             
                    }
                     
                case 4000:
                    switch (materialName)
                    {
                        case "Tiled floor":
                            return 0.04f;
                             
                        case "Carpet":
                            return 0.9f;
                             
                        case "PVC floor":
                            return 0.04f;
                             
                        case "PVC ceiling":
                            return 0.05f;
                             
                        case "Vibrasto ceiling":
                            return 0.49f;
                             
                        case "Tonga ceiling":
                            return 0.95f;
                             
                        case "Concrete":
                            return 0.04f;
                             
                        case "Prégybel plaster":
                            return 0.60f;
                             
                        case "Agglomerated cork":
                            return 0.2f;
                             
                        case "Acoustic panel":
                            return 0.85f;
                             
                        case "Wooden seat":
                            return 0.06f;
                             
                        case "Fabric armchair":
                            return 0.6f;
                             
                        case "Plastic chair":
                            return 0.07f;
                             
                        case "Fiberboard table":
                            return 0.74f;
                             
                        case "Table":
                            return 0.28f;
                             
                        case "Cupboard":
                            return 0.18f;
                             
                        case "Fiberboard cupboard":
                            return 0.74f;
                             
                        default:
                            return 0.04f;
                             
                    }
                     
                default:
                    return 0.03f;
                     
            }
        }
        
        public float AbsorptionCoef
        {
            get => absorptionCoef;
            set => absorptionCoef = value;
        }

        public float Surface
        {
            get => surface;
            set => surface = value;
        }

        public string MaterialName
        {
            get => materialName;
            set => materialName = value;
        }
    }
}



