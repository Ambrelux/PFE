using System;
using System.Collections.Generic;
using Res.Scripts.Object;
using UnityEngine;

namespace Res.Scripts.UserInterface
{
    public class UiObject : MonoBehaviour
    {
        
        private static UiObject _instance;
        public static UiObject Instance { get { return _instance; } }
        public ObjectData objData;

        private void Awake()
        {
            _instance = this;
        }

        public void ChangeState()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }

        public void OnClickDropDown(string matName)
        {
            objData.UpdateMaterial(matName);
        }

    }
}
