using Res.Scripts.UserInterface;
using UnityEngine;

namespace Res.Scripts.Object
{
    public class ClickToModify : MonoBehaviour {
        void Update()
        {
            if(!AcousticCalculation.IsPointerOverUIObject()){
                if (Input.GetMouseButtonDown(1))
                {
                    Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out var hit, 100.0f))
                    {
                        if (hit.transform.gameObject.CompareTag("Furniture"))
                        {
                            UiObject.Instance.ChangeState();
                            UiObject.Instance.objData = hit.transform.gameObject.GetComponent<ObjectData>();
                        }
                        else if (hit.transform.gameObject.CompareTag("Material"))
                        {
                            UiWalls.Instance.ChangeState();
                            UiWalls.Instance.objData = hit.transform.gameObject.GetComponent<ObjectData>();
                        }
                    }
                }
            }
        }
    }
}