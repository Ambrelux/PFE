using System;
using UnityEngine;

namespace Res.Scripts.UserInterface
{
    public class UiToggle : MonoBehaviour
    {
        public static bool toggle;

        private void OnEnable()
        {
            toggle = false;
        }

        public void UpdateToggle()
        {
            toggle = !toggle;
        }
    }
}