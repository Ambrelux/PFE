using Res.Scripts.UserInterface;
using UnityEngine;

namespace Res.Scripts.Object
{
    public class ClickToModify : MonoBehaviour {
        void Update()
        {
            // if (!UiObject.Instance.uiObjectState && !UiWalls.Instance.uiWallsState && !UiReplaySounds.Instance.uiReplaySoundsState && !UiSounds.Instance.uiSoundsState)
            // {
            if(!AcousticCalculation.IsPointerOverUIObject()){
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100.0f))
                    {
                        if (hit.collider.gameObject.CompareTag("Furniture"))
                        {
                            if (UiObject.Instance.uiObjectState == false)
                            {
                                UiObject.Instance.ChangeState();
                                UiObject.Instance.objData = hit.collider.gameObject.GetComponent<ObjectData>();
                            }
                        }
                        else if (hit.collider.gameObject.CompareTag("Material"))
                        {
                            UiWalls.Instance.ChangeState();
                            UiWalls.Instance.objData = hit.collider.gameObject.GetComponent<ObjectData>();
                        }
                    }
                }
            }
        }
    }
}