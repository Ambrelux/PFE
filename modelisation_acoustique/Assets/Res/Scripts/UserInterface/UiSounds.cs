﻿using System;
using UnityEngine;

namespace Res.Scripts.UserInterface
{
    public class UiSounds : MonoBehaviour
    {
        private static UiSounds _instance;
        public static UiSounds Instance { get { return _instance; } }
        
        public KeyCode key = KeyCode.LeftControl;
        public bool uiSoundsState = false;
        
        private void Awake()
        {
            _instance = this;
        }

        public void ChangeState()
        {
            foreach (Transform child in transform)
            {
                uiSoundsState = !uiSoundsState;
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(key))
            {
                ChangeState();
            }
        }

    }
}
