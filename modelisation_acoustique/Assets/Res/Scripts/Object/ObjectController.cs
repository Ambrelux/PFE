using UnityEngine;

namespace Res.Scripts.Object
{
    public class ObjectController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private GameObject _movableObject;
        private void Update()
        {
            OnClickSelectObject();
            Move();
        }

        private void Move()
        {
            if (_movableObject != null)
            {
                if (Input.GetKey(KeyCode.Keypad2))
                {
                    _movableObject.transform.Translate(Vector3.left * (Time.deltaTime * speed));
                }

                if (Input.GetKey(KeyCode.Keypad5))
                {
                    _movableObject.transform.Translate(Vector3.right * (Time.deltaTime * speed));
                }

                if (Input.GetKey(KeyCode.Keypad1))
                {
                    _movableObject.transform.Translate(Vector3.forward * (Time.deltaTime * speed));
                }

                if (Input.GetKey(KeyCode.Keypad3))
                {
                    _movableObject.transform.Translate(-Vector3.forward * (Time.deltaTime * speed));
                }

                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    _movableObject.transform.Rotate(new Vector3(0,90,0));
                }
                
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    _movableObject.transform.Rotate(new Vector3(0,-90,0));
                }
            }
        }

        private void OnClickSelectObject()
        {
            if (Input.GetMouseButtonDown(2))
            {
                RaycastHit hit;
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.collider.gameObject.CompareTag("Furniture"))
                    {
                        _movableObject = hit.collider.gameObject;
                    }
                }
            }
        }
        
    }
}
