using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _rotationSpeed = 10;
    private Transform _target;

    public event Action<Enemy> Died;

    private void OnEnable()
    {
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Target>(out Target target))
        {
            SetActive(false);
            Died?.Invoke(this);
        }
    }

    private void Update()
    {
        Rotate();
        Move();
    }

    public void Init(Vector3 direction, Transform transform)
    {
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        _target = transform;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void Rotate()
    {
        Vector3 targetVector = _target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed);
    }

    private void Move()
    {
        if (gameObject.activeSelf)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }
}