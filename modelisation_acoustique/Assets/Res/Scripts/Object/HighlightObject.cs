using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Res.Scripts.Object
{
    public class HighlightObject : MonoBehaviour
    {
        
        private int _redCol;
        private int _greenCol;
        private int _blueCol;
        private bool _lookingAtObject = false;
        private bool _flashingIn = true;
        private bool _startedFlashing = false;
        public List<Material> materialList;
        public List<Color32> materialsColors;

        private void Start()
        {
            // we save the list of the materials of the game object and their respecting color
            AddMaterialInList(materialList);
            AddPreviousColorInList(materialsColors);
        }
        
        void Update()
        {
            if (_lookingAtObject == true)
            {
                
                for (int k = 0; k < materialList.Count; k++)
                {
                    materialList[k].color = new Color32((byte)_redCol, (byte)_greenCol, (byte)_blueCol,255);
                   
                }
 
            }
        }

        void AddMaterialInList(List<Material> list)
        {
            //a function that makes a list of all the materials present on the renderer
            list.Clear();

            Renderer rend = transform.GetComponent<Renderer>();
            
            for (int i = 0; i < rend.materials.Length; i++)
            {
                list.Add(rend.materials[i]);
            }
        }
        
        void AddPreviousColorInList(List<Color32> colorList)
        {
            //a function that saves original colors of each materials of a renderer
            colorList.Clear();

            Renderer rend = transform.GetComponent<Renderer>();
            
            for (int i = 0; i < rend.materials.Length; i++)
            {
                colorList.Add(rend.materials[i].color);
            }
        }


       

        void OnMouseOver()
        {
            //if the mouse is over the game object, it starts flashing
            if (!AcousticCalculation.IsPointerOverUIObject())
            {
                _lookingAtObject = true;
                if (_startedFlashing == false)
                {
                    _startedFlashing = true;
                    StartCoroutine(FlashObject());
                }
            }
        }

        void OnMouseExit()
        {
            //if the mouse is not over the game object anymore, the materials are set to their original color
            _startedFlashing = false;
            _lookingAtObject = false;
            StopCoroutine(FlashObject());
            for (int k = 0; k < materialList.Count; k++)
            {
                transform.GetComponent<Renderer>().materials[k].color = materialsColors[k];
            }
            
        }
        

        IEnumerator FlashObject()
        {
            
            while (_lookingAtObject)
            {
                yield return new WaitForSeconds(0.05f);
                if (_flashingIn)
                {
                    if (_redCol <= 30)
                    {
                        _flashingIn = false;
                    }
                    else
                    {
                        _redCol -= 20;
                        _blueCol -= 20;
                        _greenCol -= 20;
                    }
                }

                if (_flashingIn == false)
                {
                    if (_redCol >= 220)
                    {
                        _flashingIn = true;
                    }
                    else
                    {
                        _redCol +=20;
                        _greenCol +=20;
                        _blueCol +=20;
                    }
                }
            }
        }

    }
}
