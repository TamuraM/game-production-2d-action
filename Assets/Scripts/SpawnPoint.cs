using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _enemy = default;
    [SerializeField] private float _spawnInterval = default;
    private float _timer = 0;
    private GameObject _spawnEnemy = default;

    void Start()
    {
        _spawnEnemy = Instantiate(_enemy, transform);
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _spawnInterval)
        {

            if(_spawnEnemy == null)
            {
                _spawnEnemy = Instantiate(_enemy, transform);
            }
            
            _timer = 0;
        }


    }

}
