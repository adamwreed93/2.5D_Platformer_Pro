using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _movementTime = 10.0f;
    [SerializeField] private float _waitTime = 5.0f; 
    private Transform _target;

    private bool _moveElevator;

    private void Start()
    {
        _target = _waypoints[1];
        StartCoroutine(ElevatorMovement());
    }

    private void FixedUpdate()
    {
        if (_moveElevator == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }


    private IEnumerator ElevatorMovement()
    {
        while (true)
        {
            _moveElevator = true;
            yield return new WaitForSeconds(_movementTime);
            _moveElevator = false;

            if (_target == _waypoints[0])
            {
                _target = _waypoints[1];
            }
            else if(_target == _waypoints[1])
            {
                _target = _waypoints[0];
            }
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
