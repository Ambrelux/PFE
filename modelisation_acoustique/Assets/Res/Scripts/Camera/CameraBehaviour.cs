using System;
using UnityEngine;

namespace Res.Scripts.Camera
{
    public class CameraBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed = 15;
        [SerializeField] private float rotationSpeed = 50;

        private void Update()
        {
            Rotate();
            Move();
            Raise();
        }

        /// <summary>
        /// This function allows user to rotate active camera with arrow keys.
        /// </summary>
        private void Rotate()
        {
            if(Input.GetAxis("Horizontal") > 0)
            {
                transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed));
            }
            else if(Input.GetAxis("Horizontal") < 0)
            {
                transform.Rotate(-Vector3.up * (Time.deltaTime * rotationSpeed));
            }
        }
        
        /// <summary>
        /// This function allows user to move active camera with arrow keys.
        /// </summary>
        private void Move()
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * speed));
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                transform.Translate(-Vector3.forward * (Time.deltaTime * speed));
            }
        }

        /// <summary>
        /// This function allows user to rise or lower active camera with H and B keys.
        /// </summary>
        private void Raise()
        {
            if (Input.GetKey(KeyCode.H))
            {
                transform.Translate(Vector3.up * (Time.deltaTime * speed));
            }
            else if (Input.GetKey(KeyCode.B))
            {
                transform.Translate(-Vector3.up * (Time.deltaTime * speed));
            }
        }
    }
}