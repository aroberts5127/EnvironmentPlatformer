using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMotor _motor;
        private Vector3 _currentMovement;

        private void Start()
        {
            _motor = this.GetComponent<PlayerMotor>();
            _currentMovement = new Vector3();
        }

        // Update is called once per frame
        void Update()
        {
            _currentMovement.x = Input.GetAxis("Horizontal");
            _currentMovement = _currentMovement.normalized;
            if(_currentMovement != Vector3.zero)
                _motor.MovePlayer(_currentMovement);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _motor.Jump();
            }
                
            
        }
    }
}