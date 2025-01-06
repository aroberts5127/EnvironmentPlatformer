using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        private float _speed = 5;
        //private float _jumpPower = 350f;
        private Rigidbody2D _rb;
        public bool IsGrounded { get; private set; }
        private float _groundedCheckDist = 1.1f;

        private float _jumpCurTimer;
        private float _jumpMaxTimer = .4f;
        private float _jumpMaxHeight = 1.4f;
        [SerializeField]
        private float _standardGravity = 1.0f;
        [SerializeField]
        private float _fallingGravity = 1.3f;



        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _jumpCurTimer = 0.0f;
        }

        public void MovePlayer(Vector3 movement)
        {
            if (movement == Vector3.zero)
                return;
            Vector3 target = transform.position + movement.normalized;
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            //transform.position += movement.normalized * speed;
        }

        public void Jump()
        {
            _rb.gravityScale = _standardGravity;
            _jumpCurTimer = 0.0f;
            var v0 = (2 * _jumpMaxHeight) / _jumpMaxTimer;
            _rb.velocity = new Vector2(_rb.velocity.x, v0);
        }

        public void HandleExtendedJumpInput()
        {
            
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            JumpBehavior();

        }

        private void CheckGrounded()
        {
            RaycastHit2D results = Physics2D.Raycast(this.transform.position, Vector2.down, _groundedCheckDist, 7);
            IsGrounded = (results.collider != null);
            //Debug.Log("Grounded: " + IsGrounded);
            //TODO - Handle Coyote Timer?
        }

        private void JumpBehavior()
        {
            if (IsGrounded)
            {
                return;
            }
            if (_jumpCurTimer < _jumpMaxTimer)
            {
                _rb.gravityScale = _standardGravity;
                _jumpCurTimer += Time.deltaTime;
            }
            else
            {
                _rb.gravityScale = _fallingGravity;
            }
        }

        // private void OnDrawGizmos()
        // {
        //     Vector2 center = this.GetComponent<Collider2D>().transform.position;
        //     Gizmos.DrawLine(center, center + Vector2.down * _groundedCheckDist);
        // }
    }
}
