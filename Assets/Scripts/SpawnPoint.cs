using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _player = default;
    [SerializeField] private GameObject _enemy = default;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private float _spawnDistance = 4f;
    private Transform[] _spawnEnemies;

    [SerializeField] private float _spawnInterval = default;
    private float _timer = 0;
    private GameObject _spawnEnemy = default;

    void Start()
    {
        _spawnEnemies = new Transform[_spawnPoints.Count];
    }

    void Update()
    {

        //登録したポイントにスポーンさせる
        foreach (var sp in _spawnPoints)
        {

            if (sp.childCount > 0) continue;

            if (Vector3.Distance(sp.position, _player.position) <= _spawnDistance)
            {
                Instantiate(_enemy, sp);
            }

        }

        //一定時間ごとにスポーン
        //_timer += Time.deltaTime;

        //if(_timer > _spawnInterval)
        //{

        //    if(_spawnEnemy == null)
        //    {
        //        _spawnEnemy = Instantiate(_enemy, transform);
        //    }

        //    _timer = 0;
        //}

    }

}
