using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class CameraTransitionController : MonoBehaviour
    {
        private Transform mainCameraTransform;
        [SerializeField]
        private float _moveSpeed = 50.0f;
        [SerializeField]
        private Room _currentRoom;
        private PlayerInput _pi;
        private PlayerMotor _pm;
        private Vector3 cameraTarget;
        private void Start()
        {
            _pi = this.GetComponent<PlayerInput>();
            _pm = this.GetComponent<PlayerMotor>();
            if(Camera.main != null)
                mainCameraTransform = Camera.main.transform;
            else
            {
                Debug.LogError("Main Camera Not Found for Transition Controller.");
                this.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (cameraTarget != Vector3.zero)
            {
                //Offset is larger than .05f because of the Z position
                if (Vector3.Distance(mainCameraTransform.position, cameraTarget) > 0.05f)
                {
                    MoveCameraToNewScreen(cameraTarget);
                }
                else
                {
                    cameraTarget = Vector3.zero;
                    _pi.StartInputs();
                }
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("World Trigger Entered");
            if (other.CompareTag("World"))
            {
                _currentRoom = other.GetComponentInParent<Room>();
                cameraTarget = new Vector3(_currentRoom.transform.position.x, _currentRoom.transform.position.y + 1.7f, mainCameraTransform.position.z);
                _pi.StopInputs();
            }

        }

        private void MoveCameraToNewScreen(Vector3 target)
        {
            mainCameraTransform.position = Vector3.MoveTowards(mainCameraTransform.position, target, 
                _moveSpeed * Time.deltaTime);
        }
    }
}