using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    private Transform _target;

    private bool _switching;

    private void Start()
    {
        _target = _waypoints[1];
    }

    private void FixedUpdate()
    {
        PlatformMovement();
    }


    private void PlatformMovement()
    {
        if(_switching == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[1].position, _speed * Time.deltaTime);
        }
        else if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[0].position, _speed * Time.deltaTime);
        }

        if (transform.position == _waypoints[1].position)
        {
            _switching = true;
        }
        else if (transform.position == _waypoints[0].position)
        {
            _switching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
