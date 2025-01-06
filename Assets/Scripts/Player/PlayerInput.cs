using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMotor _motor;
        private Vector3 _currentMovement;
        private bool _acceptInputs;

        private void Start()
        {
            _motor = this.GetComponent<PlayerMotor>();
            _currentMovement = new Vector3();
            _acceptInputs = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_acceptInputs)
            {
                _currentMovement.x = Input.GetAxis("Horizontal");
                _currentMovement = _currentMovement.normalized;
                if (_currentMovement != Vector3.zero)
                    _motor.MovePlayer(_currentMovement);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_motor.IsGrounded)
                        _motor.Jump();
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    _motor.HandleExtendedJumpInput();
                }
            }
        }

        public void StartInputs()
        {
            if (_acceptInputs)
                return;
            //Debug.Log("Accepting Inputs");
            _acceptInputs = true;
        }
        public void StopInputs()
        {
            if (!_acceptInputs)
                return;
            //Debug.Log("Not Accepting Inputs");
            _acceptInputs = false;
        }
    }
}