using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Vector3[] _wayPoints;
    [SerializeField] private float _speed = 0.005f;
    private int _currentPointIndex = 0;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 position = Vector3.MoveTowards(transform.position, _wayPoints[_currentPointIndex], _speed * Time.deltaTime);
        transform.position = position;

        if (transform.position == _wayPoints[_currentPointIndex])
        {
            _currentPointIndex++;

            if (_currentPointIndex == _wayPoints.Length)
            {
                _currentPointIndex = 0;
            }
        }
    }
}
