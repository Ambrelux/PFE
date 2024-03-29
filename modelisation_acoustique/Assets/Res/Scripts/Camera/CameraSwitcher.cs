﻿using System;
using UnityEngine;

namespace Res.Scripts.Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject topViewCamera;


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Tab))
                SwitchCamera();
        }

        /// <summary>
        /// This function switches between main and top view cameras. (Activated or disactived)
        /// 
        /// </summary>
        private void SwitchCamera()
        {
            mainCamera.SetActive(!mainCamera.activeSelf);
            topViewCamera.SetActive(!topViewCamera.activeSelf);    
        }
    }
}