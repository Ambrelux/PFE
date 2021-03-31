using System.Collections;
using System.Collections.Generic;
using Res.Scripts.Object;
using Res.Scripts.UserInterface;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
        public float speed = 5f;
        public GameObject _movableObject;
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
            }
        }


        private void OnClickSelectObject()
        {
            if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
