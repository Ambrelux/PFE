using UnityEngine;

namespace Res.Scripts.UserInterface
{
    public class UiToggle : MonoBehaviour
    {
        public static bool toggle = false;

        public void UpdateToggle()
        {
            toggle = !toggle;
        }
    }
}