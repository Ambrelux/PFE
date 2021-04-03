﻿using System;
using UnityEngine;

namespace Res.Scripts.Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        public GameObject mainCamera;
        public GameObject topViewCamera;


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Keypad8))
            {
                topViewCamera.SetActive(!topViewCamera.activeSelf);

            }
        }
    }
}