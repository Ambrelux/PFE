using System;
using UnityEngine;

namespace Res.Scripts.Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        public GameObject mainCamera;
        public GameObject topViewCamera;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                mainCamera.SetActive(!mainCamera.activeSelf);
                topViewCamera.SetActive(!topViewCamera.activeSelf);               
            }
        }
    }
}