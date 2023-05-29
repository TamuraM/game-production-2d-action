using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>プレイヤーに向かって突進する敵</summary>
public class RushEnemy : MonoBehaviour
{
    [SerializeField] private float _rushSpeed = 3f;
    [SerializeField] private float _knockBackSpeed = 5f;
    [SerializeField] private float _interval = 2f;
    private float _timer = 0f;
    private Transform _player = default;
    [SerializeField] private float _rushDistance = 5f;
    private Vector3 _direction = Vector3.up;
    [SerializeField] private bool _isRush = false;
    [SerializeField] private bool _isCollisionPlayer = false;

    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        
        if(!_isRush && Vector3.Distance(transform.position, _player.transform.position) < _rushDistance)
        {
            _isRush = true;
            _direction = (_player.transform.position - transform.position).normalized;
        }

        if(!_isCollisionPlayer && _isRush)
        {
            transform.position += _direction * _rushSpeed * Time.deltaTime;

            if(transform.position.x < -10 || transform.position.x > 10 || transform.position.y < -6 || transform.position.y > 6)
            {
                Destroy(gameObject);
            }

            bool isHitX = Mathf.Abs(transform.position.x - _player.position.x) <= (transform.localScale.x + _player.localScale.x) / 2;
            bool isHitY = Mathf.Abs(transform.position.y - _player.position.y) <= (transform.localScale.y + _player.localScale.y) / 2;

            if(isHitX && isHitY)
            {
                _isCollisionPlayer = true;
                _player.GetComponent<IDamageable>().Damage(1);
                _direction = -(_player.transform.position - transform.position).normalized;
            }

        }

        if(_isCollisionPlayer)
        {
            transform.position += _direction * _knockBackSpeed * Time.deltaTime;

            if (transform.position.x < -10 || transform.position.x > 10 || transform.position.y < -6 || transform.position.y > 6)
            {
                Destroy(gameObject);
            }

            _timer += Time.deltaTime;

            if(_timer > _interval)
            {
                _isRush = false;
                _isCollisionPlayer = false;
                _timer = 0f;
            }

        }

    }
}
