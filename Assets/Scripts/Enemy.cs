using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] public float _lifeTime = 10;
    private Coroutine _waitForDeath;

    public event Action<Enemy> Died;

    private void OnEnable()
    {
        _waitForDeath = StartCoroutine(WaitForDeath());
    }

    private void OnDisable()
    {
        StopCoroutine(_waitForDeath);
    }

    private void Update()
    {
        Move();
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void Move()
    {
        if (gameObject.activeSelf)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }

    private IEnumerator WaitForDeath()
    {
        WaitForSeconds time = new WaitForSeconds(_lifeTime);
        yield return time;
        SetActive(false);
        Died.Invoke(this);
    }
}