using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Target _target;
    [SerializeField] float _spawnPeriod = 0.5f;
    [SerializeField] int _poolSize = 500;
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemy),
            actionOnGet: (enemy) => InitEnemy(enemy),
            actionOnRelease: (enemy) => DoReleaseActions(enemy),
            collectionCheck: true,
            defaultCapacity: _poolSize,
            maxSize: _poolSize);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = Random.insideUnitCircle.normalized;
        direction.z = direction.y;
        direction.y = 0;
        return direction;
    }

    private void InitEnemy(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.Init(GetDirection(), _target.transform);
        enemy.SetActive(true);
        enemy.Died += ReleaseEnemy;
    }

    private void DoReleaseActions(Enemy enemy)
    {
        enemy.SetActive(false);
        enemy.Died -= ReleaseEnemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds time = new WaitForSeconds(_spawnPeriod);

        while(enabled)
        {
            yield return time;
            _pool.Get();
        }
    }
}