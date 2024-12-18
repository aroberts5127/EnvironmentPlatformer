using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        private float _speed = 5;
        private float _jumpPower = 350f;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
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
            _rb.AddForce(new Vector2(0.0f, 1.0f * _jumpPower));
        }
    }
}
