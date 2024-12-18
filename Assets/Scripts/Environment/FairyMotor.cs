using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment
{
    public class FairyMotor : MonoBehaviour
    {
        // Start is called before the first frame update
        private Vector2 _startingPos;
        private float _speed = 1;
        private Vector2 _target;
        
        private Coroutine _moveRoutine;

        private bool _moveComplete;
        void Start()
        {
            _startingPos = this.transform.position;
            _moveRoutine = StartCoroutine(MotorRoutine());
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, _target) >= 0.05f)
            {
                this.transform.position =
                    Vector2.MoveTowards(this.transform.position, _target, _speed * Time.deltaTime);
            }
            else
            {
                _moveComplete = true;
            }
        }


        private IEnumerator MotorRoutine()
        {
            Debug.Log("Starting MoveRoutine");
            //Select Point from circle around startingPos
            while (true)
            {
                Debug.Log("Starting Routine Again");
                _moveComplete = false;
                Vector2 offset = new Vector2(Random.Range(-0.75f, 0.75f), Random.Range(-0.25f, 0.25f));
                _target = _startingPos + offset;
                //MoveAction(target);
                yield return new WaitWhile(() => !_moveComplete);

            }
        }

        private void MoveAction(Vector2 target)
        {
            this.transform.position =
                Vector2.MoveTowards(this.transform.position, _target, _speed * Time.deltaTime);
            //_moveComplete = true;
        }
    }
}